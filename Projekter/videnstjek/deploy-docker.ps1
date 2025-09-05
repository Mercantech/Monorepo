# Videnstjek Docker Deployment Script
# Dette script hjælper med at deploye applikationen med Docker

param(
    [string]$Environment = "dev",
    [switch]$Build,
    [switch]$Start,
    [switch]$Stop,
    [switch]$Logs,
    [switch]$Status,
    [switch]$Clean
)

Write-Host "🐳 Videnstjek Docker Deployment Script" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan

# Funktion til at tjekke om Docker er installeret
function Test-Docker {
    try {
        $null = docker --version
        Write-Host "✅ Docker er installeret og tilgængelig" -ForegroundColor Green
        return $true
    }
    catch {
        Write-Host "❌ Docker er ikke installeret eller ikke tilgængelig" -ForegroundColor Red
        Write-Host "📥 Download Docker Desktop fra: https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
        return $false
    }
}

# Funktion til at bygge Docker image
function Build-DockerImage {
    Write-Host "🔨 Bygger Docker image..." -ForegroundColor Yellow
    
    if ($Environment -eq "prod") {
        Write-Host "🏭 Production build..." -ForegroundColor Blue
        docker-compose -f docker-compose.prod.yml build
    }
    else {
        Write-Host "🛠️ Development build..." -ForegroundColor Blue
        docker-compose build
    }
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Docker image bygget succesfuldt!" -ForegroundColor Green
    }
    else {
        Write-Host "❌ Docker build fejlede!" -ForegroundColor Red
        exit 1
    }
}

# Funktion til at starte containere
function Start-Containers {
    Write-Host "🚀 Starter containere..." -ForegroundColor Yellow
    
    if ($Environment -eq "prod") {
        Write-Host "🏭 Starter production containere..." -ForegroundColor Blue
        docker-compose -f docker-compose.prod.yml up -d
    }
    else {
        Write-Host "🛠️ Starter development containere..." -ForegroundColor Blue
        docker-compose up -d
    }
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Containere startet succesfuldt!" -ForegroundColor Green
        Write-Host "🌐 Applikationen er tilgængelig på: http://localhost:8067" -ForegroundColor Green
    }
    else {
        Write-Host "❌ Start af containere fejlede!" -ForegroundColor Red
        exit 1
    }
}

# Funktion til at stoppe containere
function Stop-Containers {
    Write-Host "🛑 Stopper containere..." -ForegroundColor Yellow
    
    if ($Environment -eq "prod") {
        docker-compose -f docker-compose.prod.yml down
    }
    else {
        docker-compose down
    }
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Containere stoppet!" -ForegroundColor Green
    }
    else {
        Write-Host "❌ Stop af containere fejlede!" -ForegroundColor Red
    }
}

# Funktion til at vise logs
function Show-Logs {
    Write-Host "📋 Viser container logs..." -ForegroundColor Yellow
    
    if ($Environment -eq "prod") {
        docker-compose -f docker-compose.prod.yml logs -f
    }
    else {
        docker-compose logs -f
    }
}

# Funktion til at vise status
function Show-Status {
    Write-Host "📊 Container status:" -ForegroundColor Yellow
    
    if ($Environment -eq "prod") {
        docker-compose -f docker-compose.prod.yml ps
    }
    else {
        docker-compose ps
    }
    
    Write-Host "`n🔍 Docker container info:" -ForegroundColor Yellow
    docker ps --filter "name=videnstjek"
}

# Funktion til at rense Docker
function Clean-Docker {
    Write-Host "🧹 Renser Docker..." -ForegroundColor Yellow
    
    Write-Host "Stopper alle containere..." -ForegroundColor Blue
    docker stop $(docker ps -q) 2>$null
    
    Write-Host "Fjerner stoppede containere..." -ForegroundColor Blue
    docker container prune -f
    
    Write-Host "Fjerner ubrugte images..." -ForegroundColor Blue
    docker image prune -f
    
    Write-Host "Fjerner ubrugte volumes..." -ForegroundColor Blue
    docker volume prune -f
    
    Write-Host "Fjerner ubrugte networks..." -ForegroundColor Blue
    docker network prune -f
    
    Write-Host "✅ Docker cleanup fuldført!" -ForegroundColor Green
}

# Hovedlogik
if (-not (Test-Docker)) {
    exit 1
}

# Håndter parametre
if ($Build) {
    Build-DockerImage
}

if ($Start) {
    Start-Containers
}

if ($Stop) {
    Stop-Containers
}

if ($Logs) {
    Show-Logs
}

if ($Status) {
    Show-Status
}

if ($Clean) {
    Clean-Docker
}

# Hvis ingen parametre er givet, vis hjælp
if (-not ($Build -or $Start -or $Stop -or $Logs -or $Status -or $Clean)) {
    Write-Host "`n📖 Brug:" -ForegroundColor Cyan
    Write-Host "  .\deploy-docker.ps1 -Build -Start          # Build og start applikationen" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Start                  # Start eksisterende containere" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Stop                   # Stop containere" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Logs                   # Vis container logs" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Status                 # Vis container status" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Clean                  # Rens Docker" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Environment prod      # Brug production konfiguration" -ForegroundColor White
    Write-Host "`n🔧 Eksempler:" -ForegroundColor Cyan
    Write-Host "  .\deploy-docker.ps1 -Build -Start           # Development deployment" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Build -Start -Environment prod  # Production deployment" -ForegroundColor White
    Write-Host "  .\deploy-docker.ps1 -Status                 # Tjek status" -ForegroundColor White
}

Write-Host "`n✨ Script fuldført!" -ForegroundColor Green
