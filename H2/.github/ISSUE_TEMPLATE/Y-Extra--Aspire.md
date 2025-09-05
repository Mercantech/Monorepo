---
name: Ekstra emne - Aspire Infrastructure Setup
about: Opsæt .NET Aspire med PostgreSQL, Redis, admin UI og monitoring
title: 'Ekstra emne - Aspire Infrastructure Setup'
labels: ['ekstra-emne', 'aspire', 'postgresql', 'redis', 'infrastructure', 'monitoring']
assignees: ''
---

## .NET Aspire Infrastructure Setup

- [ ] Opsæt .NET Aspire Host projekt
  - [ ] Opret Aspire.Hosting projekt
  - [ ] Konfigurer ServiceDefaults
  - [ ] Opsæt OpenTelemetry og logging
  - [ ] Konfigurer health checks
- [ ] PostgreSQL Database opsætning
  - [ ] Opret PostgreSQL container med Aspire
  - [ ] Konfigurer database connection strings
  - [ ] Opsæt database migrations
- [ ] PostgreSQL Admin UI (pgAdmin)
  - [ ] Opret pgAdmin container
  - [ ] Konfigurer pgAdmin med database connection
  - [ ] Opsæt sikker adgang til admin UI
  - [ ] Test database administration funktioner
- [ ] Redis Caching Database
  - [ ] Opret Redis container med Aspire
  - [ ] Konfigurer Redis connection
  - [ ] Implementér caching service
  - [ ] Opsæt cache invalidation strategier
- [ ] Redis Admin UI (RedisInsight)
  - [ ] Opret RedisInsight container
  - [ ] Konfigurer RedisInsight med Redis connection
  - [ ] Opsæt monitoring af cache performance
  - [ ] Test cache administration funktioner
- [ ] Monitoring og Observability
  - [ ] Opsæt Grafana for metrics visualization
  - [ ] Konfigurer Prometheus for metrics collection
  - [ ] Opsæt Jaeger for distributed tracing
  - [ ] Implementér custom metrics og dashboards
- [ ] Service Discovery og Load Balancing
  - [ ] Opsæt Consul for service discovery
  - [ ] Konfigurer load balancer (HAProxy/Nginx)
  - [ ] Implementér service health monitoring
  - [ ] Opsæt automatic failover

---
### Eksempel på Aspire Host konfiguration
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add Aspire services
builder.AddServiceDefaults();

// Add PostgreSQL
builder.AddNpgsqlDataSource("postgres", "Host=localhost;Port=5432;Database=h2db;Username=postgres;Password=postgres");

// Add Redis
builder.AddRedis("redis");

// Add custom services
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<IDatabaseService, PostgresDatabaseService>();

var app = builder.Build();

// Configure pipeline
app.MapDefaultEndpoints();

// Add custom endpoints
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();
```

### Eksempel på PostgreSQL opsætning
```csharp
// I Program.cs eller HostExtensions.cs
public static IHostApplicationBuilder AddPostgresDatabase(this IHostApplicationBuilder builder)
{
    builder.AddNpgsqlDataSource("postgres", "Host=localhost;Port=5432;Database=h2db;Username=postgres;Password=postgres");
    
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));
    
    return builder;
}
```

### Eksempel på Redis caching service
```csharp
public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task RemoveAsync(string key);
    Task RemoveByPatternAsync(string pattern);
}

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default(T);
    }
    
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serialized = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serialized, expiry);
    }
}
```

### Docker Compose eksempel for Aspire
```yaml
version: '3.8'

services:
  # PostgreSQL Database
  postgres:
    image: postgres:15-alpine
    container_name: h2-postgres
    environment:
      POSTGRES_DB: h2db
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./init-scripts:/docker-entrypoint-initdb.d

  # pgAdmin for PostgreSQL management
  pgadmin:
    image: dpage/pgadmin4:latest
    container_name: h2-pgadmin
    environment:
      PGADMIN_DEFAULT_EMAIL: admin@h2.local
      PGADMIN_DEFAULT_PASSWORD: admin
    ports:
      - "8080:80"
    depends_on:
      - postgres

  # Redis Cache
  redis:
    image: redis:7-alpine
    container_name: h2-redis
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data

  # RedisInsight for Redis management
  redisinsight:
    image: redislabs/redisinsight:latest
    container_name: h2-redisinsight
    ports:
      - "8001:8001"
    depends_on:
      - redis

  # Grafana for monitoring
  grafana:
    image: grafana/grafana:latest
    container_name: h2-grafana
    environment:
      GF_SECURITY_ADMIN_PASSWORD: admin
    ports:
      - "3000:3000"
    volumes:
      - grafana_data:/var/lib/grafana

  # Prometheus for metrics
  prometheus:
    image: prom/prometheus:latest
    container_name: h2-prometheus
    ports:
      - "9090:9090"
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml

volumes:
  postgres_data:
  redis_data:
  grafana_data:
```

### Eksempel på monitoring konfiguration
```csharp
// I Program.cs
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddEntityFrameworkCoreInstrumentation()
               .AddRedisInstrumentation();
    })
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
               .AddHttpClientInstrumentation()
               .AddRuntimeInstrumentation();
    });
```

### Krav til Aspire setup
- [ ] PostgreSQL database med migration support
- [ ] pgAdmin for database administration
- [ ] Redis cache med performance monitoring
- [ ] RedisInsight for cache management
- [ ] Grafana dashboards for system monitoring
- [ ] Prometheus metrics collection
- [ ] Distributed tracing med Jaeger
- [ ] Health checks for alle services
- [ ] Service discovery og load balancing

### Bonus opgaver
- [ ] Implementér database backup automation
- [ ] Opret custom Grafana dashboards
- [ ] Implementér alerting system
- [ ] Opsæt log aggregation med ELK stack
- [ ] Implementér database performance monitoring
- [ ] Opsæt automated scaling baseret på metrics
- [ ] Implementér disaster recovery procedures
- [ ] Opret infrastructure as code med Terraform

### Performance og sikkerhed
- [ ] Konfigurer SSL/TLS for alle services
- [ ] Opsæt firewall regler
- [ ] Implementér rate limiting
- [ ] Konfigurer database connection pooling
- [ ] Opsæt Redis clustering for high availability
- [ ] Implementér data encryption at rest
- [ ] Opsæt backup og restore procedures
