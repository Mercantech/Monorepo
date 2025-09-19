### 💡 **1. "Deploy or Die" – Fra kode til internet**

> _Motto: “Hvad nytter det at kode, hvis ingen kan se det?”_

- Lær at deploye et projekt med:
    
    - Docker, Nginx, server setup (DigitalOcean, Render, Vercel - Localt med Dokploy)
        
    - CI/CD pipelines (Github Actions, WebHooks)
        
    - Domæne, HTTPS (Let’s Encrypt, Cloudflare)
        
    - Basic frontend/backend structure (SPA, API)
        

**Slutprodukt**: Deres projekt kører online. Mor kan se det. Og deres GitHub-profil bliver ikke flov.

Officielle valgfag ville 
### 20738 - DevNet - 10 dage

1. Lærlingen kan benytte basis Python programmeringscript
2. Lærlingen kan benytte basis Linux shell-kommandoer
3. Lærlingen kan implementere og udvikle simple DevNet-miljøer (API-kald).
4. Lærlingen kan bruge gældende standarder til at udvikle DevNet-miljøer.
5. Lærlingen kan oprette API-forespørgsler via sikre protokoller f.eks. HTTPs.
6. Lærlingen kan forklare grundlæggende netværksbegreber.
7. Lærlingen kan benytte værktøjer til at deploye og sikre data i et cloudmiljø
	[EUD](https://www.eud.uddannelsesadministration.dk/Soeg/EUDEnkeltfag/Detaljer.aspx?FAG_ID=225258)

### Deployment Service
1. Lærlingen kan redegøre for fordele og ulemper forbundet med forskellige Deployment metoder.
2. Lærlingen kan installere og konfigurere Deployment Service til brug ved udrulning af software og operativsystemer over netværk.
3. Lærlingen kan installere og tilrette et OS til brug som basis for et Deployment image.
4. Lærlingen kan producere pakker og images til udrulning af software og operativsystemer over netværk.
5. Lærlingen kan tilføje reference images og nødvendige device drivere til et Deployment Share via Deployment Workbench.
6. Lærlingen kan oprette og tilrette Task Sequences i forbindelse med et givent Deployment scenarie.
7. Lærlingen kan administrere forskellige roller ud fra individuelle behov, som tredje parts software, hardware, specifikke device drivers osv.
8. Lærlingen kan administrere og tilrette software på liveinstallationer via Group Policies.
	[EUD](https://www.eud.uddannelsesadministration.dk/Soeg/EUDEnkeltfag/Detaljer.aspx?FAG_ID=213731)

### Core Applications

1. Lærlingen kan beskrive strukturen for en 3-lags applikationsmodel og dens fordele.
2. Lærlingen kan implementere en database på op til 10 tabeller, på baggrund af et E/R-diagram.
3. Lærlingen kan oprette Stored Procedures, som kan søge, indsætte, opdatere og slette data i databasen.
4. Lærlingen kan implementere Data Access Layer klasserne og deres metoder/parametre.
5. Lærlingen kan implementere Business Logic Layer klasserne og deres metoder/parametre.
6. Lærlingen kan foretage logisk og struktureret fejlfinding på applikations datalag.
7. Lærlingen kan betjene et version-styringsystem i forbindelse med et udviklingsteam.
	[EUD](https://www.eud.uddannelsesadministration.dk/Soeg/EUDEnkeltfag/Detaljer.aspx?FAG_ID=169993)

---

## 📅 **3-ugers struktur: "Deploy or Die" - Praktisk implementation**

### **Uge 1: "Server Setup & Grundlæggende Sikkerhed"**

#### **Dag 1: SSH & Server Access + Grundlæggende Sikkerhed**
- Få egen server (DigitalOcean/AWS/Azure) - Vi bruger nok bare Datacenter maskinerne med Twingate
- SSH setup og Twingate VPN
- Grundlæggende Linux kommandoer
- SSH sikkerhed (disable root, key-only)
- System updates og pakke management
- **Mål**: Eleverne kan logge ind på deres server og har grundlæggende sikkerhed

#### **Dag 2: Domæne & DNS + Firewall**
- Køb domæne (eller brug subdomain)
- DNS konfiguration (A, CNAME records)
- Cloudflare setup
- UFW/iptables konfiguration
- **Mål**: Domæne peger på deres server og firewall er konfigureret

#### **Dag 3: Database Setup med Docker**
- Docker installation
- Database container (PostgreSQL/MySQL)
- Database konfiguration og connection
- **Mål**: Database kører i container

#### **Dag 4: Web Server Setup (Nginx) + HTTPS & SSL**
- Nginx installation og konfiguration
- Virtual hosts setup
- Static file serving
- Let's Encrypt certifikat
- Auto-renewal setup
- HTTP → HTTPS redirect
- **Mål**: "Hello World" hjemmeside kører og sikre med HTTPS

#### **Dag 5: Release & Reflektion**
- Test af komplet setup
- Dokumentation af processen
- Reflektion over læring
- Forberedelse til uge 2
- **Mål**: Eleverne har et fungerende setup og har reflekteret over processen

---

### **Uge 2: "Containerization & CI/CD med Dokploy"**

#### **Dag 6: Docker Grundlæggende**
- Docker installation
- Dockerfile skrivning
- Container build og run
- **Mål**: Simpel app i container

#### **Dag 7: Docker Compose & Multi-container**
- Docker Compose setup
- Database + app container
- Environment variables
- **Mål**: Full-stack app i containers

#### **Dag 8: Docker Volumes & Data Persistence**
- Volume mapping for database
- Data persistence setup
- Backup strategier
- **Mål**: Data overlever container restarts

#### **Dag 9: Dokploy Installation, Setup & GitHub Integration**
- Dokploy installation på server
- Web interface konfiguration
- Git repository forbindelse
- Deploy app via Dokploy interface
- GitHub webhook setup
- Automated deployment ved push
- **Mål**: Dokploy er klar til deployment, Push til GitHub = automatisk deploy via Dokploy

#### **Dag 10: Monitoring & Logging med Dokploy**
- Dokploy's built-in monitoring
- Application logging via Dokploy
- Uptime monitoring (UptimeKhana)
- **Mål**: Overvågning af live app gennem Dokploy

---

### **Uge 3: "Moderne Sikkerhed & CTF Challenge"**

#### **Dag 11: OWASP Top 10 & Modern Security Headers**
- OWASP Top 10 (2021) - moderne version
- Security headers (CSP, HSTS, X-Frame-Options)
- Input validation og sanitization
- **Mål**: Moderne sikker kode praksis

#### **Dag 12: Container Security & Secrets Management**
- Docker security best practices
- Environment variables vs secrets
- Container scanning (Trivy, Snyk)
- **Mål**: Sikre containers og secrets

#### **Dag 13: CTF Challenge - "Hack en app"**
- **Mål**: Praktisk sikkerhedstestning gennem CTF

#### **Dag 14: Monitoring & Incident Response**
- Security monitoring med Dokploy
- Log analysis og alerting
- Incident response plan
- **Mål**: Overvågning og håndtering af sikkerhedsbegivenheder

#### **Dag 15: Aflevering/fremlæggelse**

---

## 🎯 **Slutprodukt:**
Eleverne har en **komplet, sikker, overvåget webapp** der:
- Kører på deres egen server
- Har HTTPS og domæne
- Deployer automatisk ved GitHub push
- Er sikker mod almindelige angreb
- Bliver overvåget 24/7

### Mål inden det er klar til elever

Reducer 3 dage - Fredage skal ikke have teori, men tid til noter og refleksion! Find eksamens-form og tag en ekstra dag ud af uge 3! Så vi i alt har 4-4-3 fordeling, måske mindre?

Guide dem hen til 3 ugers deployment + 2 ugers ML eller 3 ugers game design! 6 uger er okay - så meget kan lade sig gøre

