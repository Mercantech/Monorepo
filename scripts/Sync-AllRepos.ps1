<#
.SYNOPSIS
    Syncer alle mapper fra repos.json: push monorepo -> GitHub og/eller pull GitHub -> monorepo.

.DESCRIPTION
    Laeser repos.json og koerer enten Push-MonorepoFolderToRepo eller Pull-RepoIntoMonorepo
    for hver mappe (eller begge med -Push og -Pull).

.PARAMETER Push
    Pusher indholdet af hver mappe i monorepo til det tilhoerende GitHub-repo.

.PARAMETER Pull
    Henter indhold fra hvert GitHub-repo ind i den tilhoerende mappe i monorepo (overskriver).

.PARAMETER WhatIf
    Vis kun hvad der ville ske for hver mappe.

.EXAMPLE
    .\Sync-AllRepos.ps1 -Push
    .\Sync-AllRepos.ps1 -Pull
    .\Sync-AllRepos.ps1 -Push -Pull
#>
[CmdletBinding()]
param(
    [switch] $Push,
    [switch] $Pull,
    [switch] $WhatIf
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ReposFile = Join-Path $ScriptDir "repos.json"

if (-not (Test-Path $ReposFile)) {
    Write-Error "repos.json findes ikke i scripts/."
}
if (-not $Push -and -not $Pull) {
    Write-Host "Angiv -Push og/eller -Pull. Eksempel: .\Sync-AllRepos.ps1 -Push"
    exit 1
}

$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }
if ($list.Count -eq 0) {
    Write-Host "repos.json er tom."
    exit 0
}

foreach ($entry in $list) {
    $path = $entry.path
    Write-Host "--- $path ---"
    if ($Push) {
        & (Join-Path $ScriptDir "Push-MonorepoFolderToRepo.ps1") -FolderPath $path -WhatIf:$WhatIf
    }
    if ($Pull) {
        & (Join-Path $ScriptDir "Pull-RepoIntoMonorepo.ps1") -FolderPath $path -WhatIf:$WhatIf
    }
}
Write-Host "Faerdig."
