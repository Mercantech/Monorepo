# SignalR vs. HTTPS/RESTful API'er

## Hvornår bruger vi hvad?

Som udviklere har vi mange forskellige måder at kommunikere på tværs af netværket. To af de mest almindelige er **SignalR** (real-time) og **HTTPS/RESTful API'er** (request-response). Men hvornår skal vi bruge hvad?

## HTTPS/RESTful API'er - Den Traditionelle Måde

### Hvad er RESTful API'er?

REST (Representational State Transfer) er en arkitektur stil der bruger HTTP protokollen til kommunikation mellem klient og server.

**Grundlæggende principper:**
- **Stateless**: Hver request indeholder al nødvendig information
- **Client-Server**: Klar separation mellem klient og server
- **Cacheable**: Responses kan caches
- **Uniform Interface**: Konsistent interface design

### Hvordan fungerer det?

```csharp
// Klient sender request
GET /api/users/123
POST /api/users
PUT /api/users/123
DELETE /api/users/123

// Server svarer med data
{
  "id": 123,
  "name": "John Doe",
  "email": "john@example.com"
}
```

### Eksempel - RESTful User API

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserRequest request)
    {
        var user = await _userService.CreateAsync(request);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
```

## SignalR - Real-time Kommunikation

### Hvad er SignalR?

SignalR er et .NET bibliotek der gør det nemt at implementere real-time web funktionalitet.

```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

## Detaljeret Sammenligning

### **1. Kommunikationsmønster**

| Aspekt | HTTPS/RESTful | SignalR |
|--------|---------------|---------|
| **Mønster** | Request-Response | Real-time Push |
| **Initiativ** | Klient starter altid | Server kan pushe data |
| **Forbindelse** | Stateless (åbn/luk) | Persistent forbindelse |
| **Data Flow** | Klient → Server → Klient | Server ↔ Klient |

#### **HTTPS/RESTful Eksempel:**
```javascript
// Klient skal spørge efter data
async function loadMessages() {
    const response = await fetch('/api/messages');
    const messages = await response.json();
    displayMessages(messages);
}

// Kalder hver 5. sekund for nye beskeder
setInterval(loadMessages, 5000);
```

#### **SignalR Eksempel:**
```javascript
// Server pusher data automatisk
connection.on("ReceiveMessage", function (user, message) {
    displayMessage(user, message);
});
// Ingen polling nødvendig!
```

### **2. Performance og Effektivitet**

| Aspekt | HTTPS/RESTful | SignalR |
|--------|---------------|---------|
| **Server Load** | Høj (mange requests) | Lav (persistent forbindelser) |
| **Network Traffic** | Høj (polling) | Lav (kun når nødvendigt) |
| **Latency** | Høj (polling interval) | Lav (øjeblikkelig) |
| **Resource Usage** | Høj CPU/Memory | Lav CPU/Memory |

#### **Polling Problem:**
```javascript
// Dårlig performance - polling hver sekund
setInterval(async () => {
    const response = await fetch('/api/notifications');
    const notifications = await response.json();
    updateUI(notifications);
}, 1000); // 3600 requests per time!
```

#### **SignalR Løsning:**
```javascript
// Optimal performance - kun når nødvendigt
connection.on("NotificationReceived", function (notification) {
    updateUI(notification);
}); // 0 unødvendige requests!
```

### **3. Use Cases - Hvornår Bruger Vi Hvad?**

#### **Brug HTTPS/RESTful når:**

**✅ Data Håndtering (CRUD)**
```csharp
// Perfekt til standard data operationer
GET    /api/users          // Hent alle brugere
GET    /api/users/123      // Hent specifik bruger
POST   /api/users          // Opret bruger
PUT    /api/users/123      // Opdater bruger
DELETE /api/users/123      // Slet bruger
```

**✅ Formulare og Data Indsendelse**
```csharp
[HttpPost]
public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
{
    // Valider data
    if (!ModelState.IsValid) return BadRequest();
    
    // Gem i database
    var order = await _orderService.CreateAsync(request);
    
    // Returner resultat
    return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
}
```

**✅ File Upload/Download**
```csharp
[HttpPost("upload")]
public async Task<IActionResult> UploadFile(IFormFile file)
{
    // Upload logik
    return Ok(new { message = "File uploaded successfully" });
}

[HttpGet("download/{id}")]
public async Task<IActionResult> DownloadFile(int id)
{
    // Download logik
    return File(fileBytes, "application/octet-stream", fileName);
}
```

**✅ Authentication/Authorization**
```csharp
[HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
{
    var result = await _authService.LoginAsync(request);
    if (result.Success)
    {
        return Ok(new { token = result.Token });
    }
    return Unauthorized();
}
```

#### **Brug SignalR når:**

**✅ Real-time Updates**
```csharp
// Live data opdateringer
public async Task UpdateStockPrice(string symbol, decimal price)
{
    await Clients.All.SendAsync("StockPriceUpdated", symbol, price);
}
```

**✅ Chat og Messaging**
```csharp
// Instant messaging
public async Task SendMessage(string user, string message)
{
    await Clients.All.SendAsync("ReceiveMessage", user, message);
}
```

**✅ Live Dashboards**
```csharp
// Real-time statistikker
public async Task UpdateDashboardStats(StatsData stats)
{
    await Clients.All.SendAsync("StatsUpdated", stats);
}
```

**✅ Collaborative Features**
```csharp
// Live cursor positioner
public async Task UpdateCursorPosition(string userId, int x, int y)
{
    await Clients.Others.SendAsync("CursorMoved", userId, x, y);
}
```

### **4. Kompleksitet og Implementering**

| Aspekt | HTTPS/RESTful | SignalR |
|--------|---------------|---------|
| **Læringskurve** | Lav (standard HTTP) | Medium (hub pattern) |
| **Setup** | Simpel | Mere kompleks |
| **Error Handling** | Standard HTTP | Custom implementation |
| **Testing** | Nem (unit tests) | Sværere (integration tests) |
| **Debugging** | Nem (HTTP logs) | Sværere (real-time) |

#### **HTTPS/RESTful - Simpel Implementering:**
```csharp
// Simpel controller - nem at forstå
[HttpGet("{id}")]
public async Task<ActionResult<User>> GetUser(int id)
{
    return await _userService.GetByIdAsync(id);
}
```

#### **SignalR - Mere Kompleks:**
```csharp
// Kræver forståelse af hub pattern
public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // Connection management
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Cleanup logic
        await base.OnDisconnectedAsync(exception);
    }
}
```

### **5. Skalering og Performance**

| Aspekt | HTTPS/RESTful | SignalR |
|--------|---------------|---------|
| **Stateless** | ✅ Nem at skale | ❌ Kræver session management |
| **Load Balancing** | ✅ Standard HTTP | ⚠️ Kræver sticky sessions |
| **Caching** | ✅ HTTP caching | ❌ Ingen standard caching |
| **CDN Support** | ✅ Fuldt understøttet | ❌ Begrænset support |

#### **HTTPS/RESTful Skalering:**
```csharp
// Nem at skale - stateless
[HttpGet("products")]
public async Task<ActionResult<List<Product>>> GetProducts()
{
    // Kan køre på flere servere uden problemer
    return await _productService.GetAllAsync();
}
```

#### **SignalR Skalering:**
```csharp
// Kræver backplane for skalering
services.AddSignalR().AddRedis("connectionString");

// Ellers virker det kun på en server
```

### **6. Fejlhåndtering**

| Aspekt | HTTPS/RESTful | SignalR |
|--------|---------------|---------|
| **HTTP Status Codes** | ✅ Standardiseret | ❌ Custom error handling |
| **Retry Logic** | ✅ Standard HTTP | ⚠️ Custom implementation |
| **Error Messages** | ✅ Standard format | ⚠️ Custom format |
| **Logging** | ✅ Standard HTTP logs | ⚠️ Custom logging |

#### **HTTPS/RESTful Error Handling:**
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<User>> GetUser(int id)
{
    try
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error getting user {UserId}", id);
        return StatusCode(500, "Internal server error");
    }
}
```

#### **SignalR Error Handling:**
```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        try
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("Error", "Failed to send message");
            _logger.LogError(ex, "Error sending message");
        }
    }
}
```

## Praktiske Eksempler fra vores Projekter

### **H2 Projekt - Ticket Support System**

#### **HTTPS/RESTful API'er bruges til:**
```csharp
// Standard CRUD operationer
[HttpGet("tickets")]
public async Task<ActionResult<List<Ticket>>> GetTickets()
{
    return await _ticketService.GetAllAsync();
}

[HttpPost("tickets")]
public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketRequest request)
{
    var ticket = await _ticketService.CreateAsync(request);
    return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
}

[HttpPut("tickets/{id}/status")]
public async Task<IActionResult> UpdateTicketStatus(int id, UpdateStatusRequest request)
{
    await _ticketService.UpdateStatusAsync(id, request.Status);
    return NoContent();
}
```

#### **SignalR bruges til:**
```csharp
// Real-time chat funktionalitet
public class TicketHub : Hub
{
    public async Task JoinTicketGroup(string ticketId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Ticket_{ticketId}");
    }

    public async Task SendMessage(string ticketId, string message)
    {
        await Clients.Group($"Ticket_{ticketId}").SendAsync("MessageReceived", message);
    }
}
```

### **Hvorfor denne opdeling?**

1. **Ticket Management** (HTTPS/RESTful):
   - Opret, læs, opdater, slet tickets
   - Standard data operationer
   - Stateless - nem at skale
   - Standard HTTP error handling

2. **Chat Communication** (SignalR):
   - Real-time messaging
   - Instant opdateringer
   - Persistent forbindelser
   - Live kommunikation

## Konklusion - Hvornår Bruger Vi Hvad?

### **Brug HTTPS/RESTful API'er når:**
- ✅ Du håndterer standard data (CRUD operationer)
- ✅ Du har brug for stateless kommunikation
- ✅ Du vil have nem skalering
- ✅ Du har brug for standard HTTP funktioner (caching, status codes)
- ✅ Du bygger traditionelle web applikationer
- ✅ Du har brug for nem testing og debugging

### **Brug SignalR når:**
- ✅ Du har brug for real-time funktionalitet
- ✅ Du bygger chat applikationer
- ✅ Du har live dashboards eller statistikker
- ✅ Du bygger collaborative tools
- ✅ Du har brug for server-to-client push notifikationer
- ✅ Performance er kritisk (ingen polling)

### **Husk:**
- **HTTPS/RESTful** er ikke "gammeldags" - det er stadig standarden for de fleste web applikationer
- **SignalR** er et specialiseret værktøj til real-time funktionalitet
- **Brug det rigtige værktøj til det rigtige job** - ikke alt skal være real-time
- **Kombiner begge** - de fleste moderne applikationer bruger både RESTful API'er og SignalR

### **Eksempel - Komplet Applikation:**
```csharp
// RESTful API til data håndtering
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet] public async Task<ActionResult<List<Product>>> GetProducts() { }
    [HttpPost] public async Task<ActionResult<Product>> CreateProduct() { }
}

// SignalR til real-time funktionalitet
public class NotificationHub : Hub
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("NotificationReceived", message);
    }
}
```

**Resultatet:** En applikation der både håndterer data effektivt (RESTful) og leverer real-time funktionalitet (SignalR) hvor det giver mening.
