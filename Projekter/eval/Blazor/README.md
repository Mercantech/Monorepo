# Evalueringsplatform Admin Dashboard

Dette er et admin dashboard til at administrere evalueringsundersøgelser med avanceret forgrening baseret på ratings og andre spørgsmålstyper.

## 🚀 Funktioner

### 📊 Survey Management
- **Opret undersøgelser** med unik 4-cifret adgangskode
- **Rediger undersøgelser** med titel, beskrivelse og udløbsdato
- **Aktivér/deaktivér** undersøgelser
- **Oversigt** over alle undersøgelser med statistikker

### ❓ Spørgsmålstyper
- **Tekst** - Fritekst svar
- **Flervalg** - Flere valgmuligheder
- **Enkeltvalg** - En valgmulighed
- **Bedømmelse** - Rating 1-5, 1-10, etc.
- **Skala** - Skala spørgsmål
- **Ja/Nej** - Binært svar
- **Dato** - Dato spørgsmål
- **Tal** - Tal spørgsmål
- **Email** - Email spørgsmål

### 🌳 Avanceret Forgrening
- **Rating-baseret forgrening** - Vis spørgsmål baseret på rating svar
- **Tekst-baseret forgrening** - Vis spørgsmål baseret på tekstindhold
- **Valg-baseret forgrening** - Vis spørgsmål baseret på valgte muligheder
- **Dato-baseret forgrening** - Vis spørgsmål baseret på datoer
- **Tal-baseret forgrening** - Vis spørgsmål baseret på tal

### 📈 Dynamisk Data
- **JSON storage** - Alle svar gemmes som JSON for maksimal fleksibilitet
- **Søgbare felter** - Separate felter for nem søgning og filtrering
- **Ingen database ændringer** - Tilføj nye spørgsmålstyper uden at ændre databasen

## 🛠️ Teknisk Stack

- **.NET 9** - Backend framework
- **Blazor Server** - Frontend framework
- **Entity Framework Core** - ORM
- **PostgreSQL** - Database med JSONB support
- **Bootstrap 5** - UI framework

## 📁 Projektstruktur

```
Blazor/
├── Data/
│   ├── Models/
│   │   ├── Survey.cs              # Undersøgelse model
│   │   ├── Question.cs            # Spørgsmål model
│   │   ├── QuestionCondition.cs   # Forgrening model
│   │   ├── Response.cs            # Svar model
│   │   ├── ResponseData.cs        # Svar data model
│   │   ├── QuestionType.cs        # Spørgsmålstyper enum
│   │   ├── QuestionOption.cs      # Valgmuligheder model
│   │   └── ResponseValue.cs       # Svar værdier model
│   └── ApplicationDbContext.cs    # Database context
├── Services/
│   └── SurveyService.cs           # Business logic service
├── Components/
│   └── Pages/
│       └── Admin/
│           ├── Index.razor        # Admin dashboard
│           ├── SurveyBuilder.razor # Survey builder
│           ├── QuestionEditor.razor # Spørgsmål editor
│           └── ConditionBuilder.razor # Forgrening builder
└── Program.cs                     # App konfiguration
```

## 🎯 Eksempel på Forgrening

### Scenario: Kursus Evaluering
1. **Spørgsmål 1**: "Hvor tilfreds er du med kurset?" (Rating 1-5)
2. **Spørgsmål 2**: "Hvad kunne forbedres?" (Tekst) - **Vises kun hvis rating ≤ 3**
3. **Spørgsmål 3**: "Vil du anbefale kurset?" (Ja/Nej) - **Vises kun hvis rating ≥ 4**
4. **Spørgsmål 4**: "Hvorfor vil du anbefale det?" (Tekst) - **Vises kun hvis anbefaling = Ja**

### Konfiguration
- **Spørgsmål 2**: Condition = Rating ≤ 3
- **Spørgsmål 3**: Condition = Rating ≥ 4  
- **Spørgsmål 4**: Condition = Ja/Nej = Ja

## 🚀 Kom i Gang

1. **Kør projektet**:
   ```bash
   dotnet run
   ```

2. **Gå til admin dashboard**:
   ```
   https://localhost:5001/admin
   ```

3. **Opret din første undersøgelse**:
   - Klik "Ny Undersøgelse"
   - Udfyld titel og beskrivelse
   - Tilføj spørgsmål
   - Konfigurer forgrening

4. **Test undersøgelsen**:
   - Brug adgangskoden til at besvare undersøgelsen
   - URL: `https://localhost:5001/survey/{adgangskode}`

## 📊 Database Schema

### Surveys Table
- `Id` - Primary key
- `Title` - Undersøgelse titel
- `Description` - Beskrivelse
- `AccessCode` - 4-cifret adgangskode (unik)
- `IsActive` - Om undersøgelsen er aktiv
- `CreatedAt` - Oprettelsesdato
- `ExpiresAt` - Udløbsdato

### Questions Table
- `Id` - Primary key
- `SurveyId` - Foreign key til Survey
- `Text` - Spørgsmålstekst
- `Type` - Spørgsmålstype enum
- `Options` - JSON med valgmuligheder
- `MinValue/MaxValue` - For rating/scale spørgsmål
- `Order` - Rækkefølge i undersøgelsen

### QuestionConditions Table
- `Id` - Primary key
- `QuestionId` - Spørgsmål der skal vises
- `ParentQuestionId` - Spørgsmål der evalueres
- `Type` - Betingelsestype enum
- `Value` - Værdi at sammenligne med
- `Operator` - Operator (equals, greater_than, etc.)

### Responses Table
- `Id` - Primary key
- `SurveyId` - Foreign key til Survey
- `StudentName` - Elevens navn (valgfri)
- `StudentEmail` - Elevens email (valgfri)
- `SubmittedAt` - Indsendelsestidspunkt
- `IpAddress` - IP-adresse

### ResponseData Table
- `Id` - Primary key
- `ResponseId` - Foreign key til Response
- `QuestionId` - Foreign key til Question
- `Data` - JSON med svar data
- `TextValue` - Tekst svar (for søgning)
- `NumericValue` - Tal svar (for søgning)
- `DateValue` - Dato svar (for søgning)
- `BooleanValue` - Ja/Nej svar (for søgning)

## 🔧 Avancerede Funktioner

### Custom Operators
Du kan bruge custom operators i conditions:
- `equals` - Lige med
- `greater_than` - Større end
- `less_than` - Mindre end
- `contains` - Indeholder
- `between` - Mellem to værdier

### JSON Data Struktur
Svar gemmes som JSON med følgende struktur:
```json
{
  "text": "Fritekst svar",
  "number": 5,
  "date": "2024-01-01",
  "boolean": true,
  "selectedOptions": ["option1", "option2"],
  "selectedOption": "option1"
}
```

### Performance Optimering
- **Indexes** på alle foreign keys
- **JSONB** for effektiv JSON søgning
- **Separate søgbare felter** for almindelige queries
- **Cascade delete** for data integritet

## 🎨 UI Komponenter

### Admin Dashboard
- Oversigt over alle undersøgelser
- Hurtig statistik (spørgsmål, svar, adgangskode)
- Dropdown menuer for handlinger
- Responsive design

### Survey Builder
- Drag-and-drop interface (kommende)
- Live forhåndsvisning
- Spørgsmål rækkefølge
- Forgrening indikatorer

### Question Editor
- WYSIWYG editor
- Spørgsmålstype valg
- Valgmuligheder management
- Forhåndsvisning

### Condition Builder
- Visuelt condition builder
- Parent question valg
- Operator konfiguration
- Live forhåndsvisning

## 🔒 Sikkerhed

- **Ingen login** - Kun adgangskode beskyttelse
- **IP tracking** - Undgå duplikate svar
- **Data validering** - Server-side validering
- **SQL injection** - Entity Framework protection
- **XSS protection** - Razor auto-escaping

## 📈 Fremtidige Forbedringer

- [ ] Drag-and-drop survey builder
- [ ] Real-time analytics dashboard
- [ ] Export til Excel/CSV
- [ ] Email notifikationer
- [ ] Multi-language support
- [ ] Advanced reporting
- [ ] API endpoints
- [ ] Mobile app

## 🤝 Bidrag

Dette er et internt projekt, men feedback og forslag er velkomne!

## 📞 Support

For spørgsmål eller problemer, kontakt udviklingsteamet.
