@echo off
REM CTF Hub Start Script for Windows

echo 🏴‍☠️ Starter CTF Hub...

REM Tjek om Docker er installeret
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker er ikke installeret. Installer Docker først.
    pause
    exit /b 1
)

REM Stop eksisterende container hvis den kører
echo 🛑 Stopper eksisterende container...
docker-compose down 2>nul
docker compose down 2>nul

REM Byg og start container
echo 🔨 Bygger og starter CTF Hub...
docker-compose up --build -d 2>nul
if %errorlevel% neq 0 (
    docker compose up --build -d
)

REM Vent på at containeren starter
echo ⏳ Venter på at containeren starter...
timeout /t 3 /nobreak >nul

REM Tjek om containeren kører
docker ps | findstr ctf-hub >nul
if %errorlevel% equ 0 (
    echo ✅ CTF Hub kører nu!
    echo.
    echo 🌐 Adgang til CTF Hub:
    echo    Lokalt: http://localhost:8080
    echo    Pharos: http://localhost:8080/Pharos/
    echo    Crypto: http://localhost:8080/Crypto/
    echo.
    echo 🔗 For tunnel (f.eks. ngrok):
    echo    ngrok http 8080
    echo.
    echo 🛑 For at stoppe: docker-compose down
) else (
    echo ❌ Fejl ved opstart af container. Tjek logs:
    docker-compose logs
    docker compose logs
)

pause
