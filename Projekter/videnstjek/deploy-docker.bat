@echo off
REM Videnstjek Docker Deployment Batch Script
REM Dette script hjælper med at deploye applikationen med Docker

echo 🐳 Videnstjek Docker Deployment Script
echo =====================================

REM Tjek om Docker er installeret
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker er ikke installeret eller ikke tilgængelig
    echo 📥 Download Docker Desktop fra: https://www.docker.com/products/docker-desktop
    pause
    exit /b 1
)

echo ✅ Docker er installeret og tilgængelig

REM Hvis ingen parametre er givet, vis hjælp
if "%1"=="" (
    echo.
    echo 📖 Brug:
    echo   deploy-docker.bat build-start     # Build og start applikationen
    echo   deploy-docker.bat start           # Start eksisterende containere
    echo   deploy-docker.bat stop            # Stop containere
    echo   deploy-docker.bat logs            # Vis container logs
    echo   deploy-docker.bat status          # Vis container status
    echo   deploy-docker.bat clean           # Rens Docker
    echo   deploy-docker.bat prod            # Production deployment
    echo.
    echo 🔧 Eksempler:
    echo   deploy-docker.bat build-start     # Development deployment
    echo   deploy-docker.bat prod            # Production deployment
    echo   deploy-docker.bat status          # Tjek status
    echo.
    pause
    exit /b 0
)

REM Håndter kommandoer
if "%1"=="build-start" (
    echo 🔨 Bygger og starter Docker containere...
    docker-compose up --build -d
    if %errorlevel% equ 0 (
        echo ✅ Applikationen startet succesfuldt!
        echo 🌐 Tilgængelig på: http://localhost:8067
    ) else (
        echo ❌ Fejl ved start af applikationen
    )
) else if "%1"=="start" (
    echo 🚀 Starter containere...
    docker-compose up -d
    if %errorlevel% equ 0 (
        echo ✅ Containere startet!
        echo 🌐 Tilgængelig på: http://localhost:8067
    ) else (
        echo ❌ Fejl ved start af containere
    )
) else if "%1"=="stop" (
    echo 🛑 Stopper containere...
    docker-compose down
    if %errorlevel% equ 0 (
        echo ✅ Containere stoppet!
    ) else (
        echo ❌ Fejl ved stop af containere
    )
) else if "%1"=="logs" (
    echo 📋 Viser container logs...
    docker-compose logs -f
) else if "%1"=="status" (
    echo 📊 Container status:
    docker-compose ps
    echo.
    echo 🔍 Docker container info:
    docker ps --filter "name=videnstjek"
) else if "%1"=="clean" (
    echo 🧹 Renser Docker...
    docker system prune -f
    echo ✅ Docker cleanup fuldført!
) else if "%1"=="prod" (
    echo 🏭 Production deployment...
    docker-compose -f docker-compose.prod.yml up --build -d
    if %errorlevel% equ 0 (
        echo ✅ Production applikation startet!
        echo 🌐 Tilgængelig på: http://localhost:8067
    ) else (
        echo ❌ Fejl ved production deployment
    )
) else (
    echo ❌ Ukendt kommando: %1
    echo Kør scriptet uden parametre for at se hjælp
)

echo.
echo ✨ Script fuldført!
pause
