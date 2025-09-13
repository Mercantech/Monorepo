# 🚀 .NET Aspire - Komplet Guide

> **.NET Aspire** er Microsoft's nye cloud-native orkestrerings-stack til .NET applikationer. Den giver dig værktøjer til at bygge, deploye og overvåge distribueret .NET applikationer med indbygget telemetry, service discovery og resilience.

## 📋 Indholdsfortegnelse

- [🎯 Hvad er .NET Aspire?](#-hvad-er-net-aspire)
- [🏗️ Arkitektur og Komponenter](#️-arkitektur-og-komponenter)
- [📊 Telemetry og Observability](#-telemetry-og-observability)
- [🗄️ Database Services](#️-database-services)
- [⚡ Caching med Redis](#-caching-med-redis)
- [🔧 Service Discovery](#-service-discovery)
- [🛡️ Resilience og Health Checks](#️-resilience-og-health-checks)
- [🚀 Setup og Konfiguration](#-setup-og-konfiguration)
- [📈 Monitoring og Debugging](#-monitoring-og-debugging)
- [🎯 Best Practices](#-best-practices)
- [🔍 Fejlfinding](#-fejlfinding)

---

## 🎯 Hvad er .NET Aspire?

**.NET Aspire** er en opinionated, cloud-ready stack til .NET der gør det nemt at bygge distribueret applikationer med:

- **Service Discovery** - Automatisk registrering og opdagelse af services
- **Telemetry** - Indbygget logging, metrics og tracing med OpenTelemetry
- **Resilience** - Automatisk retry, circuit breaker og timeout patterns
- **Health Checks** - Overvågning af service sundhed
- **Configuration** - Centraliseret konfiguration management
- **Orchestration** - Nem deployment og scaling

### Hvorfor Aspire?

| Problem | Traditionel .NET | Med Aspire |
|---------|------------------|------------|
| **Service Discovery** | Manuel konfiguration | Automatisk |
| **Telemetry** | Kompleks setup | Indbygget |
| **Resilience** | Custom implementation | Standard patterns |
| **Health Checks** | Manuel implementering | Automatisk |
| **Configuration** | Scattered config | Centraliseret |

---

## 🏗️ Arkitektur og Komponenter

### Aspire Stack Overblik

```
┌─────────────────────────────────────────────────────────────┐
│                    .NET Aspire Stack                       │
├─────────────────────────────────────────────────────────────┤
│  App Host (Orchestrator)                                   │
│  ├── Service Discovery                                      │
│  ├── Configuration Management                              │
│  └── Health Monitoring                                     │
├─────────────────────────────────────────────────────────────┤
│  Service Defaults (Shared)                                 │
│  ├── OpenTelemetry Integration                             │
│  ├── Resilience Patterns                                   │
│  ├── Health Checks                                         │
│  └── Service Discovery Client                              │
├─────────────────────────────────────────────────────────────┤
│  Services (Your Apps)                                      │
│  ├── API Services                                          │
│  ├── Frontend Apps                                         │
│  ├── Background Services                                   │
│  └── Database Services                                     │
└─────────────────────────────────────────────────────────────┘
```

### Hovedkomponenter

#### 1. **App Host** 🎛️
Orkestrerer hele applikationen og håndterer service discovery.

```csharp
// H2-Projekt.AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

// Tilføj services
var apiService = builder.AddProject<Projects.API>("apiservice");
var redis = builder.AddRedis("redis");
var postgres = builder.AddPostgreSQL("postgres");

// Konfigurer dependencies
builder
    .AddProject<Projects.Blazor>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(redis)
    .WithReference(postgres)
    .WaitFor(apiService);

builder.Build().Run();
```

#### 2. **Service Defaults** ⚙️
Deler konfiguration og telemetry mellem alle services.

```csharp
// H2-Projekt.ServiceDefaults/Extensions.cs
public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) 
    where TBuilder : IHostApplicationBuilder
{
    builder.ConfigureOpenTelemetry();
    builder.AddDefaultHealthChecks();
    builder.Services.AddServiceDiscovery();
    
    builder.Services.ConfigureHttpClientDefaults(http =>
    {
        http.AddStandardResilienceHandler();
        http.AddServiceDiscovery();
    });
    
    return builder;
}
```

#### 3. **Services** 🔧
Dine faktiske applikationer der bruger Aspire.

```csharp
// API/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Tilføj Aspire service defaults
builder.AddServiceDefaults();

// Tilføj dine services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurer pipeline
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
```

---

## 📊 Telemetry og Observability

### OpenTelemetry Integration

Aspire bruger OpenTelemetry til at samle telemetry data fra alle services.

#### Metrics (Målinger)
```csharp
// Automatisk instrumentation
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation();
    });
```

#### Tracing (Sporing)
```csharp
// Automatisk tracing
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSource(builder.Environment.ApplicationName)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
    });
```

#### Logging
```csharp
// Struktureret logging
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = true;
});
```

### Telemetry Data Types

#### 1. **Metrics** 📈
- **Request Rate** - Antal requests per sekund
- **Response Time** - Gennemsnitlig response tid
- **Error Rate** - Procent af fejlede requests
- **Memory Usage** - Hukommelsesforbrug
- **CPU Usage** - CPU forbrug

#### 2. **Traces** 🔍
- **Request Flow** - Fuld request path gennem services
- **Database Queries** - SQL query performance
- **External Calls** - HTTP calls til eksterne services
- **Custom Activities** - Brugerdefinerede operationer

#### 3. **Logs** 📝
- **Structured Logging** - JSON-formateret logs
- **Correlation IDs** - Spor requests på tværs af services
- **Scoped Information** - Context-specifik information

### Custom Telemetry

```csharp
// Custom metrics
public class CustomMetrics
{
    private readonly Meter _meter;
    private readonly Counter<long> _userRegistrations;
    
    public CustomMetrics(IMeterFactory meterFactory)
    {
        _meter = meterFactory.Create("H2-MAGS.API");
        _userRegistrations = _meter.CreateCounter<long>("user_registrations_total");
    }
    
    public void RecordUserRegistration() => _userRegistrations.Add(1);
}

// Custom activities
using var activity = ActivitySource.StartActivity("ProcessBooking");
activity?.SetTag("booking.id", bookingId);
activity?.SetTag("user.id", userId);
```

---

## 🗄️ Database Services

### PostgreSQL Integration

Aspire giver nem integration med PostgreSQL inklusive pgAdmin.

#### 1. **Tilføj PostgreSQL til App Host**

```csharp
// H2-Projekt.AppHost/Program.cs
var postgres = builder.AddPostgreSQL("postgres")
    .WithPgAdmin(8080) // pgAdmin på port 8080
    .WithDataVolume();

// Tilføj connection string til API
var apiService = builder.AddProject<Projects.API>("apiservice")
    .WithReference(postgres);
```

#### 2. **Konfigurer API til at bruge PostgreSQL**

```csharp
// API/Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Tilføj PostgreSQL
builder.AddNpgsqlDbContext<AppDbContext>("postgres");

var app = builder.Build();
app.Run();
```

#### 3. **Database Context**

```csharp
// API/Data/AppDbContext.cs
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfigurer modeller
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
```

#### 4. **Migrations og Seeding**

```csharp
// API/Program.cs
var app = builder.Build();

// Kør migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    
    // Seed data hvis nødvendigt
    if (!context.Users.Any())
    {
        await SeedDataAsync(context);
    }
}
```

### pgAdmin Integration

pgAdmin er automatisk tilgængelig via Aspire dashboard:

- **URL**: `https://localhost:15000` (når App Host kører)
- **Username**: `admin`
- **Password**: `admin`
- **Database Server**: `postgres:5432`

#### pgAdmin Features
- **Database Management** - Opret, slet, backup databaser
- **Query Editor** - Kør SQL queries direkte
- **Schema Browser** - Udforsk database struktur
- **Performance Monitoring** - Overvåg query performance

---

## ⚡ Caching med Redis

### Redis Integration

Aspire giver nem Redis integration til caching og session management.

#### 1. **Tilføj Redis til App Host**

```csharp
// H2-Projekt.AppHost/Program.cs
var redis = builder.AddRedis("redis")
    .WithRedisCommander(8081); // Redis Commander på port 8081

// Tilføj Redis til API
var apiService = builder.AddProject<Projects.API>("apiservice")
    .WithReference(redis);
```

#### 2. **Konfigurer API til at bruge Redis**

```csharp
// API/Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Tilføj Redis
builder.AddRedis("redis");

// Tilføj caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});

var app = builder.Build();
app.Run();
```

#### 3. **Caching Implementation**

```csharp
// API/Services/CacheService.cs
public class CacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;
    
    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        try
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (cachedValue != null)
            {
                return JsonSerializer.Deserialize<T>(cachedValue);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cache value for key {Key}", key);
        }
        
        return null;
    }
    
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
    {
        try
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions();
            
            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }
            
            await _cache.SetStringAsync(key, serializedValue, options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting cache value for key {Key}", key);
        }
    }
}
```

#### 4. **Brug af Caching i Controllers**

```csharp
// API/Controllers/UsersController.cs
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CacheService _cache;
    private readonly IUserService _userService;
    
    public UsersController(CacheService cache, IUserService userService)
    {
        _cache = cache;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        const string cacheKey = "users:all";
        
        // Prøv at hente fra cache først
        var cachedUsers = await _cache.GetAsync<IEnumerable<User>>(cacheKey);
        if (cachedUsers != null)
        {
            return Ok(cachedUsers);
        }
        
        // Hent fra database hvis ikke i cache
        var users = await _userService.GetAllUsersAsync();
        
        // Gem i cache i 5 minutter
        await _cache.SetAsync(cacheKey, users, TimeSpan.FromMinutes(5));
        
        return Ok(users);
    }
}
```

### Redis Commander

Redis Commander er automatisk tilgængelig via Aspire dashboard:

- **URL**: `https://localhost:15000` (når App Host kører)
- **Features**:
  - **Key Browser** - Udforsk Redis keys
  - **Value Editor** - Rediger Redis values
  - **Memory Usage** - Overvåg Redis memory
  - **Performance Stats** - Redis performance metrics

---

## 🔧 Service Discovery

### Automatisk Service Discovery

Aspire håndterer automatisk service discovery mellem dine services.

#### 1. **HTTP Client Configuration**

```csharp
// API/Services/ExternalApiService.cs
public class ExternalApiService
{
    private readonly HttpClient _httpClient;
    
    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> GetDataAsync()
    {
        // Aspire håndterer automatisk service discovery
        // Du behøver ikke at kende den eksakte URL
        var response = await _httpClient.GetAsync("http://frontend/api/data");
        return await response.Content.ReadAsStringAsync();
    }
}
```

#### 2. **Service Registration**

```csharp
// API/Program.cs
builder.Services.AddHttpClient<ExternalApiService>(client =>
{
    // Aspire konfigurerer automatisk service discovery
    client.BaseAddress = new Uri("http://frontend");
});
```

#### 3. **Configuration**

```csharp
// H2-Projekt.ServiceDefaults/Extensions.cs
builder.Services.ConfigureHttpClientDefaults(http =>
{
    // Tilføj resilience patterns
    http.AddStandardResilienceHandler();
    
    // Tilføj service discovery
    http.AddServiceDiscovery();
});
```

### Service Discovery Features

- **Automatic Registration** - Services registreres automatisk
- **Health-based Routing** - Kun sunde services modtager trafik
- **Load Balancing** - Automatisk load balancing mellem instances
- **Configuration Updates** - Automatisk opdatering ved ændringer

---

## 🛡️ Resilience og Health Checks

### Resilience Patterns

Aspire inkluderer standard resilience patterns:

#### 1. **Retry Policy**
```csharp
// Automatisk retry konfiguration
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler(options =>
    {
        options.Retry.MaxRetryAttempts = 3;
        options.Retry.BackoffType = DelayBackoffType.Exponential;
    });
});
```

#### 2. **Circuit Breaker**
```csharp
// Circuit breaker konfiguration
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler(options =>
    {
        options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
        options.CircuitBreaker.FailureThreshold = 0.5;
        options.CircuitBreaker.MinimumThroughput = 3;
    });
});
```

#### 3. **Timeout**
```csharp
// Timeout konfiguration
builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler(options =>
    {
        options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(30);
    });
});
```

### Health Checks

#### 1. **Default Health Checks**

```csharp
// H2-Projekt.ServiceDefaults/Extensions.cs
public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) 
    where TBuilder : IHostApplicationBuilder
{
    builder.Services.AddHealthChecks()
        .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
    
    return builder;
}
```

#### 2. **Custom Health Checks**

```csharp
// API/HealthChecks/DatabaseHealthCheck.cs
public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;
    
    public DatabaseHealthCheck(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy("Database is accessible");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database is not accessible", ex);
        }
    }
}

// Registrer custom health check
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database")
    .AddCheck<RedisHealthCheck>("redis");
```

#### 3. **Health Check Endpoints**

```csharp
// Automatisk health check endpoints i development
app.MapDefaultEndpoints(); // Inkluderer /health og /alive endpoints
```

---

## 🚀 Setup og Konfiguration

### 1. **Installation**

```bash
# Installer .NET Aspire workload
dotnet workload install aspire

# Opret nyt Aspire projekt
dotnet new aspire-starter -n H2-MAGS
cd H2-MAGS
```

### 2. **Projekt Struktur**

```
H2-MAGS/
├── H2-Projekt.AppHost/           # Orchestrator
│   ├── Program.cs
│   └── H2-Projekt.AppHost.csproj
├── H2-Projekt.ServiceDefaults/   # Shared configuration
│   ├── Extensions.cs
│   └── H2-Projekt.ServiceDefaults.csproj
├── API/                          # Backend service
│   ├── Program.cs
│   ├── Controllers/
│   └── API.csproj
├── Blazor/                       # Frontend service
│   ├── Program.cs
│   └── Blazor.csproj
└── DomainModels/                 # Shared models
    └── DomainModels.csproj
```

### 3. **App Host Konfiguration**

```csharp
// H2-Projekt.AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

// Tilføj database services
var postgres = builder.AddPostgreSQL("postgres")
    .WithPgAdmin(8080)
    .WithDataVolume();

var redis = builder.AddRedis("redis")
    .WithRedisCommander(8081);

// Tilføj API service
var apiService = builder.AddProject<Projects.API>("apiservice")
    .WithReference(postgres)
    .WithReference(redis);

// Tilføj frontend service
builder
    .AddProject<Projects.Blazor>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
```

### 4. **Service Konfiguration**

```csharp
// API/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Tilføj Aspire service defaults
builder.AddServiceDefaults();

// Tilføj dine services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tilføj database
builder.AddNpgsqlDbContext<AppDbContext>("postgres");

// Tilføj caching
builder.AddRedis("redis");

var app = builder.Build();

// Konfigurer pipeline
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
```

### 5. **Environment Variables**

```bash
# .env fil
ASPNETCORE_ENVIRONMENT=Development
OTEL_EXPORTER_OTLP_ENDPOINT=http://localhost:4317
```

---

## 📈 Monitoring og Debugging

### Aspire Dashboard

Aspire dashboard giver et centralt sted at overvåge alle dine services:

- **URL**: `https://localhost:15000` (når App Host kører)
- **Features**:
  - **Service Overview** - Status af alle services
  - **Telemetry Data** - Metrics, traces og logs
  - **Service Dependencies** - Visuelt dependency diagram
  - **Configuration** - Service konfiguration
  - **Logs** - Real-time log viewing

### Telemetry Exporters

#### 1. **OTLP Exporter**

```csharp
// H2-Projekt.ServiceDefaults/Extensions.cs
private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) 
    where TBuilder : IHostApplicationBuilder
{
    var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
    
    if (useOtlpExporter)
    {
        builder.Services.AddOpenTelemetry().UseOtlpExporter();
    }
    
    return builder;
}
```

#### 2. **Azure Monitor Integration**

```csharp
// Uncomment for Azure Monitor
//if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
//{
//    builder.Services.AddOpenTelemetry()
//       .UseAzureMonitor();
//}
```

### Debugging Tips

#### 1. **Log Levels**

```csharp
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

#### 2. **Custom Logging**

```csharp
// Custom logging i services
public class UserService
{
    private readonly ILogger<UserService> _logger;
    
    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }
    
    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        _logger.LogInformation("Creating user with email {Email}", request.Email);
        
        try
        {
            var user = new User { Email = request.Email };
            // ... create user logic
            _logger.LogInformation("User created successfully with ID {UserId}", user.Id);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create user with email {Email}", request.Email);
            throw;
        }
    }
}
```

---

## 🎯 Best Practices

### 1. **Service Design**

```csharp
// ✅ Gode service design patterns
public class UserService
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserService> _logger;
    private readonly CacheService _cache;
    
    public UserService(
        AppDbContext context, 
        ILogger<UserService> logger,
        CacheService cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }
    
    public async Task<User?> GetUserAsync(int id)
    {
        // Prøv cache først
        var cachedUser = await _cache.GetAsync<User>($"user:{id}");
        if (cachedUser != null)
        {
            return cachedUser;
        }
        
        // Hent fra database
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            await _cache.SetAsync($"user:{id}", user, TimeSpan.FromMinutes(5));
        }
        
        return user;
    }
}
```

### 2. **Error Handling**

```csharp
// Global error handling
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        
        var response = new { error = "An internal server error occurred" };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
```

### 3. **Configuration Management**

```csharp
// Strongly typed configuration
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int CommandTimeout { get; set; } = 30;
    public bool EnableRetryOnFailure { get; set; } = true;
}

// Registrer i Program.cs
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));
```

### 4. **Performance Optimization**

```csharp
// Connection pooling
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(3);
        npgsqlOptions.CommandTimeout(30);
    });
});

// Caching strategies
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
});
```

---

## 🔍 Fejlfinding

### Almindelige Problemer

#### 1. **Service Discovery Fejl**
```
Error: Service 'apiservice' not found
```
**Løsning:**
- Tjek at App Host kører
- Verificer service navne matcher
- Tjek network konfiguration

#### 2. **Database Connection Fejl**
```
Error: Unable to connect to PostgreSQL
```
**Løsning:**
- Tjek at PostgreSQL container kører
- Verificer connection string
- Tjek firewall indstillinger

#### 3. **Redis Connection Fejl**
```
Error: Unable to connect to Redis
```
**Løsning:**
- Tjek at Redis container kører
- Verificer Redis konfiguration
- Tjek memory limits

#### 4. **Telemetry Export Fejl**
```
Error: Failed to export telemetry data
```
**Løsning:**
- Tjek OTLP endpoint konfiguration
- Verificer network connectivity
- Tjek exporter konfiguration

### Debug Commands

```bash
# Tjek service status
docker ps

# Se logs
docker logs h2-api-mags25

# Tjek network
docker network ls
docker network inspect h2-network

# Tjek volumes
docker volume ls
```

### Performance Debugging

```csharp
// Performance monitoring
public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        await _next(context);
        
        stopwatch.Stop();
        
        if (stopwatch.ElapsedMilliseconds > 1000)
        {
            _logger.LogWarning("Slow request: {Method} {Path} took {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
```

---

## 🎯 Konklusion

.NET Aspire giver dig en komplet stack til at bygge moderne, cloud-native .NET applikationer med:

- ✅ **Automatisk Service Discovery** - Nem kommunikation mellem services
- ✅ **Indbygget Telemetry** - Komplet observability ud af boksen
- ✅ **Resilience Patterns** - Automatisk retry, circuit breaker og timeout
- ✅ **Database Integration** - Nem PostgreSQL og Redis integration
- ✅ **Health Monitoring** - Automatisk health checks og monitoring
- ✅ **Developer Experience** - Dashboard og debugging værktøjer

### Nøgle Takeaways:

1. **Start Simple** - Begynd med basic setup og tilføj features gradvist
2. **Leverage Defaults** - Brug Aspire's standard konfigurationer
3. **Monitor Everything** - Brug telemetry til at forstå din applikation
4. **Test Resilience** - Test hvordan din app håndterer fejl
5. **Iterate** - Forbedre baseret på telemetry data

**Med .NET Aspire kan du fokusere på forretningslogik i stedet for infrastruktur!** 🚀

---

*Denne guide dækker .NET Aspire v9.0. For opdateringer og nyeste features, besøg [Microsoft's Aspire dokumentation](https://learn.microsoft.com/en-us/dotnet/aspire/)*
