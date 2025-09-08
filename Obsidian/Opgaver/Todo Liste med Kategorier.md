# 📝 **Todo Liste med Kategorier**

**Introduktion:**

I denne opgave skal I bygge en interaktiv todo liste applikation med Blazor, hvor brugere kan oprette, organisere og administrere deres opgaver i forskellige kategorier. Det er en fantastisk måde at lære grundlæggende CRUD operationer, list manipulation og brugerinteraktion i Blazor.

---

## **Opgavebeskrivelse:**

### **1. Grundlæggende todo funktionalitet:**
- Opret en Blazor Server eller WebAssembly applikation
- Tilføj nye todo opgaver med titel og beskrivelse
- Marker opgaver som færdige/ikke færdige
- Slet opgaver fra listen
- Vis antal færdige vs. ikke færdige opgaver

### **2. Kategori system:**
- Opret forskellige kategorier (Arbejde, Privat, Indkøb, etc.)
- Tildel hver opgave til en kategori
- Filtrer opgaver baseret på kategori
- Vis opgaver grupperet efter kategori

### **3. Brugergrænsefladen:**
- Input felt til nye opgaver
- Dropdown til kategori valg
- Checkbox til at markere opgaver som færdige
- Knapper til at slette opgaver
- Filter knapper for kategorier

---

## **Tekniske krav:**

### **Data modeller:**
```csharp
public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Category { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class TodoCategory
{
    public string Name { get; set; }
    public string Color { get; set; }
    public int TaskCount { get; set; }
}
```

### **Metoder I skal implementere:**
- `AddTodoItem(string title, string description, string category)`
- `ToggleComplete(int todoId)`
- `DeleteTodoItem(int todoId)`
- `FilterByCategory(string category)`
- `GetCompletedCount()` og `GetPendingCount()`

---

## **Design krav:**

### **Layout struktur:**
- Header med titel og statistikker
- Input sektion til nye opgaver
- Kategori filter knapper
- Todo liste med gruppering
- Footer med samlet oversigt

### **Visuel feedback:**
- Forskellige farver for kategorier
- Gennemstreget tekst for færdige opgaver
- Hover effekter på knapper
- Responsive design til mobile enheder

---

## **Udvidelser for de modige:**

1. **Prioritering:** Tilføj høj/medium/lav prioritet til opgaver
2. **Deadline:** Tilføj forfaldsdato og vis overdue opgaver
3. **Søgning:** Søg i opgave titler og beskrivelser
4. **Sortering:** Sorter efter dato, prioritet eller alfabetisk
5. **Drag & Drop:** Træk opgaver mellem kategorier
6. **Statistikker:** Vis grafer over produktivitet
7. **Export/Import:** Gem og indlæs todo lister som JSON
8. **Dark mode:** Skift mellem lys og mørk tema

---

## **Læringsmål:**

- Arbejde med lister og collections i Blazor
- Implementere CRUD operationer
- Bruge conditional rendering (@if, foreach)
- Event handling og form validation
- State management og data binding
- CSS styling og responsive design
- Arbejde med DateTime og data formatering

---

## **Eksempel på brugergrænsefladen:**

```
┌─────────────────────────────────────────┐
│           📝 Min Todo Liste             │
│    Færdige: 5            |  Total: 8    │
├─────────────────────────────────────────┤
│ Ny opgave: [________________] [Tilføj]  │
│ Kategori:  [Arbejde] Beskrivelse: [...] │
├─────────────────────────────────────────┤
│ [Alle] [Arbejde] [Privat] [Indkøb]      │
├─────────────────────────────────────────┤
│ 🔵 ARBEJDE                              │
│ ☑️ Færdiggør rapport        [Slet]      │
│ ☐ Møde med klient           [Slet]      │
│                                         │
│ 🟢 PRIVAT                               │
│ ☐ Køb mælk                  [Slet]      │
│ ☑️ Ring til mor             [Slet]      │
└─────────────────────────────────────────┘
```

---

## **Afleveringsform:**

### **Live demonstration (3-5 minutter):**
- Vis hvordan man tilføjer nye opgaver
- Demonstrer kategori funktionalitet
- Test færdig/ikke færdig toggle
- Vis filter og sortering features
- Præsenter jeres design valg

### **Kode på GitHub:**
- Struktureret kode med kommentarer
- README med setup guide og screenshots
- Beskrivelse af udfordringer og løsninger

---

## **Tips til implementering:**

1. **Start med basics:** Få tilføj/slet til at virke først
2. **Brug List** Gem alle opgaver i en liste
3. **Test edge cases:** Tomme inputs, lange beskrivelser
4. **Styling gradvist:** Få funktionalitet til at virke før styling
5. **Brug @bind:** For input felter og checkboxes

### **Bonus udfordring:**
Kan I få jeres todo liste til at gemme data mellem browser sessioner? Hint: localStorage! 💾

**Lav en todo liste I selv ville bruge - det gør opgaven meget mere sjov! 🎯**