<#
.SYNOPSIS
    Viser alle repos fra repos.json med nummer, sa du kan arbejde med een ad gangen.

.DESCRIPTION
    Bruges sammen med Sync-OneRepo.ps1: koer List-Repos for at se numre, derefter
    Sync-OneRepo.ps1 -Index 3 -Push for at pushe repo nr. 3.

.EXAMPLE
    .\List-Repos.ps1
    .\List-Repos.ps1 | Out-File repos-liste.txt
#>
[CmdletBinding()]
param()

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ReposFile = Join-Path $ScriptDir "repos.json"
if (-not (Test-Path $ReposFile)) { Write-Error "repos.json findes ikke." }

$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }

Write-Host ""
Write-Host "Repos i repos.json (brug -Index <nr> med Sync-OneRepo.ps1):"
Write-Host "-----------------------------------------------------------"
$i = 1
foreach ($e in $list) {
    $exists = Test-Path (Join-Path (Split-Path -Parent $ScriptDir) $e.path)
    $mark = if ($exists) { " " } else { "?" }
    Write-Host ("{0,3}. {1} {2}  ->  {3}" -f $i, $mark, $e.path, $e.url)
    $i++
}
Write-Host "-----------------------------------------------------------"
Write-Host "? = mappe findes ikke i monorepo endnu"
Write-Host ""
Write-Host "Eksempel: .\Sync-OneRepo.ps1 -Index 5 -Push"
Write-Host "         .\Sync-OneRepo.ps1 -FolderPath `"Courses/Templates/H3`" -Pull"
Write-Host ""
