# Vejledende løsning: 3. Control Flow

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 – if/else: højere eller lavere end 18

Værdien kan sættes i koden eller læses med `Console.ReadLine()` og `int.Parse()`.

```csharp
int value = 20;
if (value > 18)
    Console.WriteLine("Værdien er højere end 18");
else if (value < 18)
    Console.WriteLine("Værdien er lavere end 18");
else
    Console.WriteLine("Værdien er præcis 18");
```

---

## Opgave 2 – if/else: lige eller ulige

Modulo-operatoren `%` giver rest ved heltalsdivision. Rest 0 betyder lige tal.

```csharp
int number = 7;
if (number % 2 == 0)
    Console.WriteLine("Tallet er lige");
else
    Console.WriteLine("Tallet er ulige");
```

---

## Opgave 3 – switch: lige eller ulige

Switch bruger her resten (0 eller 1) til at vælge udfald. `break` er nødvendig i hver case.

```csharp
int number = 7;
int remainder = number % 2;
switch (remainder)
{
    case 0:
        Console.WriteLine("Tallet er lige");
        break;
    default:
        Console.WriteLine("Tallet er ulige");
        break;
}
```

---

## Opgave 4 – ternary: lige eller ulige

Ternary operator: `betingelse ? værdiHvisSand : værdiHvisFalsk`. Kort og læsbart til simple valg.

```csharp
int number = 7;
string result = (number % 2 == 0) ? "lige" : "ulige";
Console.WriteLine($"Tallet er {result}");
```

---

## Opgave 5 – Mini-projekt: Quiz

`?.` (null-conditional) sikrer at vi ikke får fejl, hvis ReadLine() returnerer null. `ToLower()` gør sammenligningen uafhængig af store/små bogstaver.

```csharp
int score = 0;
Console.WriteLine("Hvad er hovedstaden i Danmark?");
if (Console.ReadLine()?.ToLower() == "københavn")
    score++;
Console.WriteLine("Hvor mange ben har et menneske?");
if (Console.ReadLine() == "2")
    score++;
Console.WriteLine("Hvilken farve har græsset?");
if (Console.ReadLine()?.ToLower() == "grøn")
    score++;
Console.WriteLine($"Du fik {score} ud af 3 rigtige.");
```

---

## Opgave 6 – Mini-projekt: Karakter-feedback

Rækkefølgen af betingelser er vigtig: vi tjekker fra højeste karakter og ned.

```csharp
Console.WriteLine("Indtast din karakter:");
int grade = int.Parse(Console.ReadLine());
if (grade >= 10)
    Console.WriteLine("Super flot!");
else if (grade >= 7)
    Console.WriteLine("Godt klaret!");
else if (grade >= 4)
    Console.WriteLine("Du kan gøre det bedre.");
else
    Console.WriteLine("Du bør øve mere.");
```

*Ekstra (gennemsnit):* Gem karakterer i en `List<int>`, brug et loop til indtastning og beregn gennemsnit med `list.Sum() / list.Count`.
