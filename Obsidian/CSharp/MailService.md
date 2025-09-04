# Mail Service med C# og .NET

> **Relateret til:** [[SignalR]] | [[IT-ServiceManagment]] | [[Cisco CCNA/OSI-Modellen]]

## Oversigt

Mail Service i .NET gør det muligt at sende emails via SMTP (Simple Mail Transfer Protocol). Der er flere tilgange til at implementere email funktionalitet, hver med deres egne fordele og ulemper.

## SMTP Protokollen

SMTP (Simple Mail Transfer Protocol) opererer på **Port 25** (standard) eller **Port 587** (TLS/SSL) og er en del af **Application Layer** i [[OSI-Modellen]]

### **Hvordan SMTP fungerer:**
1. **Client** forbinder til SMTP server
2. **Handshake** mellem client og server
3. **Authentication** (hvis påkrævet)
4. **Mail transfer** - sender recipient, sender, og content
5. **Delivery** til recipient's mail server

## Lokal SMTP Server vs. Cloud-baseret

### **Lokal SMTP Server**

#### **Fordele:**
- **Fuld kontrol** over mail server
- **Ingen eksterne afhængigheder**
- **Gratis** (bortset fra server hardware)
- **Custom konfiguration** mulig
- **Offline funktionalitet**

#### **Ulemper:**
- **Kompleks setup** og vedligeholdelse
- **Sikkerhedsrisici** hvis ikke konfigureret korrekt
- **IP reputation** problemer
- **Spam filtering** udfordringer
- **Delivery rates** kan være lave

#### **Populære lokale SMTP servere:**
- Mailpit (CrossPlatform og generelt anbefalet af MAGS)
- **IIS SMTP** (Windows)
- **Postfix** (Linux)
- **hMailServer** (Windows)
- **MailHog** (Development)

### **Cloud-baserede SMTP (Anbefalet)**

#### **Fordele:**
- **Høj delivery rate** (99%+)
- **Automatisk spam protection**
- **Skalerbar** infrastruktur
- **Professional support**
- **Built-in analytics**
- **Compliance** (GDPR, etc.)

#### **Ulemper:**
- **Månedlige omkostninger**
- **Afhængighed** af tredjepart
- **Rate limits**
- **Custom konfiguration** begrænset

## Implementering i C#

### **1. NuGet Packages**

```xml
<PackageReference Include="MailKit" Version="4.3.0" />
<PackageReference Include="MimeKit" Version="4.3.0" />
```

### **2. Basic Mail Service Implementation**

```csharp
using MailKit.Net.Smtp;
using MimeKit;

public class MailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _username;
    private readonly string _password;

    public MailService(string smtpHost, int smtpPort, string username, string password)
    {
        _smtpHost = smtpHost;
        _smtpPort = smtpPort;
        _username = username;
        _password = password;
        _smtpClient = new SmtpClient();
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sender Name", _username));
        message.To.Add(new MailboxAddress("Recipient", to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = body };

        await _smtpClient.ConnectAsync(_smtpHost, _smtpPort, true);
        await _smtpClient.AuthenticateAsync(_username, _password);
        await _smtpClient.SendAsync(message);
        await _smtpClient.DisconnectAsync(true);
    }
}
```

### **3. Dependency Injection Setup**

```csharp
// Program.cs
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IMailService, MailService>();

// appsettings.json
{
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "EnableSsl": true
  }
}
```

## Cloud-baserede SMTP Tjenester

### **Gmail SMTP (Anbefalet til udvikling)**

```csharp
// Gmail konfiguration
var smtpSettings = new SmtpSettings
{
    Host = "smtp.gmail.com",
    Port = 587,
    Username = "your-email@gmail.com",
    Password = "your-app-password", // Ikke din normale adgangskode!
    EnableSsl = true
};
```

**Setup Gmail:**
1. Aktiver 2-Factor Authentication
2. Generer App Password
3. Brug App Password i koden

### **Outlook/Hotmail SMTP**

```csharp
// Outlook konfiguration
var smtpSettings = new SmtpSettings
{
    Host = "smtp-mail.outlook.com",
    Port = 587,
    Username = "your-email@outlook.com",
    Password = "your-password",
    EnableSsl = true
};
```

### **SendGrid (Professionel løsning)**

```csharp
// SendGrid konfiguration
var smtpSettings = new SmtpSettings
{
    Host = "smtp.sendgrid.net",
    Port = 587,
    Username = "apikey",
    Password = "your-sendgrid-api-key",
    EnableSsl = true
};
```

## Avancerede Features

### **1. HTML Templates**

```csharp
public async Task SendWelcomeEmailAsync(string to, string name)
{
    var template = @"
    <html>
        <body>
            <h1>Velkommen, {0}!</h1>
            <p>Tak for din registrering.</p>
            <p>Dato: {1}</p>
        </body>
    </html>";
    
    var htmlBody = string.Format(template, name, DateTime.Now.ToString("dd/MM/yyyy"));
    await SendEmailAsync(to, "Velkommen!", htmlBody);
}
```

### **2. Attachments**

```csharp
public async Task SendEmailWithAttachmentAsync(string to, string subject, string body, string filePath)
{
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Sender", _username));
    message.To.Add(new MailboxAddress("Recipient", to));
    message.Subject = subject;

    var multipart = new Multipart("mixed");
    multipart.Add(new TextPart("html") { Text = body });
    
    var attachment = new MimePart("application", "pdf")
    {
        Content = new MimeContent(File.OpenRead(filePath)),
        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        ContentTransferEncoding = ContentEncoding.Base64,
        FileName = Path.GetFileName(filePath)
    };
    
    multipart.Add(attachment);
    message.Body = multipart;
    
    // Send email...
}
```

### **3. Bulk Email med Rate Limiting**

```csharp
public async Task SendBulkEmailsAsync(List<string> recipients, string subject, string body)
{
    var semaphore = new SemaphoreSlim(5); // Max 5 samtidige emails
    var tasks = recipients.Select(async recipient =>
    {
        await semaphore.WaitAsync();
        try
        {
            await SendEmailAsync(recipient, subject, body);
            await Task.Delay(1000); // Rate limiting
        }
        finally
        {
            semaphore.Release();
        }
    });
    
    await Task.WhenAll(tasks);
}
```

## Integration med SignalR

For real-time notifikationer om email status, kan du integrere med [[SignalR]]:

```csharp
public class EmailNotificationService
{
    private readonly IMailService _mailService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public async Task SendEmailWithNotificationAsync(string to, string subject, string body)
    {
        try
        {
            await _mailService.SendEmailAsync(to, subject, body);
            await _hubContext.Clients.All.SendAsync("EmailSent", new { 
                To = to, 
                Subject = subject, 
                Status = "Success",
                Timestamp = DateTime.Now 
            });
        }
        catch (Exception ex)
        {
            await _hubContext.Clients.All.SendAsync("EmailFailed", new { 
                To = to, 
                Error = ex.Message,
                Timestamp = DateTime.Now 
            });
        }
    }
}
```

## IT Service Management Integration

I forbindelse med [[IT-ServiceManagment|IT Service Management]] kan email services bruges til:

### **Incident Management**
- Automatiske notifikationer ved system nedetid
- Status updates til påvirkede brugere
- Escalation alerts til support team

### **Change Management**
- Notifikationer om planlagte ændringer
- Approval requests til Change Advisory Board
- Implementation status updates

### **Service Level Management**
- SLA breach alerts
- Performance reports
- Monthly service reviews

## Best Practices

### **Sikkerhed**
- Brug **App Passwords** i stedet for normale passwords
- Implementer **rate limiting** for at undgå spam
- Valider **email addresses** før sending
- Brug **TLS/SSL** for alle SMTP forbindelser

### **Performance**
- Implementer **connection pooling**
- Brug **async/await** patterns
- Cache **SMTP connections** hvor muligt
- Implementer **retry logic** for failed emails

### **Monitoring**
- Log alle email aktiviteter
- Monitor **delivery rates**
- Track **bounce rates**
- Implementer **health checks**

## Konfiguration Eksempler

### **Development (Gmail)**
```json
{
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "dev@yourcompany.com",
    "Password": "app-password",
    "EnableSsl": true
  }
}
```

### **Production (SendGrid)**
```json
{
  "SmtpSettings": {
    "Host": "smtp.sendgrid.net",
    "Port": 587,
    "Username": "apikey",
    "Password": "SG.your-api-key",
    "EnableSsl": true
  }
}
```

## Hotelbooking System - Praktisk Anvendelse

### **Hvorfor Email er Kritisk i Hotelbooking**

Hotelbooking systemer er afhængige af email kommunikation på flere niveauer:

#### **1. Kunde Kommunikation**
- **Bekræftelse** af reservation
- **Check-in** instruktioner
- **Service** notifikationer
- **Feedback** requests efter ophold

#### **2. Interne Processer**
- **Staff** notifikationer om nye bookinger
- **Housekeeping** assignments
- **Maintenance** requests
- **Management** rapporter

#### **3. Business Intelligence**
- **Revenue** tracking
- **Occupancy** alerts
- **Guest** satisfaction surveys
- **Marketing** campaigns

### **Email Workflow i Hotelbooking System**

```csharp
public class HotelBookingEmailService
{
    private readonly IMailService _mailService;
    private readonly IHubContext<NotificationHub> _hubContext;

    // 1. Booking Bekræftelse
    public async Task SendBookingConfirmationAsync(Booking booking)
    {
        var template = @"
        <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2>Booking Bekræftelse</h2>
                <p>Kære {0},</p>
                <p>Tak for din reservation hos {1}!</p>
                
                <div style='background: #f5f5f5; padding: 15px; margin: 10px 0;'>
                    <h3>Reservation Detaljer:</h3>
                    <p><strong>Check-in:</strong> {2}</p>
                    <p><strong>Check-out:</strong> {3}</p>
                    <p><strong>Værelse:</strong> {4}</p>
                    <p><strong>Gæster:</strong> {5}</p>
                    <p><strong>Total pris:</strong> {6} DKK</p>
                </div>
                
                <p>Vi glæder os til at byde dig velkommen!</p>
                <p>Med venlig hilsen,<br>Hotel Team</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, 
            booking.GuestName, 
            "Hotel Copenhagen",
            booking.CheckInDate.ToString("dd/MM/yyyy"),
            booking.CheckOutDate.ToString("dd/MM/yyyy"),
            booking.RoomType,
            booking.GuestCount,
            booking.TotalPrice.ToString("N0"));

        await _mailService.SendEmailAsync(booking.GuestEmail, 
            "Booking Bekræftelse - Hotel Copenhagen", htmlBody);

        // Real-time notifikation til staff
        await _hubContext.Clients.All.SendAsync("NewBooking", new {
            GuestName = booking.GuestName,
            RoomType = booking.RoomType,
            CheckIn = booking.CheckInDate,
            Timestamp = DateTime.Now
        });
    }

    // 2. Check-in Reminder
    public async Task SendCheckInReminderAsync(Booking booking)
    {
        var template = @"
        <html>
            <body>
                <h2>Check-in Reminder</h2>
                <p>Hej {0},</p>
                <p>Dette er en påmindelse om din check-in i morgen kl. 15:00.</p>
                <p><strong>Adresse:</strong> Hotel Copenhagen, København</p>
                <p><strong>Parkering:</strong> Gratis parkering tilgængelig</p>
                <p>Vi glæder os til at se dig!</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, booking.GuestName);
        await _mailService.SendEmailAsync(booking.GuestEmail, 
            "Check-in Reminder - Hotel Copenhagen", htmlBody);
    }

    // 3. Service Notifikationer
    public async Task SendServiceNotificationAsync(string guestEmail, string service, string message)
    {
        var template = @"
        <html>
            <body>
                <h2>Service Notifikation</h2>
                <p>Kære gæst,</p>
                <p><strong>Service:</strong> {0}</p>
                <p><strong>Besked:</strong> {1}</p>
                <p>Kontakt reception hvis du har spørgsmål.</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, service, message);
        await _mailService.SendEmailAsync(guestEmail, 
            "Service Notifikation - Hotel Copenhagen", htmlBody);
    }

    // 4. Feedback Request
    public async Task SendFeedbackRequestAsync(Booking booking)
    {
        var template = @"
        <html>
            <body>
                <h2>Hjælp os med at forbedre vores service</h2>
                <p>Kære {0},</p>
                <p>Tak for dit ophold hos os! Vi håber du havde et godt ophold.</p>
                <p>Din feedback er vigtig for os. Klik på linket nedenfor for at vurdere dit ophold:</p>
                <a href='https://hotel.com/feedback/{1}' style='background: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Giv Feedback</a>
                <p>Tak for din tid!</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, booking.GuestName, booking.BookingId);
        await _mailService.SendEmailAsync(booking.GuestEmail, 
            "Feedback Request - Hotel Copenhagen", htmlBody);
    }
}
```

### **Staff Notifikationer**

```csharp
public class HotelStaffNotificationService
{
    private readonly IMailService _mailService;

    // Housekeeping Assignment
    public async Task NotifyHousekeepingAsync(RoomAssignment assignment)
    {
        var template = @"
        <html>
            <body>
                <h2>Housekeeping Assignment</h2>
                <p><strong>Værelse:</strong> {0}</p>
                <p><strong>Check-out tid:</strong> {1}</p>
                <p><strong>Gæster:</strong> {2}</p>
                <p><strong>Special requests:</strong> {3}</p>
                <p>Venligst forbered værelset til næste gæst.</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, 
            assignment.RoomNumber,
            assignment.CheckOutTime.ToString("HH:mm"),
            assignment.GuestCount,
            assignment.SpecialRequests ?? "Ingen");

        await _mailService.SendEmailAsync("housekeeping@hotel.com", 
            $"Housekeeping Assignment - Værelse {assignment.RoomNumber}", htmlBody);
    }

    // Maintenance Request
    public async Task NotifyMaintenanceAsync(MaintenanceRequest request)
    {
        var template = @"
        <html>
            <body>
                <h2>Maintenance Request</h2>
                <p><strong>Værelse:</strong> {0}</p>
                <p><strong>Problem:</strong> {1}</p>
                <p><strong>Prioritet:</strong> {2}</p>
                <p><strong>Beskrivelse:</strong> {3}</p>
                <p>Venligst håndter denne anmodning snarest muligt.</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, 
            request.RoomNumber,
            request.ProblemType,
            request.Priority,
            request.Description);

        await _mailService.SendEmailAsync("maintenance@hotel.com", 
            $"Maintenance Request - Værelse {request.RoomNumber}", htmlBody);
    }
}
```

### **Integration med IT Service Management**

I forbindelse med [[IT-ServiceManagment|IT Service Management]] kan hotelbooking systemer bruge email til:

#### **Incident Management**
- **System nedetid** notifikationer til gæster
- **Booking system** fejl alerts til IT team
- **Payment** processing fejl notifikationer

#### **Change Management**
- **System updates** notifikationer til staff
- **New features** announcements til gæster
- **Maintenance** windows kommunikation

#### **Service Level Management**
- **Booking response** time monitoring
- **Email delivery** rate tracking
- **Guest satisfaction** metrics

### **Best Practices for Hotelbooking Email**

#### **Timing**
- **Bekræftelse:** Umiddelbart efter booking
- **Check-in reminder:** 24 timer før ankomst
- **Service notifikationer:** Real-time
- **Feedback request:** 24 timer efter check-out

#### **Personalization**
- Brug gæstens navn
- Inkluder booking detaljer
- Tilpas indhold til gæstens sprog
- Referer til specifikke services

#### **Compliance**
- **GDPR** compliance for data håndtering
- **CAN-SPAM** compliance for marketing emails
- **Opt-out** muligheder
- **Data retention** policies

### **Monitoring og Analytics**

```csharp
public class HotelEmailAnalytics
{
    public async Task TrackEmailMetricsAsync(string emailType, string recipient, bool success)
    {
        // Log email metrics for analysis
        var metrics = new EmailMetrics
        {
            EmailType = emailType,
            Recipient = recipient,
            Success = success,
            Timestamp = DateTime.Now,
            HotelId = GetCurrentHotelId()
        };

        await _analyticsService.LogAsync(metrics);
    }

    public async Task GenerateDailyReportAsync()
    {
        var report = await _analyticsService.GetDailyReportAsync();
        
        var template = @"
        <html>
            <body>
                <h2>Daglig Email Rapport - {0}</h2>
                <p><strong>Bekræftelser sendt:</strong> {1}</p>
                <p><strong>Reminders sendt:</strong> {2}</p>
                <p><strong>Feedback requests:</strong> {3}</p>
                <p><strong>Delivery rate:</strong> {4}%</p>
                <p><strong>Open rate:</strong> {5}%</p>
            </body>
        </html>";

        var htmlBody = string.Format(template, 
            DateTime.Now.ToString("dd/MM/yyyy"),
            report.ConfirmationsSent,
            report.RemindersSent,
            report.FeedbackRequests,
            report.DeliveryRate,
            report.OpenRate);

        await _mailService.SendEmailAsync("management@hotel.com", 
            "Daglig Email Rapport", htmlBody);
    }
}
```

## Konklusion

**Anbefaling:** Brug cloud-baserede SMTP tjenester som Gmail, Outlook eller SendGrid i stedet for lokale SMTP servere, da de giver:

- **Højere delivery rates**
- **Bedre sikkerhed**
- **Mindre vedligeholdelse**
- **Professional support**
- **Compliance** med email standarder

Lokale SMTP servere er kun anbefalede til:
- **Development/testing** miljøer
- **Høj sikkerhed** krav
- **Custom email** workflows
- **Offline** funktionalitet

---

## **Relaterede Emner i Videnbasen**

- **[[SignalR]]** - Real-time notifikationer om email status
- **[[IT-ServiceManagment]]** - Email integration i service management
- **[[Cisco CCNA/OSI-Modellen]]** - SMTP protokol forståelse

> **Tip:** Kombiner email services med SignalR for at skabe en komplet notifikationsløsning i dine .NET applikationer.