using System;

namespace Opgaver
{
    public class Inputs
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Expressions, Operators og Inputs!");
            String1();
            Int1();
            Double1();
            Bool1();

            // Mini-projekter til inspiration:
            MiniProjekt1();
            MiniProjekt2();
        }

        public static void String1()
        {
            Console.WriteLine(
                "Lav et program som gemmer et input som en string og skriver strengen ud i konsollen"
            );

            Console.WriteLine("Indtast en streng: ");
            // Lav opgaven herunder!
            // Console.ReadLine() læser input fra brugeren som en string
            string userInput = Console.ReadLine();
            Console.WriteLine($"Du indtastede: {userInput}");
        }

        public static void Int1()
        {
            Console.WriteLine(
                "Lav et program som gemmer et input som et tal og skriver tallet ud i konsollen"
            );

            Console.WriteLine("Indtast et tal: ");
            // Lav opgaven herunder!
            // Console.ReadLine() returnerer altid string, så vi skal konvertere til int
            // int.Parse() konverterer string til int
            string input = Console.ReadLine();
            int number = int.Parse(input);
            Console.WriteLine($"Du indtastede tallet: {number}");
        }

        public static void Double1()
        {
            Console.WriteLine(
                "Lav et program som gemmer et input som et decimaltal og skriver tallet ud i konsollen"
            );

            Console.WriteLine("Indtast et decimaltal: ");
            // Lav opgaven herunder!
            // double.Parse() konverterer string til double (decimaltal)
            string input = Console.ReadLine();
            double decimalNumber = double.Parse(input);
            Console.WriteLine($"Du indtastede decimaltallet: {decimalNumber}");
        }

        public static void Bool1()
        {
            Console.WriteLine(
                "Lav et program som gemmer et input som en sandhedsværdi og skriver værdien ud i konsollen"
            );

            Console.WriteLine("Indtast en sandhedsværdi (sandt/falsk): ");
            // Lav opgaven herunder!
            // bool.Parse() konverterer string til bool
            // Accepterer "true"/"false" eller "True"/"False"
            string input = Console.ReadLine();
            bool boolValue = bool.Parse(input);
            Console.WriteLine($"Du indtastede sandhedsværdien: {boolValue}");
        }

        // Mini-projekt: Personlig profil (skabelon)
        public static void MiniProjekt1()
        {
            Console.WriteLine("\nMini-projekt: Personlig profil (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine("Lav et program, hvor brugeren indtaster sit navn, alder og hjemby.");
            Console.WriteLine(
                "Gem oplysningerne i variabler og udskriv en præsentationstekst, der bruger alle oplysningerne."
            );
            Console.WriteLine("Eksempel: Hej, jeg hedder X, er X år gammel og kommer fra X!");
            // Lav opgaven herunder!
            
            Console.Write("Indtast dit navn: ");
            string navn = Console.ReadLine();
            
            Console.Write("Indtast din alder: ");
            string alderInput = Console.ReadLine();
            int alder = int.Parse(alderInput);
            
            Console.Write("Indtast din hjemby: ");
            string hjemby = Console.ReadLine();
            
            // Udskriv præsentationstekst med string interpolation
            Console.WriteLine($"Hej, jeg hedder {navn}, er {alder} år gammel og kommer fra {hjemby}!");
        }

        // Mini-projekt 2: BMI-beregner (skabelon)
        public static void MiniProjekt2()
        {
            Console.WriteLine("\nMini-projekt 2: BMI-beregner (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                "Lav et program, hvor brugeren indtaster sin vægt (i kg) og højde (i meter)."
            );
            Console.WriteLine("Programmet skal beregne brugerens BMI og udskrive resultatet.");
            Console.WriteLine(
                "Tip: BMI beregnes som vægt divideret med højde i anden (BMI = vægt / (højde * højde))."
            );
            // Lav opgaven herunder!
            
            Console.Write("Indtast din vægt i kg: ");
            string vægtInput = Console.ReadLine();
            double vægt = double.Parse(vægtInput);
            
            Console.Write("Indtast din højde i meter (f.eks. 1.75): ");
            string højdeInput = Console.ReadLine();
            double højde = double.Parse(højdeInput);
            
            // Beregn BMI: vægt / (højde * højde)
            double bmi = vægt / (højde * højde);
            
            // Udskriv resultatet med 2 decimaler
            Console.WriteLine($"Dit BMI er: {bmi:F2}");
        }
    }
}
