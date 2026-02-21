# Vejledende løsning: 7. Classes

Brug denne fil kun hvis du er stødt på problemer. Prøv selv først!

---

## Opgave 1 – Person med navn og alder

Properties med `{ get; set; }` er auto-implementeret; compileren laver det bagvedliggende felt for dig.

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
Person p = new Person { Name = "Anna", Age = 25 };
Console.WriteLine($"{p.Name}, {p.Age} år");
```

---

## Opgave 2 – Bil med mærke, model, årgang

Object initializer `{ Prop1 = value, ... }` sætter properties lige efter objektet er oprettet.

```csharp
public class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}
Car car1 = new Car { Brand = "Volvo", Model = "V60", Year = 2020 };
Car car2 = new Car { Brand = "Toyota", Model = "Yaris", Year = 2018 };
Console.WriteLine($"{car1.Brand} {car1.Model} ({car1.Year})");
Console.WriteLine($"{car2.Brand} {car2.Model} ({car2.Year})");
```

---

## Opgave 3 – Cirkel med radius

```csharp
public class Circle
{
    public double Radius { get; set; }
}
Circle c = new Circle { Radius = 5.0 };
Console.WriteLine($"Radius: {c.Radius}");
```

---

## Opgave 4 – Student med get/set på karakter

Privat felt `_grade` (underscore er konvention for private felter); den offentlige property `Grade` styrer læsning og skrivning.

```csharp
public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    private int _grade;
    public int Grade
    {
        get => _grade;
        set => _grade = value;
    }
}
Student s = new Student { Name = "Bo", Age = 20, Grade = 10 };
Console.WriteLine($"{s.Name}, karakter: {s.Grade}");
```

---

## Opgave 5 – Rektangel med read-only Areal

Expression-bodied property uden `set`: kun læsning. Arealet beregnes altid ud fra Length og Width.

```csharp
public class Rectangle
{
    public double Length { get; set; }
    public double Width { get; set; }
    public double Area => Length * Width;
}
Rectangle r = new Rectangle { Length = 4, Width = 3 };
Console.WriteLine($"Areal: {r.Area}");
```

---

## Opgave 6 – BankKonto, saldo ikke negativ

I `set` tjekker vi værdien; kun ikke-negative tal gemmes. Negativ tilskrivning ignoreres stille.

```csharp
public class BankAccount
{
    public string AccountNumber { get; set; }
    private double _balance;
    public double Balance
    {
        get => _balance;
        set { if (value >= 0) _balance = value; }
    }
}
BankAccount account = new BankAccount { AccountNumber = "123", Balance = 100 };
account.Balance = -50;  // ignoreres
Console.WriteLine(account.Balance);  // 100
```

---

## Opgave 7 – Hund med constructor

Constructoren tvinger at navn og race angives ved oprettelse. Ingen parameterløs constructor findes her (medmindre du tilføjer en).

```csharp
public class Dog
{
    public string Name { get; set; }
    public string Breed { get; set; }
    public Dog(string name, string breed)
    {
        Name = name;
        Breed = breed;
    }
}
Dog dog = new Dog("Bella", "Labrador");
Console.WriteLine($"{dog.Name}, {dog.Breed}");
```

---

## Opgave 8 – Bog med fuld og default constructor

Parameterløs constructor `Book()` tillader oprettelse uden parametre; derefter sættes properties manuelt. Den anden constructor sætter alle felter på én gang.

```csharp
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int PageCount { get; set; }
    public Book() { }
    public Book(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        PageCount = pages;
    }
}
Book b1 = new Book("Moby Dick", "Melville", 600);
Book b2 = new Book { Title = "Ukendt", Author = "?", PageCount = 0 };
```

---

## Opgave 9 – Punkt med overloaded constructor

To constructors med samme navn men forskellige parametre (overload). Den anden sætter Y til 0 som standard.

```csharp
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public Point(double x, double y) { X = x; Y = y; }
    public Point(double x) { X = x; Y = 0; }
}
Point p1 = new Point(3, 4);
Point p2 = new Point(5);
```

---

## Opgave 10 – Lommeregner-klasse med Add

En simpel "service"-agtig klasse: den har ingen tilstand (felter), kun en method der udfører en beregning.

```csharp
public class Calculator
{
    public double Add(double a, double b) => a + b;
}
var calc = new Calculator();
Console.WriteLine(calc.Add(3, 5));
```

---

## Opgave 11 – Cirkel med Areal og Omkreds

`Math.PI` er den indbyggede konstant for π. Expression-bodied methods med `=>` er korte one-liners.

```csharp
public class Circle
{
    public double Radius { get; set; }
    public double GetArea() => Math.PI * Radius * Radius;
    public double GetCircumference() => 2 * Math.PI * Radius;
}
Circle c = new Circle { Radius = 5 };
Console.WriteLine($"Areal: {c.GetArea()}, Omkreds: {c.GetCircumference()}");
```

---

## Opgave 12 – Person med IntroduceYourself()

Instance method: kaldes på et konkret objekt (`p.IntroduceYourself()`) og bruger objektets Name og Age.

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public void IntroduceYourself()
    {
        Console.WriteLine($"Hej, jeg hedder {Name} og er {Age} år gammel");
    }
}
Person p = new Person { Name = "Carla", Age = 22 };
p.IntroduceYourself();
```

---

## Opgave 13 – Dyr og Hund (arv)

`Hund : Dyr` betyder at Hund arver fra Dyr – den får Name og Age, og tilføjer Breed. Base-klassen kan bruges til fælles adfærd.

```csharp
public class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
}
public class Dog : Animal
{
    public string Breed { get; set; }
}
Animal animal = new Animal { Name = "Katte", Age = 3 };
Dog dog = new Dog { Name = "Bella", Age = 5, Breed = "Labrador" };
```

---

## Opgave 14 – Køretøj og Bil (arv + method)

Bil arver Mærke og Årgang fra Vehicle og tilføjer AntalDøre. Metoden `PrintInfo()` har adgang til både egne og arvede properties.

```csharp
public class Vehicle
{
    public string Brand { get; set; }
    public int Year { get; set; }
}
public class Car : Vehicle
{
    public int DoorCount { get; set; }
    public void PrintInfo()
    {
        Console.WriteLine($"{Brand}, {Year}, {DoorCount} døre");
    }
}
Car car = new Car { Brand = "Volvo", Year = 2020, DoorCount = 5 };
car.PrintInfo();
```

---

## Opgave 15 – Mini-projekt: Person med validering

Validering i settere: Alder kun positiv; Email kun accepteret hvis den indeholder '@'. `?.` beskytter mod null.

```csharp
public class Person
{
    public string Name { get; set; }
    private int _age;
    public int Age
    {
        get => _age;
        set { if (value > 0) _age = value; }
    }
    private string _email = "";
    public string Email
    {
        get => _email;
        set { if (value?.Contains("@") == true) _email = value; }
    }
    public string Phone { get; set; }
    public void PrintInfo() =>
        Console.WriteLine($"{Name}, {Age}, {Email}, {Phone}");
    public void SetEmail(string email)
    {
        if (email?.Contains("@") == true) _email = email;
    }
}
```

---

## Opgave 16 – Mini-projekt: Køretøj, Bil, Motorcykel

Base-klassen har fælles data og hjælper-metoder (BeregnAlder, ErGammel). Bil og Motorcykel udvider med egne properties. `DateTime.Now.Year` giver indeværende år.

```csharp
public class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int GetAge() => DateTime.Now.Year - Year;
    public bool IsOld() => GetAge() > 10;
}
public class Car : Vehicle
{
    public int DoorCount { get; set; }
    public string FuelType { get; set; }
}
public class Motorcycle : Vehicle
{
    public int CylinderCount { get; set; }
}
```
