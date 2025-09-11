# Videnstjek - Interaktiv Quiz App

> [!info] Projekt Oversigt
> Videnstjek er en moderne, interaktiv quiz-app bygget med Blazor WebAssembly til at hjælpe elever med at teste deres viden. Appen er designet med fokus på læring og giver detaljerede forklaringer på alle svar.



---

## 🎯 Formål og Mål

> [!multi-column]
> 
> Videnstjek er designet til at give elever mulighed for at:
> **Teste deres viden** med kvalitetsfulde spørgsmål
> **Få detaljerede forklaringer** på alle svar
> **Lære af deres fejl** og forbedre sig
> **Øve sig så meget de vil** uden begrænsninger
> 
> > ![[Pasted image 20250911220518.png]]
> > 
> > **Vigtigt:** Appen gemmer ingen resultater eller svar - alt forbliver privat på brugerens enhed.

---

## 🚀 Funktioner og Features

### 🎨 Brugeroplevelse
- **Moderne UI/UX**: Responsivt design der fungerer på alle enheder
- **Interaktive Quizzer**: Engagerende spørgsmål med multiple-choice svar
- **Progress Tracking**: Visuel fremgangsindikator gennem quizzen
- **Fremtrædende Resultater**: Quiz-afslutning vises som en centreret overlay med animerede elementer
- **Spørgsmål Oversigt**: Detaljeret oversigt over alle spørgsmål med farvekodning (grøn/rød) for korrekte/forkerte svar

![[Pasted image 20250911220518.png]]

### 📚 Indholdsstyring
- **Kategoriserede Quizzer**: Organiseret i GF2, Backend og Frontend kategorier
- **Intelligent Navigation**: Filtrer quizzer efter kategori med URL-parametre
- **Søgefunktionalitet**: Find specifikke quizzer hurtigt
- **Automatisk Indexering**: Ingen manuel vedligeholdelse af quiz-lister

### 🔧 Administration
- **Admin Panel**: Komplet administration af quizzer på `/admin`
- **Quiz Redigering**: Rediger eksisterende quizzer (titel, beskrivelse, spørgsmål, svar)
- **Nye Quizzer**: Tilføj nye quizzer med multiple-choice spørgsmål
- **Quiz Sletning**: Slet quizzer efter behov
- **Index Generering**: Automatisk opdatering af quiz-listen

---

## 🛠️ Teknisk Arkitektur

### Frontend Teknologier
- **Blazor WebAssembly**: Moderne C# frontend framework
- **Bootstrap 5**: Responsivt CSS framework
- **Custom CSS**: Avanceret styling med CSS variabler
- **Font Awesome**: Ikoner og visuelle elementer
- **JavaScript**: Client-side funktionalitet for interaktioner

### Backend & Data
- **.NET 9**: Seneste .NET framework
- **JSON Data**: Quizzer gemmes som JSON filer
- **File-based Storage**: Ingen database - alt data i filer
- **Automatic Indexing**: Dynamisk indlæsning af quizzer

### Deployment
- **Docker Support**: Containerisering med Docker Compose
- **Multi-stage Build**: Optimal image størrelse
- **Production Ready**: Separate dev/prod konfigurationer
- **Health Checks**: Automatisk overvågning

---

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
│   │   └── Service/
│   │       └── NavigationService.cs
│   ├── Models/
│   │   └── QuizModels.cs           # Quiz data modeller
│   ├── Service/
│   │   ├── QuizService.cs          # Quiz business logic
│   │   └── QuizIndexGenerator.cs   # Automatisk index generering
│   └── wwwroot/
│       ├── data/quizzes/            # Quiz JSON filer
│       ├── app.css                 # Custom styling
│       └── js/                     # JavaScript funktionalitet
├── docker-compose.yml              # Development Docker Compose
├── docker-compose.prod.yml         # Production Docker Compose
├── Dockerfile                      # Docker image definition
└── DOCKER.md                       # Docker deployment guide
```

---

## 📱 Quiz Kategorier

![[Pasted image 20250911220539.png]]


### 🎓 GF2 - Grundlæggende Programmering
- **GF2 Dag 1** - Dit første program & Variabler
- **GF2 Dag 2** - Expressions & Operatører  
- **GF2 Dag 3** - Control Flow
- **GF2 Dag 4** - Klasser & Objekter
- **GF2 Dag 6** - Arrays, Lists & Dictionary
- **GF2 Dag 7** - Loops & Iterationer
- **GF2 Kapitel 8** - Metoder & Funktioner

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

---

## 🏃‍♂️ Kom i Gang

### Forudsætninger
- .NET 9 SDK
- En moderne webbrowser
- Docker Desktop (for container deployment)

### Lokal Udvikling
```bash
# Klon repository'et
git clone <repository-url>
cd videnstjek

# Naviger til Blazor mappen
cd Blazor

# Restore dependencies
dotnet restore

# Kør applikationen
dotnet run

# Åbn browseren på https://localhost:5001
```

### Docker Deployment
```bash
# Development
docker-compose up --build

# Production
docker-compose -f docker-compose.prod.yml up --build -d
```

---

## 🔒 Privatliv & Sikkerhed

> [!tip] Databeskyttelse
> Videnstjek er designet med fokus på privatliv:
> 
> - **Ingen data gemmes** på serveren
> - **Alle svar og resultater** forbliver på brugerens enhed
> - **Ingen tracking** eller analytics
> - **Fokus på læring** og ikke på dataindsamling

---

## 📝 Quiz JSON Struktur

### Enkelt Kategori Format
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

### Multi-Kategorier Format (Anbefalet)
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
      },
      "learnMore": {
        "title": "Læs mere om emnet",
        "description": "Yderligere ressourcer",
        "url": "https://example.com",
        "type": "docs",
        "icon": "fas fa-book"
      }
    }
  ]
}
```

---

## 🎨 Design Features

### Visuelle Elementer
- **Gradient baggrunde** for moderne udseende
- **Floating animationer** på forsiden
- **Hover effekter** på interaktive elementer
- **Responsivt grid system** til alle skærmstørrelser
- **Konsistent farvepalet** med CSS variabler

### Interaktive Elementer
- **Shuffled Options**: Spørgsmålssvar blandes automatisk
- **Progress Indicators**: Visuel fremgang gennem quizzen
- **Animated Results**: Smidige overgange mellem spørgsmål
- **Keyboard Navigation**: Støtte for tastatur navigation

---

## 🚧 Fremtidige Forbedringer

### Planlagte Features
- **Flere quiz kategorier** - Udvidelse af emneområder
- **Timer funktionalitet** - Tidsbegrænsede quizzer
- **Læringsstatistik** - Lokal statistik over fremskridt
- **Offline support** - Fungerer uden internetforbindelse
- **Quiz deling** - Del quizzer mellem elever

### Tekniske Forbedringer
- **Performance optimering** - Hurtigere indlæsning
- **Accessibility** - Bedre tilgængelighed
- **Mobile app** - Native mobile version
- **API integration** - Eksterne data kilder

---

## 🔧 Admin Funktionalitet

### Quiz Administration
- **Oversigt**: Se alle eksisterende quizzer i en overskuelig tabel
- **Redigering**: Rediger eksisterende quizzer (titel, beskrivelse, spørgsmål, svar)
- **Oprettelse**: Tilføj nye quizzer med multiple-choice spørgsmål
- **Sletning**: Slet quizzer efter behov
- **Index Generering**: Automatisk opdatering af quiz-listen

### Spørgsmål Administration
- **Multiple Choice**: Støtte for A, B, C, D svar
- **Forklaringer**: Detaljerede forklaringer på alle svar
- **Læringsressourcer**: Links til yderligere materiale
- **Sværhedsgrad**: Indstil sværhedsgrad for hver quiz

---

## 📊 Performance og Skalering

### Optimeringer
- **Lazy Loading**: Quizzer indlæses kun når nødvendigt
- **Caching**: Intelligent caching af quiz data
- **Compression**: Optimerede assets og data
- **CDN Ready**: Klar til content delivery network

### Skalering
- **Horizontal Scaling**: Flere container instanser
- **Load Balancing**: Fordeling af trafik
- **Database Migration**: Fremtidig overgang til database
- **Microservices**: Opdeling i mindre services

---

## 🤝 Bidrag og Udvikling

### Hvordan bidrage
- **Nye quiz spørgsmål** - Tilføj kvalitetsfulde spørgsmål
- **UI/UX forbedringer** - Forbedre brugeroplevelsen
- **Bug fixes** - Rapporter og ret fejl
- **Dokumentation** - Forbedre dokumentationen

### Udviklingsmiljø
- **Git Workflow** - Standard Git branching
- **Code Review** - Alle ændringer gennemgås
- **Testing** - Automatiseret testing
- **CI/CD** - Kontinuerlig integration og deployment

---

## 📝 Licens og Brug

> [!note] Licens
> Dette projekt er udviklet til undervisningsformål og er frit tilgængeligt for uddannelsesinstitutioner.

---

## 🔗 Ressourcer

- **GitHub Repository**: [Videnstjek Repo](https://github.com/Mercantech/videnstjek)
- **Docker Guide**: Se `DOCKER.md` for detaljerede Docker instruktioner
- **Blazor Dokumentation**: [Microsoft Blazor Docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- **Bootstrap 5**: [Bootstrap Dokumentation](https://getbootstrap.com/docs/5.0/)

---

> [!success] Status
> **Videnstjek** er et aktivt udviklet projekt med fokus på at skabe den bedste læringsoplevelse for elever gennem interaktive quizzer! 🧠✨