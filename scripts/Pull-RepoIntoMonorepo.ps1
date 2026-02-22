<#
.SYNOPSIS
    Henter indhold fra et GitHub-repo ind i den tilhørende mappe i monorepo (overskriver lokalt).

.DESCRIPTION
    Laeser repos.json, finder URL for den angivne mappe, kloner repo til temp,
    kopierer indholdet ind i monorepo-mappen (overskriver). Brug naar aendringer
    er lavet direkte i det enkelte repo og du vil have dem ind i monorepo.

.PARAMETER FolderPath
    Relativ sti i monorepo (som i repos.json), fx Courses/Templates/H3

.PARAMETER WhatIf
    Vis kun hvad der ville ske.

.EXAMPLE
    .\Pull-RepoIntoMonorepo.ps1 -FolderPath "Courses/Templates/H3"
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $FolderPath,

    [switch] $WhatIf
)

$ErrorActionPreference = "Stop"
$FolderPath = $FolderPath -replace '\\', '/'
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$Root = Split-Path -Parent $ScriptDir
$ReposFile = Join-Path $ScriptDir "repos.json"
$DestPath = Join-Path $Root $FolderPath

if (-not (Test-Path $ReposFile)) {
    Write-Error "repos.json findes ikke i scripts/."
}
$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }
$entry = $list | Where-Object { $_.path -eq $FolderPath } | Select-Object -First 1
if (-not $entry) {
    Write-Error "Mappen $FolderPath er ikke i repos.json."
}

$RepoUrl = $entry.url
$repoName = [System.IO.Path]::GetFileNameWithoutExtension($RepoUrl.TrimEnd('/').Split('/')[-1])
$tempRepo = Join-Path $env:TEMP "monorepo_pull_$repoName"

if ($WhatIf) {
    Write-Host "[WhatIf] Ville: clone $RepoUrl til temp, kopiere indhold til $FolderPath (overskriver)."
    exit 0
}

if (Test-Path $tempRepo) {
    Remove-Item -Recurse -Force $tempRepo -ErrorAction SilentlyContinue
}

Write-Host "Cloner $RepoUrl til temp..."
$errFile = Join-Path $env:TEMP "git_clone_err_$repoName.txt"
$p = Start-Process -FilePath "git" -ArgumentList "clone","--depth","1",$RepoUrl,$tempRepo -Wait -NoNewWindow -PassThru -RedirectStandardError $errFile
if ($p.ExitCode -ne 0) {
    if (Test-Path $errFile) { Get-Content $errFile -ErrorAction SilentlyContinue | Write-Host }
    Write-Error "Clone fejlede (exit $($p.ExitCode))."
}
Remove-Item $errFile -ErrorAction SilentlyContinue

try {
    $parentDir = Split-Path -Parent $DestPath
    if (-not (Test-Path $parentDir)) {
        New-Item -ItemType Directory -Path $parentDir -Force | Out-Null
    }
    if (Test-Path $DestPath) {
        Get-ChildItem -Path $DestPath -Force | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    }
    Write-Host "Kopierer indhold til $FolderPath (undtagen .git)..."
    Get-ChildItem -Path $tempRepo -Force -ErrorAction SilentlyContinue | Where-Object { $_.Name -ne ".git" } | ForEach-Object {
        Copy-Item -Path $_.FullName -Destination (Join-Path $DestPath $_.Name) -Recurse -Force -ErrorAction SilentlyContinue
    }
    if (Test-Path (Join-Path $DestPath ".git")) {
        Remove-Item -Recurse -Force (Join-Path $DestPath ".git") -ErrorAction SilentlyContinue
    }
    Write-Host "Indhold fra $RepoUrl er nu i $FolderPath. Commit evt. i monorepo og push."
} finally {
    if (Test-Path $tempRepo) {
        Remove-Item -Recurse -Force $tempRepo -ErrorAction SilentlyContinue
    }
}
