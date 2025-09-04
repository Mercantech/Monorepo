Række typer, såsom arrays, lister og dictionaries har alle til formål at samle flere værdier under en samling med et navn/nøgle.

#### Hvad er array datatypen? I har allerede brugt dem!

Som vi har gennemgået fylder en `int`- 32bit og det betyder at det fylder 4 bytes. `double`, `float` og `decimal` fylder selvfølgelig mere grundet deres nøjagtighed. `int` er en simpel værdi type som vi er vant til!

<div class="columns">

<div>
  <img src="Pasted image 20250904191555.png" alt="Datatyper illustration" style="max-width:100%; border-radius:8px;">
</div>

<div>
  <img src="Pasted image 20250904191605.png" alt="Char datatype illustration" style="max-width:100%; border-radius:8px;">
  
  En `char` (karakter) fylder normalt kun en `byte` altså en 8-bit. I C# fylder den 16-bit, da det supportere et bredere valg af karaktere.
</div>

</div>

De kan altså blive vist på en kort samling og det er også derfor de bliver kaldt simple datatyper eller primitive typer!

Hvad gør vi så, når vi gerne vil kombinere flere datatyper såsom en række af karaktere. Vi kan indsætte hver enkelt `char` på en plads. Men hvordan binder vi dem sammen?

Altså hvordan kan vi pege på en række af værdier med en enkelt nøgle?

<div class="columns reverse">

<div>
  Her kommer vi til at bruge arrays og vi har faktisk allerede brugt dem! En streng er faktisk en array af karaktere og som bliver afsluttet med en `NULL` - karakter. Dette er grund idéen bag arrays, men det kan bruges til meget mere!
</div>

<div>
  <img src="Pasted image 20250904191622.png" alt="Array koncept illustration" style="max-width:100%; border-radius:8px;">
  
  <img src="Pasted image 20250904191632.png" alt="Array struktur illustration" style="max-width:100%; border-radius:8px;">
</div>

</div>

### Arrays

I C# bruges arrays til at gemme og håndtere en samling af elementer af samme type. En array er en datastruktur, der giver mulighed for at gemme flere værdier i en enkelt variabel.

<div class="columns">

<div>
  <img src="Pasted image 20250904191822.png" alt="Array indeksering illustration" style="max-width:100%; border-radius:8px;">
</div>

<div>
  Arrays (og lister) er indexet, hvilket betyder at vi kan tilgå dem med deres plads i rækken.
</div>

</div>

For at oprette en array i C#, skal du angive typen af elementer, der skal gemmes i arrayet, og derefter angive størrelsen på arrayet. For eksempel kan du oprette en array af heltal med ti elementer ved at skrive:

```csharp
int[] numbers = new int[10];
```

Her har vi oprettet en array ved navn `numbers`, der kan gemme ti heltal. Arrayet initialiseres med standardværdierne for den givne type, i dette tilfælde er det 0 for heltal.

Du kan tilgå og manipulere elementerne i arrayet ved hjælp af indekser. Indekserne starter altid fra 0, så det første element i arrayet har indeks 0, det andet element har indeks 1 osv. For at tilgå et element i arrayet kan du bruge følgende syntaks:

```csharp
int firstNumber = numbers[0];
```

Her har vi tilgået det første element i arrayet `numbers` og gemt det i variablen `firstNumber`.

Du kan også ændre værdien af et element i arrayet ved at tildele en ny værdi til det pågældende indeks:

```csharp
numbers[0] = 42;
```

Her har vi ændret værdien af det første element i arrayet `numbers` til 42.

Arrays er vigtige i C# og andre programmeringssprog, da de giver mulighed for at gemme og arbejde med en samling af data på en struktureret måde. De gør det nemt at håndtere flere elementer af samme type og giver mulighed for effektiv adgang og manipulation af data. Arrays bruges ofte til opbevaring af lister af elementer, såsom tal, strenge eller objekter.

Det er vigtigt at være opmærksom på arrayets størrelse og ikke forsøge at tilgå eller ændre elementer uden for det gyldige indeksområde, da det kan føre til uforudsigelig adfærd eller fejl i programmet.

### Lister

Lister i C# er en datastruktur, der bruges til at gemme og håndtere en samling af elementer af samme type. De er vigtige, fordi de giver fleksibilitet og dynamisk størrelse i forhold til arrays.

For at bruge lister i C#, skal du først importere `System.Collections.Generic`-namespace. Derefter kan du oprette en liste ved at angive typen af elementer, der skal gemmes i listen. For eksempel kan du oprette en liste af heltal ved at skrive:

```csharp
using System.Collections.Generic;

List<int> numbers = new List<int>();
```

Her har vi oprettet en tom liste ved navn `numbers`, der kan gemme heltal.

Du kan tilføje elementer til listen ved hjælp af `Add()`-metoden. For eksempel:

```csharp
numbers.Add(10);
numbers.Add(20);
numbers.Add(30);
```

Her har vi tilføjet tre heltal til `numbers`-listen.

Du kan også tilgå og manipulere elementer i listen ved hjælp af indekser. Indekserne starter fra 0, så det første element i listen har indeks 0, det andet element har indeks 1 og så videre. For at tilgå et element i listen kan du bruge følgende syntaks:

```csharp
int firstNumber = numbers[0];
```

Her har vi tilgået det første element i `numbers`-listen og gemt det i variablen `firstNumber`.

Du kan ændre værdien af et element i listen ved at tildele en ny værdi til det pågældende indeks:

```csharp
numbers[0] = 42;
```

Her har vi ændret værdien af det første element i `numbers`-listen til 42.

Lister kan også have dynamisk størrelse, hvilket betyder, at du kan tilføje eller fjerne elementer efter behov. Du kan bruge metoder som `Add()`, `Insert()`, `Remove()` og `Clear()` til at tilføje, indsætte, fjerne og rydde elementer i listen.

Lister er nyttige, når du har brug for at arbejde med samlinger af data, der kan ændre sig i størrelse. De giver dig mulighed for at tilføje, fjerne og manipulere elementer på en fleksibel måde. Lister er også mere effektive end arrays, når det kommer til håndtering af store datamængder og dynamisk allokering af hukommelse.

Det er vigtigt at huske, at lister er reference-typer i C#, hvilket betyder, at du arbejder med en henvisning til listen og ikke selve listen. Derfor skal du være opmærksom på, hvordan du kopierer og deler lister mellem forskellige variabler og metoder.

Når du bruger lister, kan du udnytte de mange indbyggede metoder og egenskaber, der gør det nemt at manipulere og behandle data. Listens fleksibilitet og funktionalitet gør den til en vigtig datastruktur i C#-programmering.

### Hvornår skal man bruge lists overfor arrays

Både lister og arrays i C# er datastrukturer, der bruges til at gemme og håndtere en samling af elementer af samme type. Men der er nogle vigtige forskelle mellem dem:

|Emne|Arrays|Lists|
|---|---|---|
|Hukommelse|Arrays bruger mindre hukommelse end lister, da de ikke har nogen ekstra baggrundsinformation eller metoder.|Lister bruger mere hukommelse end arrays, da de har ekstra baggrundsinformation og metoder.|
|Størrelse|Arrays er en fast størrelse datastruktur, hvor størrelsen på arrayet er defineret ved oprettelsen og kan ikke ændres senere.|Lister er en dynamisk størrelse datastruktur, hvor størrelsen kan ændres efter behov.|
|Adgang og modificering af indhold|Elementerne i et array er gemt i en kontinuerlig blok i hukommelsen, hvilket giver en hurtig og direkte adgang til elementerne via indeksering.|Elementerne i en liste er gemt i en dynamisk allokering af hukommelse, hvilket giver mulighed for nem tilføjelse og fjernelse af elementer.|

Generelt set, hvis du ved, at størrelsen på dine data er fast, og du har brug for en hurtig og direkte adgang til elementerne baseret på indeksering, er et array en god mulighed. Hvis du derimod har brug for en dynamisk størrelse og fleksibilitet til at tilføje, fjerne og manipulere elementer, er en liste mere passende.

Begge datastrukturer har deres anvendelsesområder og fordele afhængigt af situationen og kravene i din kode.

Personligt vælger jeg næsten altid en liste, fordi de er mere fleksible end et array!

### **Dictionary<TKey,TValue>**

Dictionarys er gode til at lave et nøgle og værdi par (key - value pair). Man binder en værdi af eget valg op på en nøgle, ofte af typen “string”.

<div class="columns">

<div>
  <img src="Pasted image 20250904191921.png" alt="Dictionary illustration" style="max-width:100%; border-radius:8px;">
</div>

<div>
  Dictionarys er specielt gode til at lave nøgle-værdi-par, fordi de er bygget som et <a href="https://en.wikipedia.org/wiki/Hash_table">hashmap</a>. Det betyder, at de har en meget effektiv måde at lave opslag på, og hvis de er implementeret korrekt, er kompleksiteten for at finde en værdi ud fra dens nøgle kun O(1) (Big O).

  De er også forholdsvis nemme at loope igennem og bruge på mere komplekse måder. Det kan du se i eksemplet til venstre, hvor vi looper over en <strong>Dictionary&lt;TKey,TValue&gt;</strong>.

  <br>
  <a href="https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-8.0">.NET dokumentation for Dictionary</a>
</div>

</div>

```csharp
// Vi kan lave en Dictionary (Hashmap) med typerne string og int
Dictionary<string, int> ageMap = new Dictionary<string, int>();

// Vi kan tilføje elementer til en Dictionary med Add kodeordet. 
// Husk det er altid på formen - (TKey, TValue)
ageMap.Add("Anders", 25);
ageMap.Add("Line", 30);

// Vi kan bruge ["key"], notation til at tilgå en værdi i en dictionary
int aliceAge = ageMap["Line"];
```

