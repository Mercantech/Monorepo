# Videnstjek - Quiz App

En moderne, interaktiv quiz-app bygget med Blazor WebAssembly til at hjælpe elever med at teste deres viden.

## 🎯 Formål

Videnstjek er designet til at give elever mulighed for at:
- Teste deres viden med kvalitetsfulde spørgsmål
- Få detaljerede forklaringer på alle svar
- Lære af deres fejl og forbedre sig
- Øve sig så meget de vil uden begrænsninger

**Vigtigt:** Appen gemmer ingen resultater eller svar - alt forbliver privat på brugerens enhed.

## 🚀 Funktioner

- **Moderne UI/UX**: Responsivt design der fungerer på alle enheder
- **Kategoriserede Quizzer**: Organiseret i GF2, Backend og Frontend kategorier
- **Intelligent Navigation**: Filtrer quizzer efter kategori med URL-parametre
- **Interaktive Quizzer**: Engagerende spørgsmål med multiple-choice svar
- **Detaljerede Forklaringer**: Læringsfokus med grundige forklaringer på alle svar
- **Progress Tracking**: Visuel fremgangsindikator gennem quizzen
- **Responsivt Design**: Fungerer perfekt på computer, tablet og mobil
- **Fremtrædende Resultater**: Quiz-afslutning vises som en centreret overlay med animerede elementer
- **Spørgsmål Oversigt**: Detaljeret oversigt over alle spørgsmål med farvekodning (grøn/rød) for korrekte/forkerte svar
- **Automatisk Indexering**: Ingen manuel vedligeholdelse af quiz-lister
- **Søgefunktionalitet**: Find specifikke quizzer hurtigt

## 🛠️ Teknologier

- **Frontend**: Blazor WebAssembly
- **Styling**: Bootstrap 5 + Custom CSS
- **Sprog**: C# + Razor
- **Ikoner**: Font Awesome
- **Framework**: .NET 9

## 📱 Quiz Kategorier

Appen indeholder quizzer organiseret i følgende kategorier:

### 🎓 GF2 - Grundlæggende Programmering
- **GF2 Dag 1** - Dit første program & Variabler
- **GF2 Dag 2** - Expressions & Operatører  
- **GF2 Dag 3** - Control Flow
- **GF2 Dag 4** - Klasser & Objekter
- **GF2 Dag 6** - Arrays, Lists & Dictionary

### 🔧 Backend Teknologier
- **Entity Framework Core** - Relations & LINQ Querying
- **JWT & Token Sikkerhed** - Authentication & Authorization
- **API Calls & CORS** - Web API sikkerhed
- **Caching & Redis** - Performance optimering
- **Error Handling** - Robust fejlhåndtering
- **SOLID Principles** - Software design patterns
- **OOP Advanced** - Avanceret objektorienteret programmering
- **Data Serialization** - JSON, XML og DTOs
- **Rate Limiting** - API beskyttelse
- **XML Comments** - Dokumentation
- **Bogus & Faker** - Test data generering

### 🎨 Frontend Teknologier
- **Blazor** - WebAssembly komponenter
- **CSS** - Styling og layout
- **JavaScript** - Client-side funktionalitet

## 🏃‍♂️ Kom i Gang

### Forudsætninger
- .NET 9 SDK
- En moderne webbrowser
- Docker Desktop (for container deployment)

### Installation
1. Klon repository'et
2. Naviger til `Blazor` mappen
3. Kør `dotnet restore`
4. Kør `dotnet run`
5. Åbn browseren på `https://localhost:5001`

### Bygning
```bash
dotnet build
dotnet run
```

### Docker Deployment
```bash
# Build og start med Docker Compose
docker-compose up --build

# Eller kør i baggrunden
docker-compose up -d --build

# Production deployment
docker-compose -f docker-compose.prod.yml up --build -d
```

Se `DOCKER.md` for detaljerede Docker instruktioner.

## 📁 Projektstruktur

```
videnstjek/
├── Blazor/                      # Blazor applikation
│   ├── Components/
│   │   ├── Layout/
│   │   │   ├── MainLayout.razor
│   │   │   └── NavMenu.razor
│   │   ├── Pages/
│   │   │   ├── Home.razor          # Forside
│   │   │   ├── QuizPage.razor      # Quiz funktionalitet
│   │   │   ├── QuizList.razor      # Quiz oversigt
│   │   │   ├── Admin.razor         # Quiz administration
│   │   │   └── About.razor         # Om siden
│   │   ├── App.razor               # Hovedapp
│   │   └── Routes.razor            # Routing
│   ├── wwwroot/
│   │   ├── data/
│   │   │   └── quizzes/            # Quiz JSON filer
│   │   └── app.css                 # Custom styling
│   ├── Models/
│   │   └── QuizModels.cs           # Quiz data modeller
│   ├── Service/
│   │   ├── QuizService.cs          # Quiz business logic
│   │   └── QuizIndexGenerator.cs   # Automatisk index generering
│   ├── Program.cs                  # App konfiguration
│   └── Dockerfile                  # Docker image definition
├── docker-compose.yml              # Development Docker Compose
├── docker-compose.prod.yml         # Production Docker Compose
├── deploy-docker.ps1               # PowerShell deployment script
├── deploy-docker.bat               # Windows batch deployment script
├── Makefile                        # Linux/macOS deployment
└── DOCKER.md                       # Docker deployment guide
```

## 🎨 Design Features

- **Gradient baggrunde** for moderne udseende
- **Floating animationer** på forsiden
- **Hover effekter** på interaktive elementer
- **Responsivt grid system** til alle skærmstørrelser
- **Konsistent farvepalet** med CSS variabler

## 🔒 Privatliv & Sikkerhed

- Ingen data gemmes på serveren
- Alle svar og resultater forbliver på brugerens enhed
- Ingen tracking eller analytics
- Fokus på læring og ikke på dataindsamling

## 🚧 Fremtidige Forbedringer

- Flere quiz kategorier
- Timer funktionalitet
- Læringsstatistik (lokalt)
- Offline support
- Quiz deling mellem elever

## 🔧 Admin Funktionalitet

Appen indeholder nu en admin-side på `/admin` hvor du kan:
- Se alle eksisterende quizzer i en overskuelig tabel
- Redigere eksisterende quizzer (titel, beskrivelse, spørgsmål, svar)
- Tilføje nye quizzer med multiple-choice spørgsmål
- Slette quizzer
- Administrere spørgsmål med forklaringer på alle svar
- **Generer Index** - Automatisk opdatering af quiz-listen

## 📝 Tilføje Quizzer til Kategorier

### Eksplicit Kategorisering
Quizzer kategoriseres **kun** baseret på det eksplicitte `category` felt i JSON-filen. Der er ingen automatisk mapping - kun eksakte matches:

#### 🎓 GF2 Kategorier
For at tilføje en quiz til GF2-kategorien, sæt `category` til `"GF2"`:
```json
{
  "id": "gf2-dag7-metoder-funktioner",
  "title": "GF2 Dag 7 - Metoder & Funktioner",
  "description": "Test din viden om metoder og funktioner i C#",
  "category": "GF2"
}
```

#### 🔧 Backend Kategorier
For backend-teknologier, sæt `category` til `"Backend"`:
```json
{
  "id": "aspnet-core-webapi",
  "title": "ASP.NET Core Web API",
  "description": "Grundlæggende Web API koncepter",
  "category": "Backend"
}
```

**Backend teknologier:**
- Entity Framework Core
- JWT & Sikkerhed
- API Calls & CORS
- Caching & Redis
- Error Handling
- SOLID Principles
- OOP Advanced
- Data Serialization
- Rate Limiting
- XML Comments
- Bogus & Faker

#### 🎨 Frontend Kategorier
For frontend-teknologier, sæt `category` til `"Frontend"`:
```json
{
  "id": "blazor-components",
  "title": "Blazor Komponenter",
  "description": "Grundlæggende Blazor komponenter",
  "category": "Frontend"
}
```

**Frontend teknologier:**
- Blazor WebAssembly
- CSS & Styling
- JavaScript

### Quiz JSON Struktur

#### Enkelt Kategori (Gammel format - stadig understøttet)
```json
{
  "id": "quiz-navn",
  "title": "Quiz Titel",
  "description": "Beskrivelse af quizzen",
  "category": "GF2|Backend|Frontend",
  "difficulty": "Begynder|Mellem|Avanceret",
  "estimatedTime": "5-10 minutter",
  "questions": [...]
}
```

#### Multi-Kategorier (Nyt format - anbefalet)
```json
{
  "id": "quiz-navn",
  "title": "Quiz Titel",
  "description": "Beskrivelse af quizzen",
  "categories": ["Frontend", "Backend"],
  "difficulty": "Begynder|Mellem|Avanceret",
  "estimatedTime": "5-10 minutter",
  "questions": [
    {
      "id": 1,
      "questionText": "Dit spørgsmål?",
      "options": {
        "A": "Svar 1",
        "B": "Svar 2", 
        "C": "Svar 3",
        "D": "Svar 4"
      },
      "correctAnswers": ["A"],
      "explanations": {
        "A": "Forklaring af det korrekte svar",
        "B": "Hvorfor dette svar er forkert",
        "C": "Hvorfor dette svar er forkert",
        "D": "Hvorfor dette svar er forkert"
      }
    }
  ]
}
```

### ⚠️ Vigtigt om Kategorisering

#### Enkelt Kategori
- **Kun eksakte matches:** `"category": "GF2"` → GF2 navigation
- **Ingen automatisk mapping:** `"category": "C#"` → "Andet" kategori
- **Tom kategori:** `"category": ""` → "Andet" kategori
- **Ukendt kategori:** `"category": "Custom"` → "Andet" kategori

#### Multi-Kategorier
- **Flere kategorier:** `"categories": ["Frontend", "Backend"]` → Vises i begge navigationer
- **Backward compatibility:** Gamle `"category"` felt migreres automatisk til `"categories"`
- **Kombination:** Både `"category"` og `"categories"` kan bruges samtidig
- **Eksempel:** En Blazor & SignalR quiz kan være både Frontend og Backend

### Automatisk Index Generering
- Quizzer scannes automatisk fra `wwwroot/data/quizzes/` mappen
- Ingen manuel `index.json` vedligeholdelse nødvendig
- Brug "Generer Index" knappen i admin-panelet for at opdatere

## 📝 Licens

Dette projekt er udviklet til undervisningsformål.

## 🤝 Bidrag

Velkommen til at bidrage med:
- Nye quiz spørgsmål
- UI/UX forbedringer
- Bug fixes
- Dokumentation

---

**Videnstjek** - Din personlige læringsassistent til at teste og forbedre din viden! 🧠✨
