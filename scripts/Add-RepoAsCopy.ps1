<#
.SYNOPSIS
    Tilfoejer et GitHub-repo til monorepo som almindelige filer (fuld kopi, ingen submodule).

.DESCRIPTION
    Clone repo til angivet mappesti, sletter .git sa mappen bliver almindelige filer i monorepo.
    Tilfoejer mappen til repos.json sa de andre scripts kan synce til/fra repoet.

.PARAMETER RepoUrl
    URL til GitHub-repo (fx https://github.com/Mercantech/h3.git)

.PARAMETER FolderPath
    Relativ sti i monorepo hvor indholdet skal ligge (fx Courses/Templates/H3).
    Bruges / i stien.

.EXAMPLE
    .\Add-RepoAsCopy.ps1 -RepoUrl "https://github.com/Mercantech/h3.git" -FolderPath "Courses/Templates/H3"
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $RepoUrl,

    [Parameter(Mandatory = $true)]
    [string] $FolderPath
)

$ErrorActionPreference = "Stop"
$FolderPath = $FolderPath -replace '\\', '/'
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$Root = Split-Path -Parent $ScriptDir

if (-not (Test-Path (Join-Path $Root ".git"))) {
    Write-Error "Kor dette script fra Monorepo-roden eller lad scripts ligge i Monorepo/scripts."
}

$ReposFile = Join-Path $ScriptDir "repos.json"
$FullPath = Join-Path $Root $FolderPath

# Mappen maa ikke allerede eksistere med indhold (ellers clone vil fejle eller overskrive)
if (Test-Path $FullPath) {
    $items = Get-ChildItem -Path $FullPath -Force -ErrorAction SilentlyContinue
    if ($items) {
        Write-Error "Mappen findes allerede og har indhold: $FolderPath. Valg anden sti eller flyt/omdoeb den foerst."
    }
}

# Clone til midlertidig mappe, flyt indhold til target, slet .git
$repoName = [System.IO.Path]::GetFileNameWithoutExtension($RepoUrl.TrimEnd('/').Split('/')[-1])
$tempClone = Join-Path $Root "._temp_clone_$repoName"
try {
    Write-Host "Cloner $RepoUrl til midlertidig mappe..."
    $errFile = Join-Path $env:TEMP "git_clone_err_$repoName.txt"
    $p = Start-Process -FilePath "git" -ArgumentList "clone","--depth","1",$RepoUrl,$tempClone -Wait -NoNewWindow -PassThru -RedirectStandardError $errFile
    if ($p.ExitCode -ne 0) {
        if (Test-Path $errFile) { Get-Content $errFile -ErrorAction SilentlyContinue | Write-Host }
        Write-Error "Clone fejlede (exit $($p.ExitCode)). Tjek URL og adgang."
    }
    Remove-Item $errFile -ErrorAction SilentlyContinue

    $parentDir = Split-Path -Parent $FullPath
    if (-not (Test-Path $parentDir)) {
        New-Item -ItemType Directory -Path $parentDir -Force | Out-Null
    }

    Write-Host "Flytter indhold til $FolderPath..."
    New-Item -ItemType Directory -Path $FullPath -Force | Out-Null
    Get-ChildItem -Path $tempClone -Force | Where-Object { $_.Name -ne ".git" } | ForEach-Object {
        Move-Item -Path $_.FullName -Destination $FullPath -Force -ErrorAction SilentlyContinue
    }
    if (Test-Path (Join-Path $FullPath ".git")) {
        Remove-Item -Recurse -Force (Join-Path $FullPath ".git")
    }
}
finally {
    if (Test-Path $tempClone) {
        Remove-Item -Recurse -Force $tempClone -ErrorAction SilentlyContinue
    }
}

# Opdater repos.json
$list = @()
if (Test-Path $ReposFile) {
    $list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($list -isnot [Array]) { $list = @($list) }
}
$existing = $list | Where-Object { $_.path -eq $FolderPath }
if ($existing) {
    $existing.url = $RepoUrl
} else {
    $list += [PSCustomObject]@{ path = $FolderPath; url = $RepoUrl }
}
$list | ConvertTo-Json -Depth 3 | Set-Content $ReposFile -Encoding UTF8

Write-Host "Tilfojet til monorepo og til repos.json. Naeste skridt:"
Write-Host "  cd `"$Root`""
Write-Host "  git add `"$FolderPath`" scripts/repos.json"
Write-Host "  git commit -m `"Add $repoName as copy`""
Write-Host "  git push"
Write-Host ""
Write-Host "For at pushe aendringer i denne mappe til GitHub-repoet: .\scripts\Push-MonorepoFolderToRepo.ps1 -FolderPath `"$FolderPath`""
