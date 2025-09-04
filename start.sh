#!/bin/bash

echo "🚀 Starter Obsidian Docs med MkDocs Material..."

# Tjek om docker-compose er installeret
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose er ikke installeret!"
    exit 1
fi

# Start containeren
docker-compose up -d

echo "✅ Obsidian Docs er startet!"
echo "🌐 Tilgængelig på: http://localhost:2847"
echo "📁 Dokumenter mappet fra: ./Obsidian"

# Vis logs
echo "📋 Viser logs (Ctrl+C for at stoppe):"
docker-compose logs -f
