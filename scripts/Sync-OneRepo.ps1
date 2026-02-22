<#
.SYNOPSIS
    Push eller pull for EET enkelt repo (nummer fra List-Repos eller FolderPath).

.DESCRIPTION
    Sa du kan gaae repos enkeltvis igennem og evt. lose merge-konflikter per repo.
    Koer List-Repos.ps1 foerst for at se numre.

.PARAMETER Index
    Nummer fra List-Repos (1-based). Eksempel: -Index 5

.PARAMETER FolderPath
    Relativ sti som i repos.json. Eksempel: -FolderPath "Courses/Templates/H3"
    Bruges hvis du ikke vil bruge Index.

.PARAMETER Push
    Pusher monorepo-mappen til GitHub-repoet.

.PARAMETER Pull
    Henter GitHub-repoet ind i monorepo-mappen (overskriver).

.PARAMETER WhatIf
    Vis kun hvad der ville ske.

.PARAMETER Force
    Ved Push: sendes til Push-MonorepoFolderToRepo (git push --force).

.EXAMPLE
    .\scripts\List-Repos.ps1
    .\scripts\Sync-OneRepo.ps1 -Index 3 -Push
    .\scripts\Sync-OneRepo.ps1 -Index 3 -Push -Force
    .\scripts\Sync-OneRepo.ps1 -FolderPath "Courses/Templates/H3" -Push -WhatIf
#>
[CmdletBinding()]
param(
    [Parameter(ParameterSetName = "ByIndex")]
    [int] $Index,

    [Parameter(ParameterSetName = "ByPath")]
    [string] $FolderPath,

    [switch] $Push,
    [switch] $Pull,
    [switch] $WhatIf,
    [switch] $Force
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ReposFile = Join-Path $ScriptDir "repos.json"

if (-not (Test-Path $ReposFile)) { Write-Error "repos.json findes ikke." }
$list = Get-Content $ReposFile -Raw -Encoding UTF8 | ConvertFrom-Json
if ($list -isnot [Array]) { $list = @($list) }

if ($PSCmdlet.ParameterSetName -eq "ByIndex") {
    if ($Index -lt 1 -or $Index -gt $list.Count) {
        Write-Error "Index skal vaere mellem 1 og $($list.Count). Koer List-Repos.ps1 for at se listen."
    }
    $FolderPath = $list[$Index - 1].path
    Write-Host "Repo $Index : $FolderPath"
}

if (-not $FolderPath) {
    Write-Host "Angiv -Index <nr> eller -FolderPath `"path`". Eksempel: .\Sync-OneRepo.ps1 -Index 1 -Push"
    exit 1
}
if (-not $Push -and -not $Pull) {
    Write-Host "Angiv -Push og/eller -Pull."
    exit 1
}

if ($Push) {
    & (Join-Path $ScriptDir "Push-MonorepoFolderToRepo.ps1") -FolderPath $FolderPath -WhatIf:$WhatIf -Force:$Force
}
if ($Pull) {
    & (Join-Path $ScriptDir "Pull-RepoIntoMonorepo.ps1") -FolderPath $FolderPath -WhatIf:$WhatIf
}
