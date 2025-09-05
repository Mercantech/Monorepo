# Docker Deployment Guide for Videnstjek

Denne guide forklarer hvordan du deployer Videnstjek Blazor applikationen med Docker.

## 🐳 Forudsætninger

- Docker Desktop installeret og kørende
- Docker Compose installeret
- Git til at klone repository'et

## 🚀 Hurtig Start

### 1. Klon Repository
```bash
git clone <repository-url>
cd videnstjek
```

### 2. Build og Start (Development)
```bash
# Start applikationen i development mode
docker-compose up --build

# Eller kør i baggrunden
docker-compose up -d --build
```

### 3. Build og Start (Production)
```bash
# Start applikationen i production mode
docker-compose -f docker-compose.prod.yml up --build -d
```

## 📁 Docker Filer

### Dockerfile
- **Multi-stage build** for optimal image størrelse
- **Build stage**: Kompilerer applikationen med .NET 9 SDK
- **Runtime stage**: Kører applikationen med .NET 9 Runtime
- **Port 8067** eksponeret

### docker-compose.yml
- **Development konfiguration**
- **Port mapping**: 8067:8067
- **Volume mounting** for live reload
- **Health checks** inkluderet

### docker-compose.prod.yml
- **Production optimeret**
- **Resource limits** defineret
- **Restart policies** konfigureret
- **Security hardening**

## 🔧 Kommandoer

### Development
```bash
# Start applikationen
docker-compose up

# Start i baggrunden
docker-compose up -d

# Se logs
docker-compose logs -f

# Stop applikationen
docker-compose down
```

### Production
```bash
# Start production version
docker-compose -f docker-compose.prod.yml up -d

# Se production logs
docker-compose -f docker-compose.prod.yml logs -f

# Stop production version
docker-compose -f docker-compose.prod.yml down
```

### Docker Commands
```bash
# Build image manuelt
docker build -t videnstjek-blazor ./Blazor

# Kør container manuelt
docker run -p 8067:8067 videnstjek-blazor

# Se kørende containere
docker ps

# Se container logs
docker logs <container-id>
```

## 🌐 Adgang

Efter deployment er applikationen tilgængelig på:
- **Local**: http://localhost:8067
- **Server**: http://your-server-ip:8067

## 📊 Health Checks

Applikationen inkluderer health check endpoint:
- **URL**: `/health`
- **Interval**: 30 sekunder
- **Timeout**: 10 sekunder
- **Retries**: 3

## 🔒 Sikkerhed

### Production Anbefalinger
- Brug `docker-compose.prod.yml` for production
- Sæt resource limits
- Aktiver restart policies
- Brug read-only volumes hvor muligt
- Overvåg container logs

### Miljøvariabler
```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8067
```

## 📈 Monitoring

### Container Status
```bash
# Se container status
docker-compose ps

# Se resource usage
docker stats

# Se container info
docker inspect videnstjek-blazor
```

### Logs
```bash
# Se alle logs
docker-compose logs

# Følg logs i real-time
docker-compose logs -f

# Se logs for specifik service
docker-compose logs videnstjek-blazor
```

## 🚨 Troubleshooting

### Container starter ikke
```bash
# Se container logs
docker-compose logs videnstjek-blazor

# Tjek container status
docker-compose ps

# Restart service
docker-compose restart videnstjek-blazor
```

### Port allerede i brug
```bash
# Tjek hvilke processer bruger port 8067
netstat -an | grep 8067

# Stop eksisterende container
docker-compose down

# Start igen
docker-compose up -d
```

### Build fejler
```bash
# Rens Docker cache
docker system prune -a

# Build uden cache
docker-compose build --no-cache

# Tjek Dockerfile syntax
docker build --dry-run ./Blazor
```

## 🔄 Updates

### Opdater applikationen
```bash
# Stop eksisterende container
docker-compose down

# Pull nyeste kode
git pull

# Build og start igen
docker-compose up --build -d
```

### Opdater Docker image
```bash
# Pull nyeste base images
docker-compose pull

# Rebuild med nye base images
docker-compose build --pull

# Start opdateret version
docker-compose up -d
```

## 📝 Miljø Konfiguration

### Development
- Hot reload aktiveret
- Debug information inkluderet
- Volume mounting for live updates

### Production
- Optimized builds
- Resource limits
- Health checks
- Restart policies
- Security hardening

## 🤝 Support

Hvis du oplever problemer med Docker deployment:
1. Tjek container logs
2. Verificer port konfiguration
3. Tjek Docker daemon status
4. Se troubleshooting sektion ovenfor

---

**Videnstjek** - Docker deployment guide 🐳✨
