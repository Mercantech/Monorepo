<#
.SYNOPSIS
    Viser for hver repo i repos.json om den er opsat korrekt (almindelige filer) eller stadig submodule/mangler.

.DESCRIPTION
    Tjekker: findes mappen, indeholder den .git (submodule/embedded repo), er den i Git-index som gitlink (160000).
    OK = mappe findes, ingen .git indeni, monorepo tracker filerne som almindelige filer.

.EXAMPLE
    .\Get-RepoStatus.ps1
#>
[CmdletBinding()]
param()

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$Root = Split-Path -Parent $ScriptDir
$ReposFile = Join-Path $ScriptDir "repos.json"

if (-not (Test-Path $ReposFile)) { Write-Error "repos.json findes ikke." }
if (-not (Test-Path (Join-Path $Root ".git"))) { Write-Error "Kør scriptet fra monorepo-roden (mappe med .git)." }

$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }

# Hent alle gitlink-stier fra index (160000)
$gitlinks = @{}
Push-Location $Root
try {
    $lines = git ls-files -s 2>$null | Where-Object { $_ -match "^\s*160000\s+" }
    foreach ($line in $lines) {
        $parts = $line -split "\t", 2
        if ($parts.Count -ge 2) { $gitlinks[$parts[1].Trim()] = $true }
    }
} finally {
    Pop-Location
}

Write-Host ""
Write-Host "Status for repos (OK = almindelige filer, Submodule = har .git eller er gitlink, Mangler = mappe findes ikke):"
Write-Host "=========================================================================================================="

$ok = 0
$sub = 0
$mangler = 0

foreach ($e in $list) {
    $path = $e.path -replace '\\', '/'
    $fullPath = Join-Path $Root $path
    $exists = Test-Path $fullPath
    $hasGit = $exists -and (Test-Path (Join-Path $fullPath ".git"))
    $isGitlink = $gitlinks.ContainsKey($path)
    $normPath = $path -replace '/', [System.IO.Path]::DirectorySeparatorChar
    $isGitlinkAlt = $gitlinks.ContainsKey($normPath)

    if (-not $exists) {
        $status = "Mangler"
        $mangler++
    } elseif ($hasGit -or $isGitlink -or $isGitlinkAlt) {
        $status = "Submodule"
        $sub++
    } else {
        $status = "OK"
        $ok++
    }

    $col = 14
    $statusPadded = $status.PadRight($col)
    Write-Host ("  {0}  {1}" -f $statusPadded, $path)
}

Write-Host "=========================================================================================================="
Write-Host "  OK: $ok  |  Submodule: $sub  |  Mangler: $mangler"
Write-Host ""
Write-Host "Submodule = mappen har .git eller er registreret som gitlink. Kør evt.:"
Write-Host "  Remove-Item -Recurse -Force `"<sti>\.git`" -ErrorAction SilentlyContinue"
Write-Host "  git rm --cached `"<sti>`""
Write-Host "  git add `"<sti>`""
Write-Host "  (eller Pull for at hente indhold, derefter fjerne .git i mappen og git add igen)"
Write-Host ""
