using System;

namespace Opgaver
{
    public class Variabler
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Variabler!");

            Int1();
            Double1();
            Strings1();
            Bool1();
            StringInterpolation();
            StringInterpolation2();
            Float1();
            Char1();
            Decimal1();
        }

        public static void Int1()
        {
            Console.WriteLine("Opgave 1: ");
            Console.WriteLine("Lav en variabel af typen int og tildel den en værdi af ti!");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 1 herunder! (Her er en vejledende løsning – prøv selv først!)
            int number = 10;
            Console.WriteLine(number);
        }

        public static void Double1()
        {
            Console.WriteLine("Opgave 2: ");
            Console.WriteLine("Lav en variabel af typen double og tildel den en decimalværdi svarende til 5 og 1/4 (5,25).");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 2 herunder!

        }

        public static void Strings1()
        {
            Console.WriteLine("Opgave 3: ");
            Console.WriteLine("Lav en variabel af typen string og tildel den en værdi - den skal indeholde teksten 'Hello, World' med et udråbstegn til sidst!");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 3 herunder!
        }

        public static void Bool1()
        {
            Console.WriteLine("Opgave 4: ");
            Console.WriteLine("Lav en variabel af typen bool og tildel den en sandhedsværdi (true/false).");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 4 herunder!

        }


        public static void StringInterpolation()
        {
            Console.WriteLine("Opgave 5: ");
            Console.WriteLine("Lav to string-variabler med 'Hello, ' og 'World!' og udskriv dem samlet med string interpolation.");
            // TODO: Lav opgave 5 herunder! Tip: brug $\"{variabel1}{variabel2}\"
        }

        public static void StringInterpolation2()
        {
            Console.WriteLine("Opgave 6: ");
            Console.WriteLine("Her er fire strenge. Din opgave er at kombinere dem til én sætning ved brug af string interpolation. Sætningen skal blive: Hej med dig!");
            Console.WriteLine("Strengene er: ");
            string del1 = "Hej";
            string del4 = "med";
            string del3 = "dig";
            string del2 = "!";
            Console.WriteLine($"del1: {del1}");
            Console.WriteLine($"del2: {del2}");
            Console.WriteLine($"del3: {del3}");
            Console.WriteLine($"del4: {del4}");
            Console.WriteLine("Kombiner dem nu til én sætning:");

            // TODO: Lav opgave 6 herunder!
        }

        public static void Float1()
        {
            Console.WriteLine("Opgave 7: ");
            Console.WriteLine("Lav en variabel af typen float og tildel den en værdi af 3 + 0,14 (brug f-suffix: 3.14f).");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 7 herunder!
        }

        public static void Char1()
        {
            Console.WriteLine("Opgave 8: ");
            Console.WriteLine("Lav en variabel af typen char og tildel den en værdi af det første bogstav i alfabetet (Det skal være stort!)");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 8 herunder!
        }

        public static void Decimal1()
        {
            Console.WriteLine("Opgave 9: ");
            Console.WriteLine("Lav en variabel af typen decimal og tildel den en værdi af 100,5 (brug m-suffix: 100.5m).");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // TODO: Lav opgave 9 herunder!
        }
    }
}
