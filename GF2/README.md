# GF2-CSharp

En omfattende C# læringsplatform for GF2 (Grundforløb 2) med fokus på praktisk programmering og projektbaseret læring.

## 📖 Læringsvej: Bogen + opgaver

**C#-bogen** ([CSharp-Bogen/](CSharp-Bogen/CSharp-Bogen.md)) er teorien; **opgaverne** ([Opgaver/](Opgaver/)) er den praktiske del. Brug dem sammen:

| Opgave | Emne | Læs i bogen |
|--------|------|--------------|
| [1. Variabler](Opgaver/1.%20Variabler.cs) | Datatyper, string interpolation | [2. Variabler](CSharp-Bogen/2.%20Variabler.md), [1. Dit første program](CSharp-Bogen/1.%20Dit%20første%20program.md) |
| [2. Inputs](Opgaver/2.%20Inputs.cs) | Console.ReadLine, Parse | [3.5 Inputs](CSharp-Bogen/3.5%20Inputs.md), [3. Expressions](CSharp-Bogen/3.%20Expressions%20og%20operatører.md) |
| [3. Control Flow](Opgaver/3.ControlFlow.cs) | if/else, switch, ternary | [5. Control Flow](CSharp-Bogen/5.%20Control%20Flow.md) |
| [4. Loops](Opgaver/4.Loops.cs) | for, while, do-while, foreach | [7. Loops og iterationer](CSharp-Bogen/7.%20Loops%20og%20iterationer.md) |
| [5. Arrays](Opgaver/5.Arrays.cs) | Array, List, Dictionary | [4. Arrays, Lists & Dictionary](CSharp-Bogen/4.%20Arrays%2C%20Lists%20%26%20Dictionary.md) |
| [6. Methods](Opgaver/6.Methods.cs) | Metoder, parametre, rekursion | [8. Metoder - Funktioner](CSharp-Bogen/8.%20Metoder%20-%20Funktioner.md) |
| [7. Classes](Opgaver/7.Classes.cs) | Klasser, properties, arv | [6. Klasser og Objekter](CSharp-Bogen/6.%20Klasser%20og%20Objekter.md) |

**Vejledende løsninger** findes i [Opgaver/Solutions/](Opgaver/Solutions/) – brug dem kun hvis du er stødt på problemer.

**Kør opgaverne:** [Opgaver/Program.cs](Opgaver/Program.cs)

---

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

## 🖱️ Editor og udviklingsmiljø

Du kan arbejde med opgaver og projekterne i **Visual Studio** (fuld IDE) eller **VS Code** – begge understøtter C# og .NET fint.

**På GF2** bruger vi **GitHub Codespaces** for nemhedens skyld. Her er en kort guide:

### Hvad er GitHub Codespaces?

**Codespaces** er GitHub's cloud-baserede udviklingsmiljø: du får en fuld Linux-maskine i skyen med VS Code-oplevelsen i browseren (eller i VS Code-appen). Du behøver ikke installere .NET, Git eller andet på din egen computer – det er allerede sat op i Codespace'en. Perfekt til skolebrug, når alle skal kunne køre samme kode uden forskellige opsætninger.

### Sådan åbner du GF2 i Codespaces

1. **Åbn repo'et på GitHub** (fx det repo, I bruger til GF2).
2. Klik på den grønne **"Code"**-knap øverst til højre.
3. Vælg fanen **"Codespaces"**.
4. Klik **"Create codespace on main"** (eller vælg en anden branch).  
   – GitHub starter en ny Codespace; det tager typisk 1–2 minutter første gang.
5. Når den er klar, åbnes VS Code i browseren med hele projektet. Du kan nu fx:
   - Åbne terminalen (`` Ctrl+` `` eller **Terminal → New Terminal**)
   - Køre opgaverne: `cd Opgaver` og derefter `dotnet run`
   - Redigere filer som i VS Code

**Tip:** Du kan også bruge **"Code" → "Open in GitHub Codespaces"** fra repo-siden, hvis du foretrækker den vej.

Når du er færdig, lukker du bare fanen eller stopper Codespace'en under **github.com → Your codespaces** for at spare ressourcer.

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
├── CSharp-Bogen/    # Teori og lærebog (kapitler 1–11)
├── Opgaver/         # Grundlæggende programmeringsopgaver + Solutions/
├── Projekter/       # Praktiske projekter (Blazor, Konsol)
├── WPF/             # Desktop applikationer
├── Teori/           # Teoretiske eksempler og demonstrationskode
├── UnitTest/        # Automatiserede tests
└── README.md        # Denne fil
```

Dette repository danner grundlag for en omfattende C# læringsrejse fra grundlæggende koncepter til avancerede virksomhedsløsninger.
