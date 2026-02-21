# Vejledende løsning: 1. Variabler

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 2 – double

```csharp
double value = 5.25;
Console.WriteLine(value);
```

---

## Opgave 3 – string

```csharp
string text = "Hello, World!";
Console.WriteLine(text);
```

---

## Opgave 4 – bool

```csharp
bool isTrue = true;  // eller false
Console.WriteLine(isTrue);
```

---

## Opgave 5 – string interpolation (to variabler)

```csharp
string part1 = "Hello, ";
string part2 = "World!";
Console.WriteLine($"{part1}{part2}");
```

---

## Opgave 6 – string interpolation (fire dele → "Hej med dig!")

Variablerne `del1`, `del4`, `del3`, `del2` er allerede defineret i opgaven – vi kombinerer dem i den rigtige rækkefølge.

```csharp
Console.WriteLine($"{del1} {del4} {del3}{del2}");
```

---

## Opgave 7 – float

`f`-suffixet er nødvendigt, så compileren tolker tallet som float i stedet for double.

```csharp
float number = 3.14f;
Console.WriteLine(number);
```

---

## Opgave 8 – char

Enkelt citationstegn bruges til ét tegn; dobbelt til strenge.

```csharp
char letter = 'A';
Console.WriteLine(letter);
```

---

## Opgave 9 – decimal

`m`-suffixet angiver decimal (velegnet til penge og præcise tal).

```csharp
decimal amount = 100.5m;
Console.WriteLine(amount);
```
