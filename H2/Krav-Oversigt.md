# H2 Projekt - Krav Oversigt

## Uge 2 - Grundlæggende Backend

### 📦 Database & EFCore
- [ ] Opret `User` modellen i C#
- [ ] Konfigurer relationer i `DbContext`
- [ ] Lav og kør første EF Core migration
- [ ] Bekræft at databasen oprettes korrekt i PostgreSQL

### 🔐 Brugerhåndtering (Registrering & Login)
- [ ] Opret `UserController`
- [ ] Implementer registrering (POST /register) med BCrypt hashing
- [ ] Implementer login (POST /login) med validering

### 🏨 Database Relationer & DTO
- [ ] Opret `UserInfo` model og konfigurer 1:1 relation med `User`
- [ ] Opret `Hotel` model og konfigurer 1:n relation med `Room`
- [ ] Opret `Booking` model og konfigurer n:m relation mellem `User` og `Room`
- [ ] Opret DTO'er for optimeret data-overførsel
- [ ] Lav kontrollers til alle klasser med CRUD operationer
- [ ] Kør migration og opdater database

### 🔒 JWT og beskyttet endpoint
- [ ] Konfigurer JWT-token udstedelse og validering
- [ ] Opret endpoint (GET /me) der returnerer brugeroplysninger

### 🧪 Test og dokumentation
- [ ] Test registrering og login med Swagger/Postman
- [ ] Test JWT-beskyttet adgang til `/me`
- [ ] Skriv kort API-beskrivelse i README

---

## Uge 3 - Avanceret Backend

### 📝 XML Dokumentation af API
- [ ] Tilføj XML-kommentarer til alle controllere og endpoints
- [ ] Brug `<summary>`, `<param>`, `<returns>` og `<response code>` pr. endpoint
- [ ] Aktivér XML-dokumentation i .csproj-fil
- [ ] Konfigurer Swagger til at vise XML-dokumentation

### 🛡️ Fejlhåndtering & Sikkerhed
- [ ] Implementér restriktiv CORS-politik i API
- [ ] Test at kun frontend kan tilgå API'et
- [ ] Identificér og beskriv XSS beskyttelse

### 🗄️ Indsæt data & Repository Pattern
- [ ] Indsæt eksempeldata i databasen (brugere, værelser, bookinger)
- [ ] Brug AI eller Faker (Bogus) til test data
- [ ] Implementér repository pattern for mindst én model
- [ ] Refaktor kode så ansvar er adskilt (services, repositories, controllers)

### 🚀 Udvidet EF Core og Hosting
- [ ] Host applikationen på deploy.mercantec.tech
- [ ] Implementér Eager vs Lazy Loading
- [ ] Sammenlign performance mellem begge tilgange

---

## Uge 4 - Frontend med Blazor

### 🎯 Opstart af Blazor projekt
- [ ] Opret Blazor WASM projekt
- [ ] Konfigurer HTTP client til API kald
- [ ] Opret service-klasser til API kald med fejlhåndtering
- [ ] Lav simpel side der viser data fra API

### 🔐 Loginsystem med API & Blazor
- [ ] Opret login/register sider i Blazor
- [ ] Implementér authentication service i frontend
- [ ] Håndter JWT tokens i localStorage/sessionStorage
- [ ] Opret protected routes/komponenter
- [ ] Implementér logout funktionalitet

### 🔄 State Management i Blazor
- [ ] Implementér state management i Blazor app
- [ ] Håndter global state (user authentication)
- [ ] Test at state opdateres korrekt mellem komponenter
- [ ] Dokumentér state management løsning

### 📱 Skalerbar services & PWA
- [ ] Implementér mindst én PWA-funktion:
  - Service Worker
  - Web App Manifest
  - Offline funktionalitet
  - Push notifications
- [ ] Refaktor services til at være mere skalerbare
- [ ] Implementér dependency injection
- [ ] Opret interfaces for services

---

## Uge 6 - Active Directory Integration

### 🏢 Opsætning af Active Directory
- [ ] Opsæt Windows Server 2022 med Active Directory
- [ ] Følg guiden: H2 - Windows Server 2022 og AD
- [ ] Bekræft at AD kører og kan tilgås

### 👥 Roller, brugere og tilladelser
- [ ] Opret grupper, brugere og tildel roller i AD
- [ ] Konfigurer tilladelser for forskellige roller
- [ ] Test adgangsrettigheder for forskellige brugere

### 🔗 Loginsystem og integration til AD
- [ ] Implementér login op mod lokale AD (LDAP)
- [ ] Integrér AD-login i eksisterende system
- [ ] Test at brugere kan logge ind med AD-brugere

### 📊 Administrator Dashboard
- [ ] Design og implementér admin dashboard på frontend
- [ ] Vis og administrér brugere, grupper og roller fra AD
- [ ] Test at dashboardet kan kommunikere med AD

---

## Uge 8 - Ticket System & Real-time

### 🎫 Design af Ticket System
- [ ] Design datamodellen for Ticket System (Ticket, User, Status, etc.)
- [ ] Opret modeller i C#
- [ ] Konfigurer relationer i DbContext
- [ ] Lav og kør EF Core migration

### ⚡ Intro til SignalR / WebSocket
- [ ] Læs om og forstå SignalR/WebSocket
- [ ] Integrér SignalR i Ticket System
- [ ] Opret SignalR Hub
- [ ] Implementér realtidsopdateringer (nye tickets, statusopdateringer)

---

## Uge 10 - Aflevering & Eksamen

### 📋 Aflevering af projekt
- [ ] Sikr at Main branch er fungerende og stabil
- [ ] Test at alle features virker korrekt
- [ ] Fix alle kritiske bugs og fejl
- [ ] Verificer at alle tests kører succesfuldt
- [ ] Opret eller opdater README.md med komplet dokumentation
- [ ] Dokumentér implementerede features
- [ ] Dokumentér ufærdige features og fremtidige forbedringer
- [ ] Opret projekt dokumentation (arkitektur diagrammer, database ERD)
- [ ] Sikr kode kvalitet og standarder
- [ ] Opret deployment package

### 🎓 Eksamen
- [ ] **Projekt Status Verificering**
  - [ ] Main branch fungerende og stabil
  - [ ] Alle kritiske features virker
  - [ ] Projekt kan bygges og køres uden fejl
  - [ ] README.md er opdateret og komplet
- [ ] **Kode Opdeling og Fremlæggelse Forberedelse**
  - [ ] Identificer hvem der fremlægger hvilke dele
  - [ ] Opdel kodebase i logiske sektioner
  - [ ] Forbered demo data og test scenarier
- [ ] **Teknisk Forberedelse**
  - [ ] Test alle demo scenarier på forhånd
  - [ ] Sikr stabile internet forbindelser
  - [ ] Forbered backup planer
- [ ] **Dokumentation og Præsentation**
  - [ ] Præsentations slides er klar
  - [ ] Demo script er forberedt
  - [ ] Tekniske diagrammer er opdateret

---

## 📊 Release Oversigt

### Release 1 (Uge 2-3)
- Database & EF Core setup
- Brugerhåndtering med JWT
- Database relationer og DTO'er
- XML dokumentation og sikkerhed

### Release 2 (Uge 4)
- Blazor frontend med API integration
- Authentication og state management
- PWA funktionalitet

### Release 3 (Uge 6)
- Active Directory integration
- Administrator dashboard
- Roller og tilladelser

### Release 4 (Uge 8)
- Ticket system implementation
- Real-time funktionalitet med SignalR

### Final Release (Uge 10)
- Komplet dokumentation
- Aflevering og eksamen forberedelse
