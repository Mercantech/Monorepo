# Vejledende løsning: 5. Arrays

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 (Array) – 5 fornavne

Array har fast størrelse; vi bruger indeks `i` til at sætte værdier. `foreach` er nem til at læse alle elementer.

```csharp
string[] names = new string[5];
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast navn {i + 1}:");
    names[i] = Console.ReadLine();
}
foreach (string name in names)
    Console.WriteLine(name);
```

---

## Opgave 2 (Array) – største af 5 tal

Vi antager at det første tal er det største og sammenligner med resten. Alternativt: `tal.Max()` med `using System.Linq`.

```csharp
int[] numbers = new int[5];
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast tal {i + 1}:");
    numbers[i] = int.Parse(Console.ReadLine());
}
int max = numbers[0];
for (int i = 1; i < 5; i++)
    if (numbers[i] > max) max = numbers[i];
Console.WriteLine($"Største tal: {max}");
```

---

## Opgave 3 (Array) – bynavne omvendt

Vi løber baglæns fra sidste indeks (Length - 1 eller 4) ned til 0.

```csharp
string[] cities = new string[5];
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast by {i + 1}:");
    cities[i] = Console.ReadLine();
}
for (int i = 4; i >= 0; i--)
    Console.WriteLine(cities[i]);
```

---

## Opgave 4 (List) – 5 fornavne i liste

List kan vokse; `Add()` tilføjer et element til slutningen. `.Count` er antal elementer.

```csharp
List<string> names = new List<string>();
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast navn {i + 1}:");
    names.Add(Console.ReadLine());
}
foreach (string name in names)
    Console.WriteLine(name);
```

---

## Opgave 5 (List) – navne indtil "stop"

`do-while` sikrer at vi læser mindst én linje. Kun ikke-"stop" tilføjes til listen.

```csharp
List<string> names = new List<string>();
string input;
do
{
    Console.WriteLine("Indtast et navn (eller 'stop'):");
    input = Console.ReadLine();
    if (input != "stop")
        names.Add(input);
} while (input != "stop");
foreach (string name in names)
    Console.WriteLine(name);
```

---

## Opgave 6 (List) – gennemsnit af 5 tal

Gennemsnit = sum / antal. Vi bruger `double` til sum og resultat for at undgå heltalsdivision.

```csharp
List<int> numbers = new List<int>();
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast tal {i + 1}:");
    numbers.Add(int.Parse(Console.ReadLine()));
}
double sum = 0;
foreach (int n in numbers) sum += n;
Console.WriteLine($"Gennemsnit: {sum / numbers.Count}");
```

---

## Opgave 7 (List) – indkøbsliste med tilføj/fjern

*Forenklet:* Indtast "fjern [varenavn]" for at fjerne, ellers tilføjes teksten. `Substring(6)` tager teksten efter "fjern ".

```csharp
List<string> items = new List<string>();
string input;
do
{
    Console.WriteLine("Skriv ting at købe, 'fjern [navn]' for at fjerne, eller 'stop':");
    input = Console.ReadLine();
    if (input == "stop") break;
    if (input.StartsWith("fjern "))
        items.Remove(input.Substring(6));
    else if (!string.IsNullOrEmpty(input))
        items.Add(input);
} while (input != "stop");
Console.WriteLine("Din liste:");
foreach (string item in items) Console.WriteLine("- " + item);
```

---

## Opgave 8 (List) – venner der starter med 'A'

`StartsWith("A")` tjekker kun store A. Brug `StartsWith("A", StringComparison.OrdinalIgnoreCase)` for at ignorere store/små bogstaver.

```csharp
List<string> friends = new List<string>();
Console.WriteLine("Indtast venners navne (stop for at afslutte):");
string input;
while ((input = Console.ReadLine()) != "stop")
    friends.Add(input);
int countStartingWithA = 0;
foreach (string name in friends)
    if (name.StartsWith("A")) countStartingWithA++;
Console.WriteLine($"{countStartingWithA} navne starter med 'A'.");
```

---

## Opgave 9 (Dictionary) – 3 personer navn/alder

Dictionary gemmer nøgle-værdi-par. Her er nøglen navn (string), værdien er alder (int). Initializer-syntax `{ { k, v }, ... }` er praktisk til fast data.

```csharp
Dictionary<string, int> people = new Dictionary<string, int>
{
    { "Anna", 20 },
    { "Bo", 25 },
    { "Carla", 22 }
};
foreach (var pair in people)
    Console.WriteLine($"{pair.Key} er {pair.Value} år");
```

---

## Opgave 10 (Dictionary) – slå alder op på navn

`ContainsKey()` undgår at vi forsøger at læse en nøgle der ikke findes (ville give exception).

```csharp
Dictionary<string, int> people = new Dictionary<string, int>
{
    { "Anna", 20 },
    { "Bo", 25 },
    { "Carla", 22 }
};
Console.WriteLine("Indtast et navn:");
string name = Console.ReadLine();
if (people.ContainsKey(name))
    Console.WriteLine($"{name} er {people[name]} år");
else
    Console.WriteLine("Navnet findes ikke.");
```

---

## Opgave 11 – Mini-projekt: Klasseliste

Antal elever bestemmes af brugeren; derefter indlæses lige så mange navne.

```csharp
List<string> classList = new List<string>();
Console.WriteLine("Indtast antal elever:");
int count = int.Parse(Console.ReadLine());
for (int i = 0; i < count; i++)
{
    Console.WriteLine($"Elev {i + 1}:");
    classList.Add(Console.ReadLine());
}
Console.WriteLine("Klasseliste:");
foreach (string student in classList) Console.WriteLine(student);
```

---

## Opgave 12 – Mini-projekt: Indkøbsliste med pris

Dictionary med varenavn som nøgle og pris som værdi. Vi summerer alle priser til total.

```csharp
Dictionary<string, double> shoppingList = new Dictionary<string, double>();
for (int i = 0; i < 3; i++)
{
    Console.WriteLine("Indtast varenavn:");
    string itemName = Console.ReadLine();
    Console.WriteLine("Indtast pris:");
    shoppingList[itemName] = double.Parse(Console.ReadLine());
}
double total = 0;
Console.WriteLine("Indkøbsliste:");
foreach (var item in shoppingList)
{
    Console.WriteLine($"  {item.Key}: {item.Value:F2} kr");
    total += item.Value;
}
Console.WriteLine($"Total: {total:F2} kr");
```
