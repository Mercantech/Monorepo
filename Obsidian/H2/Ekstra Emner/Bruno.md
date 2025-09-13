# 🚀 Bruno API Testing - Komplet Guide

> **Bruno** er et moderne, open-source API-testværktøj der fungerer som et kraftfuldt alternativ til Postman. Med Bruno gemmes alle API-kald som filer i dit projekt, hvilket gør det nemt at versionsstyre og dele API-tests med teamet.

## 📋 Indholdsfortegnelse

- [🎯 Hvad er Bruno?](#-hvad-er-bruno)
- [✨ Fordele ved Bruno](#-fordele-ved-bruno)
- [🛠️ Installation og Setup](#️-installation-og-setup)
- [📁 Projektstruktur](#-projektstruktur)
- [🔧 Grundlæggende Konfiguration](#-grundlæggende-konfiguration)
- [🌍 Environment Management](#-environment-management)
- [📝 Request Types og Syntax](#-request-types-og-syntax)
- [🧪 Testing og Assertions](#-testing-og-assertions)
- [📊 Scripting og Automation](#-scripting-og-automation)
- [📈 Best Practices](#-best-practices)
- [🔍 Fejlfinding](#-fejlfinding)
- [💡 Tips og Tricks](#-tips-og-tricks)

---

## 🎯 Hvad er Bruno?

Bruno er et **file-based API client** der gemmer alle dine API-kald som `.bru` filer i dit projekt. Dette giver flere fordele:

- **Version Control**: Alle API-tests kan versionsstyres med Git
- **Team Collaboration**: Nem deling og samarbejde på API-tests
- **Offline Support**: Fungerer uden internetforbindelse
- **Cross-Platform**: Kører på Windows, macOS og Linux
- **Open Source**: Gratis og open source

### Hvordan Bruno Adskiller sig fra Postman

| Feature | Bruno | Postman |
|---------|-------|---------|
| **Data Storage** | Filer i projektet | Cloud/Workspace |
| **Version Control** | ✅ Native Git support | ❌ Begrænset |
| **Offline** | ✅ Fuldt offline | ❌ Kræver internet |
| **Pricing** | ✅ Gratis | ❌ Betalt for teams |
| **File Format** | `.bru` (læsbart) | `.json` (binær) |

---

## ✨ Fordele ved Bruno

### 🎯 **File-Based Architecture**
```bash
Bruno/
├── API/
│   ├── collection.bru
│   ├── bruno.json
│   ├── environments/
│   │   └── H2-MAGS.bru
│   ├── Users/
│   │   ├── Login.bru
│   │   └── Get-User.bru
│   └── Hotels/
│       └── Create-Hotel.bru
```

### 🔄 **Git Integration**
- Alle tests gemmes som læsbare filer
- Nem diff og merge i Git
- Historik over API-ændringer
- Branch-baseret API testing

### 🚀 **Performance**
- Hurtigere end Postman
- Mindre ressourceforbrug
- Instant loading af store collections

---

## 🛠️ Installation og Setup

### 1. Download Bruno
```bash
# Besøg https://www.usebruno.com/
# Download til dit operativsystem
```

### 2. Åbn Collection
1. Start Bruno
2. Klik "Open Collection"
3. Naviger til din `Bruno/API` mappe
4. Vælg `collection.bru` filen

### 3. Verificer Setup
- Tjek at alle endpoints er synlige
- Verificer environment variabler
- Test et simpelt API-kald

---

## 📁 Projektstruktur

### Bruno Collection Struktur
```
Bruno/
├── API/                          # Hovedcollection
│   ├── bruno.json               # Collection metadata
│   ├── collection.bru           # Collection konfiguration
│   ├── environments/            # Environment variabler
│   │   └── H2-MAGS.bru         # H2-MAGS environment
│   ├── Auth/                    # Autentificering endpoints
│   │   ├── AD-Login.bru
│   │   └── AD-Me.bru
│   ├── Users/                   # Bruger management
│   │   ├── Login.bru
│   │   ├── Get-Users.bru
│   │   └── Create-User.bru
│   ├── Hotels/                  # Hotel management
│   │   ├── Create-Hotel.bru
│   │   └── Get-Hotels.bru
│   ├── Rooms/                   # Rum management
│   └── Status/                  # Health checks
│       └── Ping.bru
```

### .bru Fil Struktur
```bru
meta {
  name: "Request Name"
  type: http
  seq: 1
  tags: ["tag1", "tag2"]
}

get {
  url: {{baseUrl}}/api/endpoint
  body: none
  auth: inherit
}

headers {
  Authorization: {{authToken}}
  Content-Type: application/json
}

body:json {
  {
    "key": "value"
  }
}

tests {
  test("Test name", function() {
    expect(res.status).to.equal(200);
  });
}

script:pre-request {
  // Pre-request script
}

script:post-response {
  // Post-response script
}
```

---

## 🔧 Grundlæggende Konfiguration

### Collection Konfiguration (`collection.bru`)
```bru
auth {
  mode: none  // eller bearer, basic, etc.
}

docs {
  API testing for MAGS 2025 H2 projekt
}
```

### Bruno Metadata (`bruno.json`)
```json
{
  "version": "1",
  "name": "API",
  "type": "collection",
  "ignore": ["node_modules", ".git"],
  "presets": {
    "requestType": "http",
    "requestUrl": "https://25h2-mags.mercantec.tech/"
  }
}
```

---

## 🌍 Environment Management

### Environment Fil (`H2-MAGS.bru`)
```bru
vars {
  baseUrl: https://25h2-mags.mercantec.tech
  apiKey: "{{JWT-Admin}}"
  userId: "sample-user-id"
  userEmail: "test@example.com"
  userPassword: "password123"
}
```

### Brug af Environment Variabler
```bru
get {
  url: {{baseUrl}}/api/users/{{userId}}
}

headers {
  Authorization: Bearer {{apiKey}}
}
```

### Environment Switching
- Klik på environment dropdown øverst
- Vælg det ønskede environment
- Alle `{{variabel}}` referencer opdateres automatisk

---

## 📝 Request Types og Syntax

### HTTP Methods

#### GET Request
```bru
get {
  url: {{baseUrl}}/api/users
  body: none
  auth: inherit
}
```

#### POST Request
```bru
post {
  url: {{baseUrl}}/api/users
  body: json
  auth: inherit
}

body:json {
  {
    "email": "{{userEmail}}",
    "password": "{{userPassword}}",
    "username": "testuser"
  }
}
```

#### PUT Request
```bru
put {
  url: {{baseUrl}}/api/users/{{userId}}
  body: json
  auth: inherit
}

body:json {
  {
    "username": "updateduser"
  }
}
```

#### DELETE Request
```bru
delete {
  url: {{baseUrl}}/api/users/{{userId}}
  body: none
  auth: inherit
}
```

### Body Types

#### JSON Body
```bru
body:json {
  {
    "name": "Test Hotel",
    "address": "Test Street 123",
    "city": "Copenhagen"
  }
}
```

#### Form Data
```bru
body:form {
  name: "Test Hotel"
  address: "Test Street 123"
  city: "Copenhagen"
}
```

#### URL Encoded
```bru
body:urlencoded {
  name: "Test Hotel"
  address: "Test Street 123"
}
```

### Headers
```bru
headers {
  Authorization: Bearer {{apiKey}}
  Content-Type: application/json
  Accept: application/json
  X-API-Version: v1
}
```

---

## 🧪 Testing og Assertions

### Grundlæggende Tests
```bru
tests {
  // Status code test
  test("Status skal være 200 OK", function() {
    expect(res.status).to.equal(200);
  });

  // Response body test
  test("Response skal indeholde data", function() {
    expect(res.body).to.not.be.empty;
  });

  // Response time test
  test("Response tid skal være under 1 sekund", function() {
    expect(res.responseTime).to.be.lessThan(1000);
  });
}
```

### Avancerede Tests
```bru
tests {
  // JSON struktur test
  test("Response har korrekt JSON struktur", function() {
    expect(res.body).to.have.property("data");
    expect(res.body).to.have.property("message");
    expect(res.body.data).to.be.an("array");
  });

  // Array længde test
  test("Data array har mindst 1 element", function() {
    expect(res.body.data).to.have.length.greaterThan(0);
  });

  // String format test
  test("Email er i korrekt format", function() {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    expect(res.body.user.email).to.match(emailRegex);
  });

  // UUID format test
  test("ID er et gyldigt UUID", function() {
    const uuidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    expect(res.body.user.id).to.match(uuidRegex);
  });
}
```

### Error Handling Tests
```bru
tests {
  // Error status test
  test("Fejl status returneres korrekt", function() {
    expect(res.status).to.equal(404);
  });

  // Error message test
  test("Fejl besked er informativ", function() {
    expect(res.body).to.have.property("error");
    expect(res.body.error).to.not.be.empty;
  });
}
```

---

## 📊 Scripting og Automation

### Pre-Request Scripts
```bru
script:pre-request {
  // Generer timestamp
  const timestamp = new Date().toISOString();
  bru.setVar("timestamp", timestamp);
  
  // Generer random ID
  const randomId = Math.random().toString(36).substr(2, 9);
  bru.setVar("randomId", randomId);
  
  // Log request info
  console.log("Sending request at:", timestamp);
}
```

### Post-Response Scripts
```bru
script:post-response {
  // Gem JWT token
  if (res.body && res.body.token) {
    bru.setEnvVar("JWT-User", res.body.token);
    console.log("JWT token gemt i environment");
  }
  
  // Gem user ID
  if (res.body && res.body.user && res.body.user.id) {
    bru.setEnvVar("currentUserId", res.body.user.id);
    console.log("User ID gemt:", res.body.user.id);
  }
  
  // Log response info
  console.log("Response status:", res.status);
  console.log("Response time:", res.responseTime + "ms");
}
```

### Environment Variable Management
```bru
script:post-response {
  // Gem data i environment
  bru.setEnvVar("lastCreatedId", res.body.id);
  bru.setEnvVar("lastCreatedName", res.body.name);
  
  // Gem data i collection variabler
  bru.setVar("collectionData", res.body.data);
}
```

---

## 📈 Best Practices

### 1. **Organisering af Requests**
- Gruppér relaterede requests i mapper
- Brug beskrivende navne på dansk
- Organisér efter funktionalitet (Users, Hotels, etc.)

### 2. **Environment Management**
- Brug forskellige environments til dev/staging/prod
- Gem sensitive data i environment variabler
- Dokumenter alle environment variabler

### 3. **Testing Strategy**
- Skriv tests for alle endpoints
- Test både success og error cases
- Inkludér performance tests
- Test data validation

### 4. **Naming Conventions**
```
✅ Gode navne:
- "Login - New User"
- "Hent alle hoteller"
- "Opret booking - Success"
- "Get User by ID - Error"

❌ Dårlige navne:
- "test1"
- "api call"
- "request"
```

### 5. **Documentation**
- Brug `docs` sektion i collection
- Tilføj kommentarer i scripts
- Dokumenter environment variabler
- Beskriv test cases

### 6. **Version Control**
- Commit ofte med beskrivende messages
- Brug branches til nye features
- Tag releases af API collections
- Review changes før merge

---

## 🔍 Fejlfinding

### Almindelige Problemer

#### 1. **Connection Refused**
```
Error: connect ECONNREFUSED
```
**Løsning:**
- Tjek at API'en kører
- Verificer baseUrl i environment
- Tjek firewall indstillinger

#### 2. **401 Unauthorized**
```
Error: 401 Unauthorized
```
**Løsning:**
- Tjek JWT token i headers
- Verificer token er ikke udløbet
- Tjek authentication flow

#### 3. **404 Not Found**
```
Error: 404 Not Found
```
**Løsning:**
- Tjek URL endpoint
- Verificer API version
- Tjek routing konfiguration

#### 4. **Environment Variables Not Working**
```
Error: {{baseUrl}} not resolved
```
**Løsning:**
- Tjek environment er valgt
- Verificer variabel navn
- Tjek syntax (dobbelt curly braces)

### Debug Tips
```bru
script:post-response {
  // Debug response
  console.log("Full response:", res);
  console.log("Response body:", res.body);
  console.log("Response headers:", res.headers);
  console.log("Response time:", res.responseTime);
}
```

---

## 💡 Tips og Tricks

### 1. **Bulk Operations**
```bru
// Brug loops til at teste flere endpoints
script:pre-request {
  const endpoints = ["/users", "/hotels", "/rooms"];
  const randomEndpoint = endpoints[Math.floor(Math.random() * endpoints.length)];
  bru.setVar("randomEndpoint", randomEndpoint);
}
```

### 2. **Data Generation**
```bru
script:pre-request {
  // Generer test data
  const testData = {
    email: `test${Date.now()}@example.com`,
    username: `user${Math.random().toString(36).substr(2, 5)}`,
    timestamp: new Date().toISOString()
  };
  
  bru.setVar("testData", JSON.stringify(testData));
}
```

### 3. **Conditional Testing**
```bru
tests {
  test("Conditional test", function() {
    if (res.status === 200) {
      expect(res.body).to.have.property("data");
    } else {
      expect(res.body).to.have.property("error");
    }
  });
}
```

### 4. **Performance Monitoring**
```bru
tests {
  test("Performance test", function() {
    expect(res.responseTime).to.be.lessThan(1000);
    
    // Log performance data
    console.log(`Performance: ${res.responseTime}ms`);
  });
}
```

### 5. **Chain Requests**
```bru
script:post-response {
  // Gem data til næste request
  if (res.body && res.body.id) {
    bru.setEnvVar("lastCreatedId", res.body.id);
  }
}
```

---

## 🎯 Konklusion

Bruno er et kraftfuldt værktøj til API testing der kombinerer:

- **Simplicitet** - Nem at lære og bruge
- **Kraft** - Avancerede testing og scripting muligheder  
- **Fleksibilitet** - File-based architecture med Git integration
- **Performance** - Hurtig og effektiv

Med Bruno kan du:
- ✅ Organisere API tests struktureret
- ✅ Automatisere testing workflows
- ✅ Samarbejde effektivt med teamet
- ✅ Integrere med version control
- ✅ Teste både manuelt og automatisk

**Start med Bruno i dag og oplev en ny måde at teste APIs på!** 🚀

---

*Dette dokument dækker Bruno v1.x. For opdateringer og nyeste features, besøg [usebruno.com](https://www.usebruno.com/)*
