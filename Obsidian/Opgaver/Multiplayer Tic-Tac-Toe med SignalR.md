# 🎮 **<Multiplayer Tic-Tac-Toe med SignalR>**

**Introduktion:**

I denne opgave skal I bygge et multiplayer Tic-Tac-Toe spil, hvor spillere kan spille mod hinanden i real-time gennem browseren. I skal bruge SignalR til at håndtere kommunikationen mellem spillerne og sikre, at alle ser de samme træk øjeblikkeligt.

---

## **Opgavebeskrivelse:**

### **1. Grundlæggende funktionalitet:**
- Opret en Blazor Server applikation med SignalR Hub
- Implementer et 3x3 Tic-Tac-Toe spilbræt
- Spillere skal kunne oprette eller tilslutte sig eksisterende spil
- Real-time opdateringer når en spiller laver et træk
- Automatisk detektion af vinder eller uafgjort

### **2. Spil logik:**
- Spillere skiftes til at placere X og O
- Valider at træk er gyldige (ikke optaget felt)
- Tjek for vinderbetingelser efter hvert træk
- Håndter spil genstart funktionalitet

### **3. Brugergrænsefladen:**
- Visuelt spilbræt med klikbare felter
- Vis hvem der har turen
- Vis spilstatus (venter på spiller, dit træk, vinder, etc.)
- Liste over aktive spil der kan tilsluttes

---

## **Tekniske krav:**

### **SignalR Hub funktioner:**
```csharp
// Eksempel på hub metoder I skal implementere
public async Task JoinGame(string gameId, string playerName)
public async Task MakeMove(string gameId, int position)
public async Task CreateGame(string playerName)
public async Task RestartGame(string gameId)
```

### **Client-side funktioner:**
- Modtag spil opdateringer fra serveren
- Send træk til serveren
- Håndter forbindelsesstatus
- Vis real-time beskeder til spillere

---

## **Udvidelser for de modige:**

1. **Spectator mode:** Tilføj mulighed for at se på andres spil
2. **Chat funktionalitet:** Lad spillere skrive til hinanden under spillet
3. **Spil historik:** Gem og vis tidligere spil resultater
4. **Turnering system:** Organiser spillere i brackets
5. **Forskellige spilstørrelser:** 4x4 eller 5x5 brætter med tilpassede vinderbetingelser
6. **AI modstander:** Implementer en computer modstander for single-player

---

## **Læringsmål:**

- Forstå real-time kommunikation med SignalR
- Arbejde med Blazor Server og state management
- Implementere spil logik og validering
- Håndtere multiple samtidige forbindelser
- Designe responsive brugergrænseflader

---

## **Afleveringsform:**

### **Live demonstration (5-7 minutter):**
- Vis hvordan spillere kan oprette og tilslutte sig spil
- Demonstrer real-time funktionalitet med to browsere
- Forklar jeres SignalR implementering
- Vis særlige features I har tilføjet

### **Kode på GitHub:**
- Dokumenteret kode med kommentarer
- README med setup instruktioner
- Beskrivelse af arkitektur og design beslutninger

---

## **Inspiration:**

Tænk på hvordan I kan gøre jeres version unik:
- Forskellige temaer eller designs
- Lydeffekter og animationer  
- Mobile-venlig interface
- Statistikker og achievements
- Custom spilregler eller variationer

**Bonus:** Hvis I får det til at virke lokalt, prøv at deploye det så andre kan spille!