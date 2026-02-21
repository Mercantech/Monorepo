# Vejledende løsning: 4. Loops

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 – tallene 1 til 10

Klassisk for-loop: startværdi, betingelse og optælling i én linje.

```csharp
for (int i = 1; i <= 10; i++)
    Console.WriteLine(i);
```

---

## Opgave 2 – lige tal fra 2 til 20

Vi starter ved 2 og tjekker med modulo; kun lige tal udskrives.

```csharp
for (int i = 2; i <= 20; i++)
{
    if (i % 2 == 0)
        Console.WriteLine(i);
}
```

---

## Opgave 3 – sum 1 til 100

Akkumulator-mønster: en variabel (her `sum`) opdateres i hver gennemløb.

```csharp
int sum = 0;
for (int i = 1; i <= 100; i++)
    sum += i;
Console.WriteLine(sum);
```

---

## Opgave 4 – udskriv navn X gange

```csharp
Console.WriteLine("Indtast dit navn:");
string name = Console.ReadLine();
Console.WriteLine("Hvor mange gange?");
int count = int.Parse(Console.ReadLine());
for (int i = 0; i < count; i++)
    Console.WriteLine(name);
```

---

## Opgave 5 – ned fra tal til 1

Loopet kører baglæns ved at starte på det indtastede tal og trække 1 fra i hver omgang.

```csharp
Console.WriteLine("Indtast et tal:");
int number = int.Parse(Console.ReadLine());
for (int i = number; i >= 1; i--)
    Console.WriteLine(i);
```

---

## Opgave 6 – bogstaver i navn (ét per linje)

`.Length` giver antal tegn; vi bruger indeks `i` til at læse ét tegn ad gangen.

```csharp
string name = "Anders";
for (int i = 0; i < name.Length; i++)
    Console.WriteLine(name[i]);
```

---

## Opgave 7 – tæl 'a' i tekst

Tjek både lille og stor 'a' for at være uafhængig af store/små bogstaver.

```csharp
Console.WriteLine("Indtast en tekst:");
string text = Console.ReadLine();
int count = 0;
for (int i = 0; i < text.Length; i++)
    if (text[i] == 'a' || text[i] == 'A')
        count++;
Console.WriteLine($"Bogstavet 'a' optræder {count} gange.");
```

---

## Opgave 8 – ulige tal 1–50

`i % 2 != 0` er sand når resten er 1, dvs. tallet er ulige.

```csharp
for (int i = 1; i <= 50; i++)
{
    if (i % 2 != 0)
        Console.WriteLine(i);
}
```

---

## Opgave 9 – sum af 5 indtastede tal

```csharp
int sum = 0;
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Indtast tal {i + 1}:");
    sum += int.Parse(Console.ReadLine());
}
Console.WriteLine($"Summen er: {sum}");
```

---

## Opgave 10 – gæt et tal 1–10

`Random.Next(min, max)` giver et tal fra min inkl. til max *ekskl*. Derfor 1–11 for at få 1–10. `do-while` sikrer mindst ét gæt.

```csharp
Random random = new Random();
int secretNumber = random.Next(1, 11);
int guess;
do
{
    Console.WriteLine("Gæt et tal mellem 1 og 10:");
    guess = int.Parse(Console.ReadLine());
    if (guess < secretNumber) Console.WriteLine("For lavt!");
    else if (guess > secretNumber) Console.WriteLine("For højt!");
} while (guess != secretNumber);
Console.WriteLine("Rigtigt!");
```

---

## Opgave 11 – BankeBøf

Tjek først "deleligt med både 3 og 5", ellers vil fx 15 også matche kun "Banke" eller "Bøf".

```csharp
for (int i = 1; i <= 30; i++)
{
    if (i % 3 == 0 && i % 5 == 0)
        Console.WriteLine("BankeBøf");
    else if (i % 3 == 0)
        Console.WriteLine("Banke");
    else if (i % 5 == 0)
        Console.WriteLine("Bøf");
    else
        Console.WriteLine(i);
}
```

---

## Opgave 12 – Mini-projekt: Lommeregner

`op` er den indtastede operator; vi vælger den rigtige udregning med if/else.

```csharp
Console.WriteLine("Indtast første tal:");
double a = double.Parse(Console.ReadLine());
Console.WriteLine("Indtast andet tal:");
double b = double.Parse(Console.ReadLine());
Console.WriteLine("Vælg regneart (+, -, *, /):");
string op = Console.ReadLine();
if (op == "+") Console.WriteLine(a + b);
else if (op == "-") Console.WriteLine(a - b);
else if (op == "*") Console.WriteLine(a * b);
else if (op == "/") Console.WriteLine(a / b);
else Console.WriteLine("Ukendt regneart");
```
