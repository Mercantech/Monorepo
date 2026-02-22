<#
.SYNOPSIS
    Pusher indholdet af en mappe i monorepo til det tilhørende GitHub-repo (fuld kopi).

.DESCRIPTION
    Laeser repos.json, finder URL for den angivne mappe, kloner repo til temp-mappe,
    kopierer monorepo-mappens indhold ind (overskriver), committer og pusher til GitHub.

.PARAMETER FolderPath
    Relativ sti i monorepo (som i repos.json), fx Courses/Templates/H3

.PARAMETER CommitMessage
    Valgfri commit-besked. Default: "Sync from monorepo"

.PARAMETER WhatIf
    Vis kun hvad der ville ske, uden clone/copy/push.

.PARAMETER Force
    Ved push: brug git push --force (overskriver remote). Kun hvis monorepo er sandheden.

.EXAMPLE
    .\Push-MonorepoFolderToRepo.ps1 -FolderPath "Courses/Templates/H3"
    .\Push-MonorepoFolderToRepo.ps1 -FolderPath "Courses/Templates/H3" -Force
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $FolderPath,

    [string] $CommitMessage = "Sync from monorepo",

    [switch] $WhatIf,
    [switch] $Force
)

$ErrorActionPreference = "Stop"
$FolderPath = $FolderPath -replace '\\', '/'
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$Root = Split-Path -Parent $ScriptDir
$ReposFile = Join-Path $ScriptDir "repos.json"
$SourcePath = Join-Path $Root $FolderPath

if (-not (Test-Path $ReposFile)) {
    Write-Error "repos.json findes ikke i scripts/. Koer Add-RepoAsCopy.ps1 foerst for at tilfoeje repoet."
}
$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }
$entry = $list | Where-Object { $_.path -eq $FolderPath } | Select-Object -First 1
if (-not $entry) {
    Write-Error "Mappen $FolderPath er ikke i repos.json. Tilfoej den med Add-RepoAsCopy.ps1."
}
if (-not (Test-Path $SourcePath)) {
    Write-Error "Mappen findes ikke i monorepo: $FolderPath"
}

$RepoUrl = $entry.url
$repoName = [System.IO.Path]::GetFileNameWithoutExtension($RepoUrl.TrimEnd('/').Split('/')[-1])
$tempRepo = Join-Path $env:TEMP "monorepo_push_$repoName"

if ($WhatIf) {
    Write-Host "[WhatIf] Ville: clone $RepoUrl til temp, kopiere $FolderPath ind, commit + push med besked: $CommitMessage"
    exit 0
}

# Ryd evt. gammel temp-klon
if (Test-Path $tempRepo) {
    Remove-Item -Recurse -Force $tempRepo -ErrorAction SilentlyContinue
}

Write-Host "Cloner $RepoUrl til temp..."
$errFile = Join-Path $env:TEMP "git_clone_err_$repoName.txt"
$p = Start-Process -FilePath "git" -ArgumentList "clone",$RepoUrl,$tempRepo -Wait -NoNewWindow -PassThru -RedirectStandardError $errFile
if ($p.ExitCode -ne 0) {
    if (Test-Path $errFile) { Get-Content $errFile -ErrorAction SilentlyContinue | Write-Host }
    Write-Error "Clone fejlede (exit $($p.ExitCode)). Tjek URL og adgang."
}
Remove-Item $errFile -ErrorAction SilentlyContinue

try {
    Write-Host "Kopierer indhold fra $FolderPath til temp-klon (overskriver, undtager .git)..."
    Get-ChildItem -Path $tempRepo -Force | Where-Object { $_.Name -ne ".git" } | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    Get-ChildItem -Path $SourcePath -Force -ErrorAction SilentlyContinue | Where-Object { $_.Name -ne ".git" } | ForEach-Object {
        Copy-Item -Path $_.FullName -Destination (Join-Path $tempRepo $_.Name) -Recurse -Force -ErrorAction SilentlyContinue
    }

    Push-Location $tempRepo
    try {
        git add -A
        $status = git status --porcelain
        if (-not $status) {
            Write-Host "Ingen aendringer at pushe."
            exit 0
        }
        git commit -m $CommitMessage
        if ($Force) { git push --force origin HEAD } else { git push origin HEAD }
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Pushet til $RepoUrl."
        } else {
            Write-Host "Push afvist (remote har evt. nye commits). Proev -Force hvis monorepo er sandheden, eller koer Pull foerst."
            exit 1
        }
    } finally {
        Pop-Location
    }
} finally {
    if (Test-Path $tempRepo) {
        Remove-Item -Recurse -Force $tempRepo -ErrorAction SilentlyContinue
    }
}
