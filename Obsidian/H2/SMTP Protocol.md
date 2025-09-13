# SMTP Protocol - Simple Mail Transfer Protocol

SMTP (Simple Mail Transfer Protocol) er det grundlæggende protokol der bruges til at sende e-mails over internettet. Det er det som Gmail, Outlook og alle andre e-mail providers bruger til at levere beskeder!

![[Pasted image 20250913132110.png]]

## Hvad er SMTP?

SMTP er en **tekst-baseret protokol** der opererer på **port 25** (standard) eller **port 587** (med TLS/SSL). Den håndterer kun **udgående e-mails** - for at modtage e-mails bruger man typisk POP3 eller IMAP.

### Hovedfunktioner:
- **Sender e-mails** fra en klient til en server
- **Overfører e-mails** mellem servere
- **Validerer** e-mail adresser og formater
- **Håndterer fejl** og leveringsstatus

## Hvordan fungerer SMTP?

### 1. **Forbindelse oprettes**
```
Klient → SMTP Server (port 25/587)
```

### 2. **SMTP Kommandoer** (tekst-baseret):
```
HELO/EHLO: Identificerer afsender
MAIL FROM: <sender@example.com>
RCPT TO: <recipient@example.com>
DATA: E-mail indhold
QUIT: Afslutter forbindelse
```

### 3. **Eksempel på SMTP session:**
```
S: 220 mail.example.com SMTP ready
C: HELO client.example.com
S: 250 Hello client.example.com
C: MAIL FROM: <user@example.com>
S: 250 OK
C: RCPT TO: <recipient@example.com>
S: 250 OK
C: DATA
S: 354 Start mail input; end with <CRLF>.<CRLF>
C: Subject: Test email
C: 
C: This is a test message.
C: .
S: 250 OK: queued as 12345
C: QUIT
S: 221 Bye
```

## Sikkerhed og kryptering

### **TLS/SSL Support:**
- **STARTTLS**: Opgraderer forbindelse til krypteret
- **SMTPS**: Direkte SSL forbindelse på port 465
- **Port 587**: Moderne standard med STARTTLS

### **Autentificering:**
- **SMTP AUTH**: Login med brugernavn/password
- **OAuth2**: Moderne sikkerhedsstandard
- **API Keys**: Til applikationer

## Opsætning af SMTP

### **1. Gmail SMTP:**
```
Server: smtp.gmail.com
Port: 587 (TLS) eller 465 (SSL)
Sikkerhed: STARTTLS eller SSL
Autentificering: Ja
```

### **2. Outlook/Hotmail SMTP:**
```
Server: smtp-mail.outlook.com
Port: 587
Sikkerhed: STARTTLS
Autentificering: Ja
```

### **3. Custom SMTP Server:**
```csharp
// C# eksempel
var client = new SmtpClient("smtp.example.com", 587)
{
    EnableSsl = true,
    Credentials = new NetworkCredential("username", "password")
};
```

## Hvorfor er SMTP så vigtigt?

### **1. Global E-mail Infrastruktur**
- **Standardiseret protokol** - alle e-mail systemer forstår SMTP
- **Interoperabilitet** mellem forskellige providers
- **Skalerbarhed** - håndterer millioner af e-mails

### **2. Pålidelighed**
- **Fejlhåndtering** med detaljerede statuskoder
- **Queue system** - e-mails gemmes hvis modtager er offline
- **Retry mekanismer** for mislykkede leveringer

### **3. Sikkerhed**
- **Krypteret transmission** med TLS/SSL
- **Autentificering** forhindrer spam
- **Validering** af e-mail adresser

### **4. Integration**
- **API integration** i web applikationer
- **Automatiserede systemer** (notifikationer, rapporter)
- **Bulk e-mail** til marketing og kommunikation

## SMTP vs. andre protokoller

| Protokol | Formål | Port | Sikkerhed |
|----------|--------|------|-----------|
| **SMTP** | Sende e-mails | 25/587 | TLS/SSL |
| **POP3** | Hente e-mails | 110/995 | TLS/SSL |
| **IMAP** | Synkronisere e-mails | 143/993 | TLS/SSL |

## Almindelige problemer og løsninger

### **1. "Connection refused"**
- Tjek firewall indstillinger
- Verificer port nummer
- Kontroller server adresse

### **2. "Authentication failed"**
- Verificer brugernavn/password
- Tjek om 2FA er aktiveret
- Brug app-specifikke passwords

### **3. "TLS/SSL errors"**
- Verificer certifikat gyldighed
- Tjek TLS version support
- Prøv forskellige sikkerhedsindstillinger

## Praktiske anvendelser

### **Web Applikationer:**
- Kontaktformularer
- Brugernotifikationer
- Password reset e-mails
- Velkomst e-mails

### **System Integration:**
- Monitoring alerts
- Automatiske rapporter
- Backup notifikationer
- Fejlmeddelelser

### **Marketing:**
- Newsletter udsendelse
- Produktannoncering
- Event invitations
- Kundeopfølgning

## Fremtiden for SMTP

- **IPv6 support** for bedre adressering
- **Enhanced security** med nyere kryptografiske standarder
- **Better spam protection** med AI-baserede systemer
- **API-first approach** med moderne web services

---

**Konklusion:** SMTP er rygraden i moderne e-mail kommunikation. Uden SMTP ville vi ikke kunne sende e-mails på tværs af forskellige platforme og providers. Det er en kritisk del af internettets infrastruktur og uundværligt for både personlig og professionel kommunikation.

## Mere end bare SMTP

![[Pasted image 20250913132924.png]]

Selvom SMTP er standarden til at **sende** e-mails, er der andre protokoller der håndterer **modtagelse** og **synkronisering** af e-mails. Her er de vigtigste:

### **[[IMAP]] - Internet Message Access Protocol**

**Hvad er IMAP?**
- **Synkroniserer** e-mails mellem server og klient
- **Bevarer** e-mails på serveren
- **Multi-device** support - samme e-mails på alle enheder
- **Mappe struktur** synkroniseres

**Hvornår bruges IMAP?**
- **Moderne e-mail klienter** (Outlook, Apple Mail, Thunderbird)
- **Mobil enheder** og tablets
- **Fleksibel adgang** fra flere lokationer
- **Team miljøer** hvor flere skal se samme e-mails

**Fordele:**
- ✅ E-mails tilgængelige overalt
- ✅ Synkroniseret mappestruktur
- ✅ Hurtig søgning på server
- ✅ Delte mailbokse mulige

**Ulemper:**
- ❌ Kræver internet forbindelse
- ❌ Kan være langsommere end POP3
- ❌ Bruger mere server plads

---

### **[[POP3]] - Post Office Protocol version 3**

**Hvad er POP3?**
- **Henter** e-mails fra server til lokal klient
- **Downloader** e-mails til computeren
- **Fjerner** e-mails fra serveren (standard)
- **Offline** adgang til hentede e-mails

**Hvornår bruges POP3?**
- **Lokale e-mail klienter** på en specifik computer
- **Begrænset server plads**
- **Offline arbejde** uden internet
- **Sikkerhed** - e-mails gemmes lokalt

**Fordele:**
- ✅ Hurtig offline adgang
- ✅ Sparer server plads
- ✅ Ingen internet krævet efter download
- ✅ Simpel protokol

**Ulemper:**
- ❌ E-mails kun på én enhed
- ❌ Ingen synkronisering
- ❌ Kan miste e-mails ved computer skade
- ❌ Ingen mappe synkronisering

---

## Sammenligning af E-mail Protokoller

| Aspekt | **SMTP** | **IMAP** | **POP3** |
|--------|----------|----------|----------|
| **Formål** | Sende e-mails | Synkronisere e-mails | Hente e-mails |
| **Port** | 25/587 | 143/993 | 110/995 |
| **Sikkerhed** | TLS/SSL | TLS/SSL | TLS/SSL |
| **Server lagring** | Midlertidig | Permanent | Midlertidig |
| **Multi-device** | N/A | ✅ Ja | ❌ Nej |
| **Offline adgang** | N/A | ❌ Begrænset | ✅ Ja |
| **Kompleksitet** | Medium | Høj | Lav |

---

## Hvad bruges mest i dag?

### **Moderne Standard: IMAP + SMTP**
```
📧 Moderne Setup:
├── SMTP (port 587) → Sende e-mails
└── IMAP (port 993) → Modtage e-mails
```

**Hvorfor IMAP er populært:**
- **Cloud-baseret** arbejdsgang
- **Mobil-first** tilgang
- **Team collaboration** muligheder
- **Backup** og sikkerhed

### **Legacy/Enterprise: POP3 + SMTP**
```
📧 Traditionel Setup:
├── SMTP (port 587) → Sende e-mails
└── POP3 (port 995) → Hente e-mails
```

**Hvorfor POP3 stadig bruges:**
- **Legacy systemer** der ikke er opdateret
- **Sikkerheds-kritiske** miljøer
- **Begrænset server plads**
- **Offline arbejdsgange**

---

## Praktiske Eksempler

### **Gmail Konfiguration:**
```
📧 Gmail Setup:
├── SMTP: smtp.gmail.com:587 (TLS)
├── IMAP: imap.gmail.com:993 (SSL)
└── POP3: pop.gmail.com:995 (SSL)
```

### **Outlook/Hotmail:**
```
📧 Outlook Setup:
├── SMTP: smtp-mail.outlook.com:587 (TLS)
├── IMAP: outlook.office365.com:993 (SSL)
└── POP3: outlook.office365.com:995 (SSL)
```

### **Custom Server:**
```
📧 Custom Setup:
├── SMTP: mail.example.com:587 (TLS)
├── IMAP: mail.example.com:993 (SSL)
└── POP3: mail.example.com:995 (SSL)
```

---

## Anbefalinger

### **Til Privat Brug:**
- **IMAP + SMTP** for moderne brugere
- **POP3 + SMTP** kun hvis du foretrækker offline arbejde

### **Til Virksomheder:**
- **IMAP + SMTP** for team collaboration
- **Exchange/Office 365** for enterprise features

### **Til Udviklere:**
- **SMTP** til at sende e-mails fra applikationer
- **IMAP** til at læse e-mails programmatisk
- **API'er** (Gmail API, Microsoft Graph) for moderne integration

---

**Konklusion:** Mens SMTP er uundværligt til at sende e-mails, er valget mellem IMAP og POP3 afhængigt af dine behov. IMAP er det moderne valg for de fleste brugere, mens POP3 stadig har sin plads i specifikke scenarier.