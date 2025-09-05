using System;
using System.Collections.Generic;

namespace Opgaver
{
    public class Classes
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Klasser og Objekter!");
            
            // Grundlæggende klasser
            Class1();
            Class2();
            Class3();
            
            // Properties opgaver
            Property1();
            Property2();
            Property3();
            
            // Constructor opgaver
            Constructor1();
            Constructor2();
            Constructor3();
            
            // Method opgaver i klasser
            Method1();
            Method2();
            Method3();
            
            // Inheritance opgaver
            Inheritance1();
            Inheritance2();
            
            // Mini-projekter
            MiniProjektPerson();
            MiniProjektBil();
        }

        public static void Class1()
        {
            Console.WriteLine("Opgave 1 (Grundlæggende klasse):");
            Console.WriteLine("Lav en klasse kaldet 'Person' med properties for navn og alder.");
            Console.WriteLine("Opret et objekt af klassen og udskriv informationen.");
            // Lav opgaven herunder!
        }

        public static void Class2()
        {
            Console.WriteLine("Opgave 2 (Grundlæggende klasse):");
            Console.WriteLine("Lav en klasse kaldet 'Bil' med properties for mærke, model og årgang.");
            Console.WriteLine("Opret to forskellige bil-objekter og udskriv deres information.");
            // Lav opgaven herunder!
        }

        public static void Class3()
        {
            Console.WriteLine("Opgave 3 (Grundlæggende klasse):");
            Console.WriteLine("Lav en klasse kaldet 'Cirkel' med properties for radius.");
            Console.WriteLine("Opret et cirkel-objekt og udskriv radiusen.");
            // Lav opgaven herunder!
        }

        public static void Property1()
        {
            Console.WriteLine("Opgave 4 (Properties):");
            Console.WriteLine("Lav en klasse kaldet 'Student' med properties for navn, alder og karakter.");
            Console.WriteLine("Brug både get og set til at håndtere karakteren.");
            Console.WriteLine("Opret et student-objekt og sæt/udskriv alle properties.");
            // Lav opgaven herunder!
        }

        public static void Property2()
        {
            Console.WriteLine("Opgave 5 (Properties):");
            Console.WriteLine("Lav en klasse kaldet 'Rektangel' med properties for længde og bredde.");
            Console.WriteLine("Tilføj en read-only property kaldet 'Areal' der beregner længde * bredde.");
            Console.WriteLine("Opret et rektangel-objekt og udskriv arealet.");
            // Lav opgaven herunder!
        }

        public static void Property3()
        {
            Console.WriteLine("Opgave 6 (Properties):");
            Console.WriteLine("Lav en klasse kaldet 'BankKonto' med properties for kontonummer og saldo.");
            Console.WriteLine("Sørg for at saldoen ikke kan være negativ (brug set-metoden til at tjekke dette).");
            Console.WriteLine("Opret en konto og prøv at sætte saldoen til både positiv og negativ værdi.");
            // Lav opgaven herunder!
        }

        public static void Constructor1()
        {
            Console.WriteLine("Opgave 7 (Constructor):");
            Console.WriteLine("Lav en klasse kaldet 'Hund' med properties for navn og race.");
            Console.WriteLine("Lav en constructor der tager navn og race som parametre.");
            Console.WriteLine("Opret et hund-objekt ved brug af constructoren.");
            // Lav opgaven herunder!
        }

        public static void Constructor2()
        {
            Console.WriteLine("Opgave 8 (Constructor):");
            Console.WriteLine("Lav en klasse kaldet 'Bog' med properties for titel, forfatter og antal sider.");
            Console.WriteLine("Lav både en constructor med alle parametre og en default constructor.");
            Console.WriteLine("Opret bøger ved brug af begge constructors.");
            // Lav opgaven herunder!
        }

        public static void Constructor3()
        {
            Console.WriteLine("Opgave 9 (Constructor):");
            Console.WriteLine("Lav en klasse kaldet 'Punkt' med properties for x og y koordinater.");
            Console.WriteLine("Lav en constructor der tager x og y som parametre.");
            Console.WriteLine("Lav en overloaded constructor der kun tager x som parameter (y skal være 0).");
            // Lav opgaven herunder!
        }

        public static void Method1()
        {
            Console.WriteLine("Opgave 10 (Methods i klasser):");
            Console.WriteLine("Lav en klasse kaldet 'Lommeregner' med en method der tager to tal og returnerer summen.");
            Console.WriteLine("Opret et lommeregner-objekt og test methoden.");
            // Lav opgaven herunder!
        }

        public static void Method2()
        {
            Console.WriteLine("Opgave 11 (Methods i klasser):");
            Console.WriteLine("Lav en klasse kaldet 'Cirkel' med properties for radius og methods for at beregne areal og omkreds.");
            Console.WriteLine("Opret et cirkel-objekt og udskriv både areal og omkreds.");
            // Lav opgaven herunder!
        }

        public static void Method3()
        {
            Console.WriteLine("Opgave 12 (Methods i klasser):");
            Console.WriteLine("Lav en klasse kaldet 'Person' med properties for navn og alder.");
            Console.WriteLine("Tilføj en method 'IntroduceYourself()' der udskriver 'Hej, jeg hedder [navn] og er [alder] år gammel'.");
            Console.WriteLine("Opret et person-objekt og kald methoden.");
            // Lav opgaven herunder!
        }

        public static void Inheritance1()
        {
            Console.WriteLine("Opgave 13 (Inheritance):");
            Console.WriteLine("Lav en base klasse kaldet 'Dyr' med properties for navn og alder.");
            Console.WriteLine("Lav en derived klasse kaldet 'Hund' der arver fra Dyr og har en ekstra property for race.");
            Console.WriteLine("Opret både et Dyr-objekt og et Hund-objekt.");
            // Lav opgaven herunder!
        }

        public static void Inheritance2()
        {
            Console.WriteLine("Opgave 14 (Inheritance):");
            Console.WriteLine("Lav en base klasse kaldet 'Køretøj' med properties for mærke og årgang.");
            Console.WriteLine("Lav en derived klasse kaldet 'Bil' der arver fra Køretøj og har en ekstra property for antal døre.");
            Console.WriteLine("Lav en method i Bil-klassen der udskriver alle informationer.");
            // Lav opgaven herunder!
        }

        public static void MiniProjektPerson()
        {
            Console.WriteLine("\nMini-projekt: Person management system (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine("Lav et system til at håndtere personer:");
            Console.WriteLine("- En Person klasse med navn, alder, email og telefonnummer");
            Console.WriteLine("- Properties med validering (email skal indeholde @, alder skal være positiv)");
            Console.WriteLine("- En method til at udskrive personens fulde information");
            Console.WriteLine("- En method til at ændre email (med validering)");
            Console.WriteLine("Opret flere person-objekter og test alle funktioner.");
            // Lav opgaven herunder!
        }

        public static void MiniProjektBil()
        {
            Console.WriteLine("\nMini-projekt: Bil showroom (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine("Lav et system til at håndtere biler:");
            Console.WriteLine("- En base klasse 'Køretøj' med mærke, model og årgang");
            Console.WriteLine("- En derived klasse 'Bil' med antal døre og brændstoftype");
            Console.WriteLine("- En derived klasse 'Motorcykel' med cylinderantal");
            Console.WriteLine("- Methods til at udskrive information og beregne alder");
            Console.WriteLine("- En method til at tjekke om køretøjet er gammelt (over 10 år)");
            Console.WriteLine("Opret forskellige køretøjer og test alle funktioner.");
            // Lav opgaven herunder!
        }
    }
}