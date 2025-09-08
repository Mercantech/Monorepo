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
            // Lav opgaven herunder! 
            int number = 10;
            Console.WriteLine(number);

        }

        public static void Double1()
        {
            Console.WriteLine("Opgave 2: ");
            Console.WriteLine("Lav en variabel af typen double og tildel den en værdi komma (decimal) værdi af 5 og en 1/4");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // Double kan indeholde decimaltal med høj præcision
            // 5 og 1/4 = 5.25
            double decimalNumber = 5.25;
            Console.WriteLine(decimalNumber);

        }

        public static void Strings1()
        {
            Console.WriteLine("Opgave 3: ");
            Console.WriteLine("Lav en variabel af typen string og tildel den en værdi - den skal indeholde teksten 'Hello, World' med et udråbstegn til sidst!");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // String bruges til tekst og skal være i anførselstegn
            string greeting = "Hello, World!";
            Console.WriteLine(greeting);
        }

        public static void Bool1()
        {
            Console.WriteLine("Opgave 4: ");
            Console.WriteLine("Lav en variabel af typen bool og tildel den en sandhedsværdi (true/false).");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // Bool kan kun være true eller false (sand eller falsk)
            bool isTrue = true;
            Console.WriteLine(isTrue);

        }


        public static void StringInterpolation()
        {
            Console.WriteLine("Opgave 5: ");
            Console.WriteLine("Lav to string variabeler og udskriv dem ved brug af string interpolation.");
            Console.WriteLine("De skal være 'Hello, ' og 'World!'");
            // Lav opgaven herunder!
            // String interpolation bruger $ foran strengen og {} omkring variabler
            string part1 = "Hello, ";
            string part2 = "World!";
            Console.WriteLine($"{part1}{part2}");
        }

        public static void StringInterpolation2()
        {
            Console.WriteLine("Opgave 5: ");
            Console.WriteLine("Her er fire forskellige strenge. Din opgave er at kombinere dem til én sætning ved brug af string interpolation!");
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

            // Løsning med string interpolation
            // Vi skal kombinere: "Hej" + "!" + "med" + "dig" til "Hej med dig!"
            // Bemærk rækkefølgen: del1, del4, del3, del2
            Console.WriteLine($"{del1} {del4} {del3}{del2}");
            
        }

        public static void Float1()
        {
            Console.WriteLine("Opgave 6: ");
            Console.WriteLine("Lav en variabel af typen float og tildel den en værdi af 3 + 0.14");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // Float bruges til decimaltal med lavere præcision end double
            // Bemærk 'f' efter tallet - det fortæller C# at det er en float
            float piApprox = 3.14f;
            Console.WriteLine(piApprox);
        }

        public static void Char1()
        {
            Console.WriteLine("Opgave 7: ");
            Console.WriteLine("Lav en variabel af typen char og tildel den en værdi af det første bogstav i alfabetet (Det skal være stort!)");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // Char bruges til enkelte tegn og skal være i enkelte anførselstegn
            // Det første bogstav i alfabetet er A (stort)
            char firstLetter = 'A';
            Console.WriteLine(firstLetter);
        }

        public static void Decimal1()
        {
            Console.WriteLine("Opgave 8: ");
            Console.WriteLine("Lav en variabel af typen decimal og tildel den en værdi af 100 og en halv");
            Console.WriteLine("Udskriv variablen til konsollen.");
            // Lav opgaven herunder!
            // Decimal bruges til meget præcise decimaltal, ofte til penge
            // Bemærk 'm' efter tallet - det fortæller C# at det er en decimal
            // 100 og en halv = 100.5
            decimal preciseNumber = 100.5m;
            Console.WriteLine(preciseNumber);
        }
    }
}
