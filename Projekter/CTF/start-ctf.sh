#!/bin/bash

# CTF Hub Start Script
echo "🏴‍☠️ Starter CTF Hub..."

# Tjek om Docker er installeret
if ! command -v docker &> /dev/null; then
    echo "❌ Docker er ikke installeret. Installer Docker først."
    exit 1
fi

# Tjek om docker-compose er tilgængelig
if ! command -v docker-compose &> /dev/null && ! docker compose version &> /dev/null; then
    echo "❌ Docker Compose er ikke tilgængelig."
    exit 1
fi

# Stop eksisterende container hvis den kører
echo "🛑 Stopper eksisterende container..."
docker-compose down 2>/dev/null || docker compose down 2>/dev/null

# Byg og start container
echo "🔨 Bygger og starter CTF Hub..."
if command -v docker-compose &> /dev/null; then
    docker-compose up --build -d
else
    docker compose up --build -d
fi

# Vent på at containeren starter
echo "⏳ Venter på at containeren starter..."
sleep 3

# Tjek om containeren kører
if docker ps | grep -q ctf-hub; then
    echo "✅ CTF Hub kører nu!"
    echo ""
    echo "🌐 Adgang til CTF Hub:"
    echo "   Lokalt: http://localhost:8080"
    echo "   Pharos: http://localhost:8080/Pharos/"
    echo "   Crypto: http://localhost:8080/Crypto/"
    echo ""
    echo "🔗 For tunnel (f.eks. ngrok):"
    echo "   ngrok http 8080"
    echo ""
    echo "🛑 For at stoppe: docker-compose down"
else
    echo "❌ Fejl ved opstart af container. Tjek logs:"
    docker-compose logs || docker compose logs
fi
