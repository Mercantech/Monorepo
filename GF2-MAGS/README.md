# GF2-CSharp

En omfattende C# læringsplatform for GF2 (Grundforløb 2) med fokus på praktisk programmering og projektbaseret læring.

## 📚 Struktur

Dette repository indeholder en komplet C# læringsmiljø med opgaver, projekter, teori og praktiske eksempler organiseret i følgende hovedkategorier:

### 🎯 Opgaver
Et interaktivt konsolprogram med 7 grundlæggende opgavesæt der dækker:
- **[Variabler](Opgaver/1.%20Variabler.cs)** - Datatyper, deklaration og initialisering
- **[Inputs](Opgaver/2.%20Inputs.cs)** - Brugerinput og databehandling
- **[Control Flow](Opgaver/3.ControlFlow.cs)** - If/else, switch og ternary operatorer
- **[Loops](Opgaver/4.Loops.cs)** - For, while og foreach løkker
- **[Arrays](Opgaver/5.Arrays.cs)** - Arrays, List og Dictionary samlinger
- **[Methods](Opgaver/6.Methods.cs)** - Metodeoprettelse og parameterhåndtering
- **[Classes](Opgaver/7.Classes.cs)** - Objektorienteret programmering og klasser

**Ekstra projekter:**
- [Sten, Saks, Papir spil](Opgaver/RockPaperScissors/)
- [Binær/Decimal konvertering](Opgaver/BinaryConverter/)
- [Banko spil](Opgaver/Banko/)

**Kør opgaverne:** [Program.cs](Opgaver/Program.cs)

### 🏠 Projekter
Praktiske projekter organiseret efter kontekst:

#### Hjemmet
Små tidsfordrivsspil til hjemmekontoret:
- Gæt et tal
- Sten, Saks, Papir
- Tic-Tac-Toe
- TypeRacer
- Wordle
- Connect Four
- Cookie Clicker

#### Kontoret
Kontorværktøjer og Blazor webapplikationer:
- Binærkodeomformer
- Informationsside i Blazor
- Brugerdefinerede kontorværktøjer

#### Enterprise
Avancerede virksomhedsløsninger:
- Active Directory integration
- Bruger- og gruppeoversigt
- Stemple ind/ud-system
- Netværksovervågning

### 🖥️ WPF Applikationer
Desktop applikationer med moderne UI:
- [Hovedmenu](WPF/MainWindow.xaml) - Interaktiv navigation
- **Spil:**
  - [TicTacToe](WPF/TicTacToe.xaml)
  - [Wordle](WPF/Wordle.xaml)
  - [TypeRacer](WPF/TypeRacer.xaml)
  - [Connect Four](WPF/ConnectFour.xaml)
  - [Cookie Clicker](WPF/CookieClicker.xaml)
- **Værktøjer:**
  - [Binary Converter](WPF/Binary.xaml)
  - [Taxa Calculator](WPF/Taxa.xaml)
  - [Hr. Gran](WPF/HrGran.xaml)
- Animeret fyrværkeri-effekt i hovedmenuen

### 🧪 Unit Testing
Omfattende test suite med NUnit:
- [Test suite](UnitTest/Testing.cs) - Automatiserede tests for alle opgavesæt
- Test af variabelhåndtering
- Validering af metoder og klasser

### 📖 Teori
Teoretiske eksempler og demonstrationskode:
- [JSON eksempler](Teori/TeoriEmner/JSON.cs) - Håndtering og serialisering
- [SQL eksempler](Teori/TeoriEmner/SQL.cs) - (under udvikling)
- [LINQ eksempler](Teori/TeoriEmner/LINQ.cs) - (under udvikling)
- [Hovedprogram](Teori/Program.cs) - Praktiske kodeeksempler med kommentarer

## 🚀 Kom i gang

1. **Kør opgaverne:** [Program.cs](Opgaver/Program.cs) - Interaktivt konsolprogram
2. **Udforsk projekterne:** [Projekter/](Projekter/) - Praktiske eksempler
3. **Test din kode:** [UnitTest/Testing.cs](UnitTest/Testing.cs) - Verificer din forståelse
4. **Lær teorien:** [Teori/Program.cs](Teori/Program.cs) - Dybere forståelse
5. **Prøv WPF apps:** [WPF/MainWindow.xaml](WPF/MainWindow.xaml) - Desktop applikationer

## 🛠️ Teknologier

- **.NET 8.0** - Moderne C# framework
- **WPF** - Windows Presentation Foundation for desktop apps
- **Blazor** - Web applikationer med C#
- **NUnit** - Unit testing framework
- **Docker** - Containerisering support
- **JSON** - Data serialisering og håndtering

## 📁 Projektstruktur

```
GF2/
├── Opgaver/         # Grundlæggende programmeringsopgaver
├── Projekter/       # Praktiske projekter (Blazor, Konsol)
├── WPF/             # Desktop applikationer
├── Teori/           # Teoretiske eksempler og demonstrationskode
├── UnitTest/        # Automatiserede tests
└── README.md        # Denne fil
```

Dette repository danner grundlag for en omfattende C# læringsrejse fra grundlæggende koncepter til avancerede virksomhedsløsninger.
