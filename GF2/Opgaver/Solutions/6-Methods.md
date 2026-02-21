# Vejledende løsning: 6. Methods

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 – method der udskriver "Hej verden!"

`void` betyder at metoden ikke returnerer en værdi.

```csharp
static void SayHelloWorld()
{
    Console.WriteLine("Hej verden!");
}
SayHelloWorld();
```

---

## Opgave 2 – method der udskriver 1–5

```csharp
static void PrintOneToFive()
{
    for (int i = 1; i <= 5; i++)
        Console.WriteLine(i);
}
PrintOneToFive();
```

---

## Opgave 3 – method der beder om navn og hilser

Metoden håndterer både input og output – den kalder ikke andre metoder, men du kunne godt splitte det op.

```csharp
static void GreetUser()
{
    Console.WriteLine("Hvad hedder du?");
    string name = Console.ReadLine();
    Console.WriteLine($"Hej, {name}!");
}
GreetUser();
```

---

## Opgave 4 – parameter: navn

Parameteren `name` får sin værdi fra kaldet; metoden kan genbruges med forskellige navne.

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Hej {name}!");
}
Greet("Mette");
```

---

## Opgave 5 – to tal, udskriv sum

```csharp
static void PrintSum(int a, int b)
{
    Console.WriteLine(a + b);
}
PrintSum(3, 5);
```

---

## Opgave 6 – parameter: tal, lige/ulige

```csharp
static void CheckEvenOrOdd(int number)
{
    if (number % 2 == 0)
        Console.WriteLine("Lige");
    else
        Console.WriteLine("Ulige");
}
CheckEvenOrOdd(4);
CheckEvenOrOdd(7);
```

---

## Opgave 7 – navn, alder, by

Flere parametre adskilles med komma. Rækkefølgen skal matche kaldet.

```csharp
static void Introduce(string name, int age, string city)
{
    Console.WriteLine($"Jeg hedder {name}, er {age} år gammel og kommer fra {city}");
}
Introduce("Anders", 20, "Aarhus");
```

---

## Opgave 8 – returner sum

Return-type er `int`; vi bruger `return` til at sende værdien tilbage. Kaldet kan bruge resultatet (fx til udskrift).

```csharp
static int Sum(int a, int b)
{
    return a + b;
}
Console.WriteLine(Sum(3, 5));
```

---

## Opgave 9 – returner om tal er lige

Metoden returnerer en bool – velegnet til at bruge i betingelser andre steder i koden.

```csharp
static bool IsEven(int number)
{
    return number % 2 == 0;
}
Console.WriteLine(IsEven(4));  // True
Console.WriteLine(IsEven(7));  // False
```

---

## Opgave 10 – returner hilsen-streng

Return-type `string`; vi bygger strengen med interpolation og returnerer den.

```csharp
static string GetGreeting(string name)
{
    return $"Hej {name}!";
}
Console.WriteLine(GetGreeting("Marie"));
```

---

## Opgave 11 – returner største af tre tal

Vi sammenligner parvis og returnerer den største. Kun én return udføres per kald.

```csharp
static int MaxOfThree(int a, int b, int c)
{
    if (a >= b && a >= c) return a;
    if (b >= c) return b;
    return c;
}
Console.WriteLine(MaxOfThree(3, 7, 2));
```

---

## Opgave 12 – rekursion: fakultet

*Rekursion:* Metoden kalder sig selv. Basistilfældet er n ≤ 1 (returner 1); ellers n * Fakultet(n-1). 5! = 5·4·3·2·1 = 120.

```csharp
static int Factorial(int n)
{
    if (n <= 1) return 1;
    return n * Factorial(n - 1);
}
Console.WriteLine(Factorial(5));  // 120
```

---

## Opgave 13 – rekursion: nedtælling

Vi udskriver først, kalder derefter rekursivt med n-1. Stop når n < 0.

```csharp
static void CountDown(int n)
{
    if (n < 0) return;
    Console.WriteLine(n);
    CountDown(n - 1);
}
CountDown(3);
```

---

## Opgave 14 – Mini-projekt: Lommeregner med methods

Hver regneart har sin egen lille method. Switch expression (`op switch { ... }`) vælger den rigtige; `_` er default.

```csharp
static double Add(double a, double b) => a + b;
static double Subtract(double a, double b) => a - b;
static double Multiply(double a, double b) => a * b;
static double Divide(double a, double b) => a / b;

Console.WriteLine("Indtast første tal:");
double x = double.Parse(Console.ReadLine());
Console.WriteLine("Indtast andet tal:");
double y = double.Parse(Console.ReadLine());
Console.WriteLine("Vælg +, -, * eller /:");
string op = Console.ReadLine();
double result = op switch
{
    "+" => Add(x, y),
    "-" => Subtract(x, y),
    "*" => Multiply(x, y),
    "/" => Divide(x, y),
    _ => 0
};
Console.WriteLine($"Resultat: {result}");
```

---

## Opgave 15 – Mini-projekt: Gæt-et-tal med methods

Opdeling i metoder gør spillet lettere at læse og teste: én method til at generere tal, én til at hente gæt, én til at sammenligne.

```csharp
static int GenerateNumber(int min, int max)
{
    Random r = new Random();
    return r.Next(min, max + 1);
}
static int GetGuess()
{
    return int.Parse(Console.ReadLine());
}
static string Compare(int guess, int secret)
{
    if (guess < secret) return "For lavt!";
    if (guess > secret) return "For højt!";
    return "Rigtigt!";
}

int secret = GenerateNumber(1, 10);
int guess;
do
{
    Console.WriteLine("Gæt et tal 1-10:");
    guess = GetGuess();
    Console.WriteLine(Compare(guess, secret));
} while (guess != secret);
```
