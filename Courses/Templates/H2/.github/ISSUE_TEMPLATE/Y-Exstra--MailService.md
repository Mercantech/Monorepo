---
name: Ekstra emne - Mail Service Implementation
about: Implementér Gmail SMTP mail service med HTML og billede support
title: 'Ekstra emne - Mail Service Implementation'
labels: ['ekstra-emne', 'mail', 'smtp', 'gmail', 'html']
assignees: ''
---

## Mail Service Implementation med Gmail SMTP

- [ ] Opsæt Gmail SMTP konfiguration
  - [ ] Konfigurer app-specifikke passwords for Gmail
  - [ ] Opsæt SMTP settings i appsettings.json
  - [ ] Implementér sikker credential management
- [ ] Implementér Mail Service interface og implementation
  - [ ] Opret IMailService interface
  - [ ] Implementér GmailSmtpService med System.Net.Mail
  - [ ] Tilføj dependency injection konfiguration
  - [ ] Implementér error handling og logging
- [ ] Support for HTML emails
  - [ ] Implementér HTML template system
  - [ ] Opret email template models
  - [ ] Support for inline CSS styling
  - [ ] Implementér template caching for performance
- [ ] Support for billeder og vedhæftninger
  - [ ] Implementér attachment support
  - [ ] Support for inline billeder (embedded images)
  - [ ] Implementér file upload til email attachments
  - [ ] Support for forskellige billede formater (PNG, JPG, GIF)
- [ ] Opret API endpoints for mail sending
  - [ ] POST /api/mail/send - Send simple text email
  - [ ] POST /api/mail/send-html - Send HTML email
  - [ ] POST /api/mail/send-with-attachments - Send email med vedhæftninger
  - [ ] GET /api/mail/templates - Hent tilgængelige templates


---
### Eksempel på Mail Service interface
```csharp
public interface IMailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = false);
    Task<bool> SendEmailWithAttachmentsAsync(string to, string subject, string body, 
        List<EmailAttachment> attachments, bool isHtml = false);
    Task<bool> SendHtmlEmailAsync(string to, string subject, string templateName, 
        object model, List<EmailAttachment> attachments = null);
    Task<List<EmailTemplate>> GetTemplatesAsync();
    Task<EmailStatus> GetEmailStatusAsync(string emailId);
}
```

### Eksempel på Gmail SMTP konfiguration
```json
{
  "MailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "EnableSsl": true,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "FromEmail": "your-email@gmail.com",
    "FromName": "H2 Application"
  }
}
```

### Eksempel på HTML email template
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>{{Subject}}</title>
    <style>
        .email-container { max-width: 600px; margin: 0 auto; }
        .header { background-color: #007bff; color: white; padding: 20px; }
        .content { padding: 20px; background-color: #f8f9fa; }
        .footer { padding: 10px; text-align: center; font-size: 12px; }
    </style>
</head>
<body>
    <div class="email-container">
        <div class="header">
            <h1>{{Title}}</h1>
        </div>
        <div class="content">
            <p>Hej {{UserName}},</p>
            <p>{{Message}}</p>
            {{#if HasImage}}
            <img src="cid:{{ImageId}}" alt="{{ImageAlt}}" style="max-width: 100%;">
            {{/if}}
        </div>
        <div class="footer">
            <p>Dette er en automatisk genereret email fra H2 Application</p>
        </div>
    </div>
</body>
</html>
```

### Eksempel på API controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;
    
    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        var result = await _mailService.SendEmailAsync(
            request.To, 
            request.Subject, 
            request.Body, 
            request.IsHtml
        );
        
        return result ? Ok() : BadRequest("Failed to send email");
    }
    
    [HttpPost("send-html")]
    public async Task<IActionResult> SendHtmlEmail([FromBody] SendHtmlEmailRequest request)
    {
        var result = await _mailService.SendHtmlEmailAsync(
            request.To, 
            request.Subject, 
            request.TemplateName, 
            request.Model,
            request.Attachments
        );
        
        return result ? Ok() : BadRequest("Failed to send HTML email");
    }
}
```

### Docker Compose eksempel for mail service
```yaml
mail-service:
  build: .
  environment:
    - MailSettings__SmtpServer=smtp.gmail.com
    - MailSettings__SmtpPort=587
    - MailSettings__EnableSsl=true
    - MailSettings__Username=${GMAIL_USERNAME}
    - MailSettings__Password=${GMAIL_APP_PASSWORD}
  volumes:
    - ./email-templates:/app/templates
    - ./email-attachments:/app/attachments
  depends_on:
    - redis
```

### Krav til mail service
- [ ] Support for både plain text og HTML emails
- [ ] Gmail SMTP integration med app-specifikke passwords
- [ ] Template system med variable substitution
- [ ] Support for billeder og vedhæftninger
- [ ] Background processing med queue system
- [ ] Error handling og retry logic
- [ ] Mail status tracking og history
- [ ] API endpoints for alle mail funktioner

### Bonus opgaver
- [ ] Implementér email scheduling (send later)
- [ ] Opret email analytics og tracking
- [ ] Implementér bulk email sending
- [ ] Opret email template editor i Blazor
- [ ] Implementér email bounces og unsubscribe handling
- [ ] Opret email campaign management
