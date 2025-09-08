# 🧮 **Lommeregner med Historik**

**Introduktion:**

I denne opgave skal I bygge en simpel lommeregner med Blazor, der kan udføre grundlæggende matematiske operationer og gemme en historik over alle beregninger. Det er en perfekt opgave til at lære grundlæggende Blazor komponenter, event handling og state management.

---

## **Opgavebeskrivelse:**

### **1. Grundlæggende lommeregner:**
- Opret en Blazor Server eller WebAssembly applikation
- Implementer de fire grundlæggende regnearter: +, -, *, /
- Vis det aktuelle tal og resultat på skærmen
- Håndter input via knapper (0-9, operatorer, =, clear)

### **2. Beregningslogik:**
- Korrekt håndtering af operatorrækkefølge
- Fejlhåndtering (division med nul, ugyldige operationer)
- Clear funktion til at nulstille lommeregneren
- Decimal tal support (komma/punktum)

### **3. Historik funktionalitet:**
- Gem alle udførte beregninger i en liste
- Vis historikken i en sidebar eller separat sektion
- Mulighed for at slette historik
- Klik på historik element for at genbruge resultatet

---

## **Tekniske krav:**

### **Blazor komponenter:**
```csharp
// Eksempel på properties I skal bruge
public string CurrentDisplay { get; set; } = "0";
public List<string> CalculationHistory { get; set; } = new();
public decimal CurrentValue { get; set; } = 0;
public string CurrentOperation { get; set; } = "";
```

### **Metoder I skal implementere:**
- `NumberClick(int number)` - Håndter tal input
- `OperatorClick(string operation)` - Håndter operator valg
- `CalculateResult()` - Udfør beregning og vis resultat
- `ClearCalculator()` - Nulstil lommeregneren
- `ClearHistory()` - Slet historik

---

## **Design krav:**

### **Lommeregner layout:**
- Grid layout med tal knapper (3x4 grid)
- Operator knapper på siden eller bunden
- Stort display område for tal og resultater
- Clear og equals knapper

### **Historik sektion:**
- Liste over tidligere beregninger
- Format: "5 + 3 = 8"
- Scroll funktion hvis mange beregninger
- Slet knap for hver beregning eller slet alt

---

## **Udvidelser for de modige:**

1. **Avancerede operationer:** Kvadratrod, procent, potens
2. **Keyboard support:** Brug tastaturet til input
3. **Tema skifter:** Lys/mørk tema eller farve temaer
4. **Gem historik:** Brug localStorage til at gemme mellem sessioner
5. **Eksporter historik:** Download historik som tekstfil
6. **Videnskabelig lommeregner:** Sin, cos, tan, logaritmer
7. **Enheds omregner:** Længde, vægt, temperatur

---

## **Læringsmål:**

- Grundlæggende Blazor komponenter og syntax
- Event handling med @onclick
- Data binding med @bind og properties
- State management i Blazor
- CSS styling og responsive design
- Fejlhåndtering og validering
- Arbejde med lister og collections

---

## **Eksempel på brugergrænsefladen:**

```
┌─────────────────────────────┐
│         123.45              │ ← Display
├─────────────────────────────┤
│  7  │  8  │  9  │    /     │
├─────┼─────┼─────┼──────────┤
│  4  │  5  │  6  │    *     │
├─────┼─────┼─────┼──────────┤
│  1  │  2  │  3  │    -     │
├─────┼─────┼─────┼──────────┤
│  0  │  .  │  =  │    +     │
├─────┴─────┴─────┼──────────┤
│      Clear       │          │
└──────────────────┴──────────┘

Historik:
• 10 + 5 = 15
• 15 * 2 = 30  
• 30 / 6 = 5
```

---

## **Afleveringsform:**

### **Live demonstration (3-5 minutter):**
- Vis grundlæggende regneoperationer
- Demonstrer historik funktionalitet
- Test fejlhåndtering (division med nul)
- Vis jeres design og eventuelle udvidelser

### **Kode på GitHub:**
- Kommenteret kode der forklarer logikken
- README med screenshots af jeres lommeregner
- Beskrivelse af hvilke udfordringer I mødte

---

## **Tips til implementering:**

1. **Start simpelt:** Byg først en lommeregner der kan lægge to tal sammen
2. **Test løbende:** Sørg for hver operation virker før I går videre
3. **Tænk på edge cases:** Hvad sker der ved division med nul?
4. **Styling til sidst:** Få funktionaliteten til at virke først
5. **Brug Blazor debugging:** Console.WriteLine() er jeres ven!

**God fornøjelse med at bygge jeres første interaktive Blazor applikation! 🚀**