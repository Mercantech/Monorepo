# Vejledende løsning: 2. Inputs

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 – string input

`Console.ReadLine()` returnerer altid en string – her bruger vi den direkte.

```csharp
string input = Console.ReadLine();
Console.WriteLine(input);
```

---

## Opgave 2 – heltal input

`int.Parse()` konverterer strengen til et heltal. Brugeren skal indtaste et gyldigt tal.

```csharp
int number = int.Parse(Console.ReadLine());
Console.WriteLine(number);
```

---

## Opgave 3 – decimaltal input

`double.Parse()` håndterer både dansk (komma) og engelsk (punktum) decimalformat, afhængigt af systemets sprog.

```csharp
double value = double.Parse(Console.ReadLine());
Console.WriteLine(value);
```

---

## Opgave 4 – bool input

Brugeren skal skrive "True" eller "False" (store begyndelsesbogstaver) for at `bool.Parse()` kan genkende værdien.

```csharp
bool value = bool.Parse(Console.ReadLine());
Console.WriteLine(value);
```

---

## Opgave 5 – Mini-projekt: Personlig profil

Vi gemmer hver værdi i en variabel og bruger string interpolation til den samlede sætning.

```csharp
Console.WriteLine("Indtast dit navn:");
string name = Console.ReadLine();
Console.WriteLine("Indtast din alder:");
int age = int.Parse(Console.ReadLine());
Console.WriteLine("Indtast din hjemby:");
string city = Console.ReadLine();
Console.WriteLine($"Hej, jeg hedder {name}, er {age} år gammel og kommer fra {city}!");
```

---

## Opgave 6 – Mini-projekt: BMI-beregner

BMI = vægt (kg) / højde (m)². `:F2` i interpolationen viser to decimaler.

```csharp
Console.WriteLine("Indtast din vægt i kg:");
double weightKg = double.Parse(Console.ReadLine());
Console.WriteLine("Indtast din højde i meter:");
double heightM = double.Parse(Console.ReadLine());
double bmi = weightKg / (heightM * heightM);
Console.WriteLine($"Din BMI er: {bmi:F2}");
```
