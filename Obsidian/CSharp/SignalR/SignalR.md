# SignalR - Real-time Web Communication

## Hvad er SignalR?

SignalR er et .NET bibliotek, der gør det nemt at implementere real-time web funktionalitet i .NET applikationer. Det gør det muligt at sende data fra server til klienter i realtid uden at klienten behøver at spørge serveren om nye data.

## Hvad kan SignalR?

### 1. **Real-time Kommunikation**
- **Server-to-Client Push**: Serveren kan sende data til alle forbundne klienter øjeblikkeligt
- **Client-to-Server**: Klienter kan sende beskeder til serveren
- **Bidirektionel Kommunikation**: To-vejs kommunikation mellem server og klienter

### 2. **Automatisk Transport Valg**
SignalR vælger automatisk den bedste transport metode:
- **WebSockets** (bedste performance)
- **Server-Sent Events** (fallback)
- **Long Polling** (fallback for ældre browsere)

### 3. **Connection Management**
- **Automatisk Reconnection**: Genopretter forbindelse automatisk ved netværksproblemer
- **Connection Groups**: Organiser klienter i grupper for målrettet kommunikation
- **User Management**: Håndter individuelle brugerforbindelser

### 4. **Skalering**
- **Backplane Support**: Redis, SQL Server, Azure Service Bus
- **Load Balancing**: Distribuer forbindelser på tværs af flere servere
- **Horizontal Scaling**: Skaler ud til flere servere

## Use Cases

### **Chat Applikationer**
```csharp
// Eksempel: Chat hub
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

### **Live Dashboards**
- Real-time data opdateringer
- Live statistikker
- System overvågning

### **Collaborative Tools**
- Google Docs-lignende redigering
- Live cursor positioner
- Real-time notifikationer

### **Gaming**
- Multiplayer spil
- Live score opdateringer
- Real-time spil events

### **Trading/Financial Apps**
- Live kurser
- Real-time handelsdata
- Market updates

## SignalR vs. Alternativer

### **Hvad er Alternativerne?**

#### **WebSockets**
WebSockets er en protokol der etablerer en persistent, bidirektionel forbindelse mellem klient og server over en enkelt TCP forbindelse.

**Hvordan fungerer det:**
- Etablerer en persistent forbindelse via HTTP handshake
- Kommunikation sker direkte over TCP
- Lav latency og høj performance
- Kræver manuel implementering af connection management

**Eksempel:**
```javascript
const socket = new WebSocket('ws://localhost:5000/ws');
socket.onmessage = function(event) {
    console.log('Modtaget:', event.data);
};
socket.send('Hej fra klient!');
```

#### **Server-Sent Events (SSE)**
Server-Sent Events er en web standard der gør det muligt for en server at sende data til en klient automatisk.

**Hvordan fungerer det:**
- Bruger standard HTTP forbindelse
- Kun server → klient kommunikation
- Automatisk reconnection
- Simpel implementering

**Eksempel:**
```javascript
const eventSource = new EventSource('/events');
eventSource.onmessage = function(event) {
    console.log('Modtaget:', event.data);
};
```

#### **Long Polling**
Long Polling er en teknik hvor klienten sender en HTTP request og serveren holder forbindelsen åben indtil der er data at sende.

**Hvordan fungerer det:**
- Klient sender request og venter på svar
- Server holder forbindelsen åben indtil data er tilgængelig
- Klient sender ny request efter modtagelse
- Simulerer real-time kommunikation

**Eksempel:**
```javascript
function longPoll() {
    fetch('/poll')
        .then(response => response.json())
        .then(data => {
            console.log('Modtaget:', data);
            longPoll(); // Send ny request
        });
}
```

### **Sammenligningstabel**

| Feature | SignalR | WebSockets | Server-Sent Events | Long Polling |
|---------|---------|------------|-------------------|--------------|
| Real-time | ✅ | ✅ | ✅ (Server→Client) | ❌ |
| Bidirektionel | ✅ | ✅ | ❌ | ❌ |
| Auto Fallback | ✅ | ❌ | ❌ | ❌ |
| .NET Integration | ✅ | ⚠️ | ⚠️ | ⚠️ |
| Skalering | ✅ | ⚠️ | ⚠️ | ❌ |

### Detaljeret Forklaring af Begreberne

#### **Real-time Kommunikation**
- **SignalR**: ✅ Fuldt real-time med automatisk optimering
- **WebSockets**: ✅ Lav latency, direkte TCP forbindelse
- **Server-Sent Events**: ✅ Real-time, men kun server → klient
- **Long Polling**: ❌ Simulerer real-time, men med delay

#### **Bidirektionel Kommunikation**
- **SignalR**: ✅ Server ↔ Klient i begge retninger
- **WebSockets**: ✅ Fuldt bidirektionel kommunikation
- **Server-Sent Events**: ❌ Kun server → klient (one-way)
- **Long Polling**: ❌ Kun server → klient (one-way)

#### **Auto Fallback**
- **SignalR**: ✅ Vælger automatisk bedste transport:
  - WebSockets → Server-Sent Events → Long Polling
- **WebSockets**: ❌ Kræver manuel implementering af fallbacks
- **Server-Sent Events**: ❌ Ingen automatisk fallback
- **Long Polling**: ❌ Ingen automatisk fallback

#### **.NET Integration**
- **SignalR**: ✅ Native .NET bibliotek med:
  - Dependency Injection support
  - Middleware pipeline integration
  - Type-safe hubs
  - Built-in authentication/authorization
- **WebSockets**: ⚠️ Kræver manuel implementering:
  - Custom WebSocket handlers
  - Manual connection management
  - Custom serialization
- **Server-Sent Events**: ⚠️ Kræver manuel implementering:
  - Custom HTTP endpoints
  - Manual event streaming
- **Long Polling**: ⚠️ Kræver manuel implementering:
  - Custom polling logic
  - Manual timeout handling

#### **Skalering**
- **SignalR**: ✅ Indbygget skalering med:
  - Redis backplane
  - SQL Server backplane
  - Azure Service Bus
  - Load balancer support
- **WebSockets**: ⚠️ Kræver custom skalering:
  - Sticky sessions på load balancer
  - Custom message broadcasting
  - Manual connection state management
- **Server-Sent Events**: ⚠️ Begrænset skalering:
  - Sticky sessions nødvendige
  - Custom broadcasting logic
- **Long Polling**: ❌ Dårlig skalering:
  - Høj server load
  - Connection limits
  - Ikke egnet til mange samtidige forbindelser

### **Praktiske Eksempler**

#### **SignalR - Chat Applikation**
```csharp
// Automatisk skalering med Redis
services.AddSignalR().AddRedis("connectionString");

// Type-safe hub
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

#### **WebSockets - Manuel Implementering**
```csharp
// Kræver manuel implementering
app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            // Manuel connection management
        }
    }
});
```

#### **Server-Sent Events - Manuel Implementering**
```csharp
// Kræver custom endpoint
app.MapGet("/events", async (HttpContext context) =>
{
    context.Response.Headers.Add("Content-Type", "text/event-stream");
    // Manuel event streaming
});
```

### **Hvornår Vælge Hvad?**

#### **Vælg SignalR når:**
- Du vil have real-time funktionalitet hurtigt
- Du bruger .NET økosystemet
- Du har brug for skalering
- Du vil have robust forbindelseshåndtering

#### **Vælg WebSockets når:**
- Du har brug for maksimal performance
- Du vil have fuld kontrol over forbindelsen
- Du ikke bruger .NET (f.eks. Node.js, Python)

#### **Vælg Server-Sent Events når:**
- Du kun har brug for server → klient kommunikation
- Du vil have enkel implementering
- Performance ikke er kritisk

#### **Vælg Long Polling når:**
- Du har ældre browser support
- Du har meget simpel real-time behov
- Du ikke kan bruge WebSockets

## Hvordan fungerer det?

### 1. **Hub Pattern**
```csharp
public class MyHub : Hub
{
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
    
    public async Task SendToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }
}
```

### 2. **Client-side JavaScript**
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start().then(function () {
    console.log("SignalR Connected");
});

connection.on("ReceiveMessage", function (user, message) {
    // Håndter modtaget besked
});
```

### 3. **Server Configuration**
```csharp
// Program.cs
builder.Services.AddSignalR();

app.MapHub<ChatHub>("/chathub");
```

## Fordele ved SignalR

1. **Enkel Implementation**: Minimal kode for real-time funktionalitet
2. **Automatisk Transport**: Vælger bedste transport automatisk
3. **Robust**: Håndterer forbindelsesproblemer automatisk
4. **Skalerbar**: Understøtter load balancing og backplane
5. **Cross-platform**: Virker på alle .NET platforme
6. **Type Safety**: Stærkt typet med C# generics

## Ulemper

1. **Microsoft-specifik**: Primært for .NET økosystemet
2. **Memory Usage**: Kan bruge meget hukommelse med mange forbindelser
3. **Complexity**: Kan blive komplekst ved høj skala
4. **Learning Curve**: Kræver forståelse af hub pattern

## Praktisk Implementering - Ticket Support System

### Vores Implementering

I vores H2 projekt har vi implementeret et komplet ticket support system med SignalR til real-time kommunikation mellem brugere og support medarbejdere.

#### **API Side - TicketHub**

```csharp
// H2-MAGS/API/Hubs/TicketHub.cs
public class TicketHub : Hub
{
    // Tilslut til ticket chat
    public async Task JoinTicketGroup(string ticketId)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Context.ConnectionId;
        var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value ?? "User";
        var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Demo User";

        // Valider adgang til ticket
        if (!await ValidateTicketAccess(ticketId, userId, userRole))
        {
            await Clients.Caller.SendAsync("Error", "Du har ikke adgang til denne ticket");
            return;
        }

        // Tilslut til ticket gruppe
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Ticket_{ticketId}");
        
        // Notificer andre i gruppen
        await Clients.Group($"Ticket_{ticketId}").SendAsync("UserJoined", new
        {
            Username = username,
            UserId = userId,
            Timestamp = DateTime.UtcNow
        });
    }

    // Send besked til ticket chat
    public async Task SendMessage(string ticketId, string message, bool isInternal = false)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Context.ConnectionId;
        var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Demo User";
        var userRole = Context.User?.FindFirst(ClaimTypes.Role)?.Value ?? "User";

        // Send besked til alle i ticket gruppen
        await Clients.Group($"Ticket_{ticketId}").SendAsync("MessageReceived", 
            username, message, isInternal, DateTime.UtcNow);
    }
}
```

#### **Blazor Client Side - TicketSignalRService**

```csharp
// H2-MAGS/Blazor/Services/TicketSignalRService.cs
public class TicketSignalRService : INotifyPropertyChanged, IAsyncDisposable
{
    private HubConnection? _hubConnection;
    private readonly string _hubUrl;

    public async Task InitializeAsync(string token)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_hubUrl, options =>
            {
                // For demo mode, skip JWT authentication
                if (token != "demo-token")
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                }
            })
            .WithAutomaticReconnect()
            .Build();

        // Registrer event handlers
        RegisterEventHandlers();
        await _hubConnection.StartAsync();
    }

    public async Task JoinTicketAsync(string ticketId, string userId, string username, string userRole)
    {
        await _hubConnection.InvokeAsync("JoinTicketGroup", ticketId);
    }

    public async Task SendMessageAsync(string message, bool isInternal = false)
    {
        if (CurrentTicketId != null)
        {
            await _hubConnection.InvokeAsync("SendMessage", CurrentTicketId, message, isInternal);
        }
    }
}
```

#### **Blazor UI - TicketChat Component**

```razor
@* H2-MAGS/Blazor/Components/TicketChat.razor *@
<div class="ticket-chat-container">
    <div class="chat-header">
        <h4>Ticket Chat - @TicketNumber</h4>
        <div class="connection-status">
            @if (SignalRService.IsConnected)
            {
                <span class="status-connected">🟢 Forbundet</span>
            }
            else
            {
                <span class="status-disconnected">🔴 Ikke forbundet</span>
            }
        </div>
    </div>

    <div class="chat-messages">
        @foreach (var message in Messages)
        {
            <div class="message @(message.IsFromCurrentUser ? "own-message" : "other-message")">
                <div class="message-header">
                    <strong>@message.AuthorName</strong>
                    <span class="message-time">@message.Timestamp.ToString("HH:mm")</span>
                    @if (message.IsInternal)
                    {
                        <span class="internal-badge">Intern</span>
                    }
                </div>
                <div class="message-content">@message.Message</div>
            </div>
        }
    </div>
</div>
```

### **Konfiguration**

#### **API Program.cs**
```csharp
// Tilføj SignalR til services
builder.Services.AddSignalR();

// Map SignalR Hub med transport konfiguration
app.MapHub<TicketHub>("/tickethub", options =>
{
    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
});
```

#### **Blazor Program.cs**
```csharp
// Registrer TicketSignalRService
builder.Services.AddScoped<TicketSignalRService>();
```

### **Funktioner i vores implementering**

1. **Real-time Chat**: Live messaging mellem brugere og support
2. **Gruppe Management**: Brugere tilslutter specifikke ticket chats
3. **Authentication**: JWT token baseret authentication (med demo mode)
4. **Role-based Access**: Forskellige rettigheder for brugere vs. support
5. **Internal Messages**: Support kan sende interne beskeder
6. **Typing Indicators**: Viser når brugere skriver
7. **Auto Reconnect**: Automatisk genopretning af forbindelse
8. **Connection Status**: Viser forbindelsesstatus i UI

### **Transport Protokoller**

Vores implementering bruger:
- **WebSockets** (primær transport)
- **Long Polling** (fallback for ældre browsere)

### **Skalering**

For fremtidig skalering kan vi tilføje:
- Redis backplane for load balancing
- SQL Server backplane for persistence
- Azure Service Bus for cloud skalering

## Konklusion

SignalR er et kraftfuldt værktøj til real-time web applikationer i .NET. Det gør det nemt at implementere funktionalitet som chat, live dashboards, og collaborative tools uden at skulle håndtere kompleksiteten ved WebSockets eller andre transport protokoller manuelt.

**Vores praktiske implementering viser:**
- Komplet ticket support system med real-time chat
- JWT authentication integration
- Role-based access control
- Robust error handling og reconnection
- Clean separation mellem API og Blazor client

Det er ideelt til:
- Chat applikationer ✅ (Vores ticket chat)
- Live dashboards
- Collaborative tools
- Gaming
- Financial/trading apps
- Eventuelt alle applikationer der kræver real-time kommunikation
