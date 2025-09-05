# Videnstjek Docker Makefile
# Dette makefile gør det nemt at deploye applikationen med Docker

.PHONY: help build start stop logs status clean prod dev

# Default target
help:
	@echo "🐳 Videnstjek Docker Makefile"
	@echo "=============================="
	@echo ""
	@echo "📖 Kommandoer:"
	@echo "  make build     # Build Docker image"
	@echo "  make start     # Start containere"
	@echo "  make stop      # Stop containere"
	@echo "  make logs      # Vis container logs"
	@echo "  make status    # Vis container status"
	@echo "  make clean     # Rens Docker"
	@echo "  make prod      # Production deployment"
	@echo "  make dev       # Development deployment"
	@echo ""
	@echo "🔧 Eksempler:"
	@echo "  make dev       # Development deployment"
	@echo "  make prod      # Production deployment"
	@echo "  make status    # Tjek status"

# Build Docker image
build:
	@echo "🔨 Bygger Docker image..."
	docker-compose build

# Start development containere
start:
	@echo "🚀 Starter development containere..."
	docker-compose up -d

# Stop containere
stop:
	@echo "🛑 Stopper containere..."
	docker-compose down

# Vis logs
logs:
	@echo "📋 Viser container logs..."
	docker-compose logs -f

# Vis status
status:
	@echo "📊 Container status:"
	docker-compose ps
	@echo ""
	@echo "🔍 Docker container info:"
	docker ps --filter "name=videnstjek"

# Rens Docker
clean:
	@echo "🧹 Renser Docker..."
	docker system prune -f
	@echo "✅ Docker cleanup fuldført!"

# Production deployment
prod:
	@echo "🏭 Production deployment..."
	docker-compose -f docker-compose.prod.yml up --build -d
	@echo "✅ Production applikation startet!"
	@echo "🌐 Tilgængelig på: http://localhost:8067"

# Development deployment (build + start)
dev: build start
	@echo "✅ Development applikation startet!"
	@echo "🌐 Tilgængelig på: http://localhost:8067"

# Full production deployment
prod-full: clean prod
	@echo "✨ Production deployment fuldført!"

# Full development deployment
dev-full: clean dev
	@echo "✨ Development deployment fuldført!"
