using System;

namespace Opgaver
{
    public class Loops
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Loops både med og uden datastrukturer!");
            Loop1();
            Loop2();
            Loop3();
            Loop4();
            Loop5();
            Loop6();
            Loop7();
            Loop8();
            Loop9();
            Loop10();
            BankeBøf();
            MiniProjektLommeregner();
        }

        public static void Loop1()
        {
            Console.WriteLine("Opgave 1:");
            Console.WriteLine("Brug et loop til at udskrive tallene fra 1 til 10.");
            // Lav opgaven herunder!
            // For-loop: for (start; betingelse; increment)
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static void Loop2()
        {
            Console.WriteLine("Opgave 2:");
            Console.WriteLine("Brug et loop og en if-betingelse til at udskrive alle lige tal fra 2 til 20.");
            // Lav opgaven herunder!
            // Loop gennem alle tal fra 2 til 20 og tjek om de er lige
            for (int i = 2; i <= 20; i++)
            {
                if (i % 2 == 0) // Tjek om tallet er lige (deleligt med 2)
                {
                    Console.WriteLine(i);
                }
            }
        }

        public static void Loop3()
        {
            Console.WriteLine("Opgave 3:");
            Console.WriteLine("Brug et loop til at lægge alle tal fra 1 til 100 sammen og udskriv resultatet.");
            // Lav opgaven herunder!
            int sum = 0; // Variabel til at gemme summen
            
            // Loop gennem alle tal fra 1 til 100
            for (int i = 1; i <= 100; i++)
            {
                sum += i; // Læg tallet til summen (sum = sum + i)
            }
            
            Console.WriteLine($"Summen af alle tal fra 1 til 100 er: {sum}");
        }

        public static void Loop4()
        {
            Console.WriteLine("Opgave 4:");
            Console.WriteLine("Bed brugeren om at indtaste sit navn og et tal. Udskriv navnet det antal gange ved hjælp af et loop.");
            // Lav opgaven herunder!
            Console.Write("Indtast dit navn: ");
            string navn = Console.ReadLine();
            
            Console.Write("Indtast et tal: ");
            int antal = int.Parse(Console.ReadLine());
            
            // Udskriv navnet det angivne antal gange
            for (int i = 1; i <= antal; i++)
            {
                Console.WriteLine($"{i}. {navn}");
            }
        }

        public static void Loop5()
        {
            Console.WriteLine("Opgave 5:");
            Console.WriteLine("Bed brugeren om at indtaste et tal. Brug et loop til at udskrive alle tal fra det indtastede tal og ned til 1.");
            // Lav opgaven herunder!
            Console.Write("Indtast et tal: ");
            int startTal = int.Parse(Console.ReadLine());
            
            // Loop fra startTal ned til 1 (decrement med i--)
            for (int i = startTal; i >= 1; i--)
            {
                Console.WriteLine(i);
            }
        }

        public static void Loop6()
        {
            Console.WriteLine("Opgave 6:");
            Console.WriteLine(@"Brug et loop til at udskrive alle bogstaverne i dit navn (ét bogstav pr. linje). 
            Navnet skal være gemt i en string variabel.");
            // Lav opgaven herunder!
            string navn = "Kiro"; // Eksempel navn
            
            // Loop gennem hvert tegn i strengen
            for (int i = 0; i < navn.Length; i++)
            {
                Console.WriteLine(navn[i]); // Udskriv bogstav på position i
            }
            
            // Alternativ løsning med foreach:
            Console.WriteLine("\nAlternativ med foreach:");
            foreach (char bogstav in navn)
            {
                Console.WriteLine(bogstav);
            }
        }

        public static void Loop7()
        {
            Console.WriteLine("Opgave 7:");
            Console.WriteLine("Brug et loop til at tælle, hvor mange gange bogstavet 'a' optræder i en tekst, som brugeren indtaster.");
            // Lav opgaven herunder!
            Console.Write("Indtast en tekst: ");
            string tekst = Console.ReadLine();
            
            int antalA = 0; // Tæller for bogstavet 'a'
            
            // Loop gennem hvert tegn i teksten
            for (int i = 0; i < tekst.Length; i++)
            {
                // Tjek om tegnet er 'a' eller 'A' (case-insensitive)
                if (tekst[i] == 'a' || tekst[i] == 'A')
                {
                    antalA++;
                }
            }
            
            Console.WriteLine($"Bogstavet 'a' optræder {antalA} gange i teksten.");
        }

        public static void Loop8()
        {
            Console.WriteLine("Opgave 8:");
            Console.WriteLine("Brug et loop til at udskrive alle ulige tal mellem 1 og 50.");
            // Lav opgaven herunder!
            // Loop gennem alle tal fra 1 til 50
            for (int i = 1; i <= 50; i++)
            {
                if (i % 2 != 0) // Tjek om tallet er ulige (ikke deleligt med 2)
                {
                    Console.WriteLine(i);
                }
            }
            
            // Alternativ løsning - start med 1 og hop 2 ad gangen:
            Console.WriteLine("\nAlternativ løsning:");
            for (int i = 1; i <= 50; i += 2)
            {
                Console.WriteLine(i);
            }
        }

        public static void Loop9()
        {
            Console.WriteLine("Opgave 9:");
            Console.WriteLine("Bed brugeren om at indtaste 5 tal (ét ad gangen). Brug et loop til at lægge dem sammen og udskriv summen til sidst.");
            // Lav opgaven herunder!
            int sum = 0; // Variabel til at gemme summen
            
            // Loop 5 gange for at få 5 tal
            for (int i = 1; i <= 5; i++)
            {
                Console.Write($"Indtast tal {i}: ");
                int tal = int.Parse(Console.ReadLine());
                sum += tal; // Læg tallet til summen
            }
            
            Console.WriteLine($"Summen af de 5 tal er: {sum}");
        }

        public static void Loop10()
        {
            Console.WriteLine("Opgave 10:");
            Console.WriteLine("Lav et program, hvor brugeren skal gætte et hemmeligt tal mellem 1 og 10. Brug et loop, så brugeren kan gætte indtil det rigtige tal er fundet.");
            // Lav opgaven herunder!
            Random random = new Random();
            int hemmeligtTal = random.Next(1, 11); // Tilfældigt tal mellem 1 og 10
            int gæt = 0;
            int antalForsøg = 0;
            
            Console.WriteLine("Jeg tænker på et tal mellem 1 og 10. Kan du gætte det?");
            
            // While-loop der kører indtil brugeren gætter rigtigt
            while (gæt != hemmeligtTal)
            {
                Console.Write("Dit gæt: ");
                gæt = int.Parse(Console.ReadLine());
                antalForsøg++;
                
                if (gæt < hemmeligtTal)
                {
                    Console.WriteLine("For lavt! Prøv igen.");
                }
                else if (gæt > hemmeligtTal)
                {
                    Console.WriteLine("For højt! Prøv igen.");
                }
                else
                {
                    Console.WriteLine($"Tillykke! Du gættede rigtigt på {antalForsøg} forsøg!");
                }
            }
        }

        public static void BankeBøf()
        {
            Console.WriteLine(@"Lav et program med et loop, som udskriver tallene fra 1 til 30. 
            Udskriv 'Banke' hvis tallet er deleligt med 3, 'Bøf' hvis tallet er deleligt med 5 
            og 'BankeBøf' hvis tallet er deleligt med både 3 og 5.");
            // Lav opgaven herunder!
            
            // Loop gennem tallene fra 1 til 30
            for (int i = 1; i <= 30; i++)
            {
                // Tjek først om tallet er deleligt med både 3 og 5 (15)
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("BankeBøf");
                }
                // Så tjek om det er deleligt med 3
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Banke");
                }
                // Så tjek om det er deleligt med 5
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Bøf");
                }
                // Ellers udskriv tallet selv
                else
                {
                    Console.WriteLine(i);
                }
            }
        }
        public static void MiniProjektLommeregner()
        {
            Console.WriteLine("\nMini-projekt: Simpel lommeregner (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine("Lav et program, hvor brugeren indtaster to tal og vælger en regneart (+, -, * eller /).");
            Console.WriteLine("Programmet skal udregne og udskrive resultatet.");
            Console.WriteLine("Tip: Brug if/else eller switch til at vælge regnearten.");
            // Lav opgaven herunder!
            
            bool fortsæt = true;
            
            // While-loop så brugeren kan lave flere beregninger
            while (fortsæt)
            {
                Console.WriteLine("\n--- Lommeregner ---");
                
                // Få første tal
                Console.Write("Indtast første tal: ");
                double tal1 = double.Parse(Console.ReadLine());
                
                // Få regneart
                Console.Write("Vælg regneart (+, -, *, /): ");
                string regneart = Console.ReadLine();
                
                // Få andet tal
                Console.Write("Indtast andet tal: ");
                double tal2 = double.Parse(Console.ReadLine());
                
                double resultat = 0;
                bool gyldigRegneart = true;
                
                // Switch statement til at udføre beregningen
                switch (regneart)
                {
                    case "+":
                        resultat = tal1 + tal2;
                        break;
                    case "-":
                        resultat = tal1 - tal2;
                        break;
                    case "*":
                        resultat = tal1 * tal2;
                        break;
                    case "/":
                        if (tal2 != 0)
                        {
                            resultat = tal1 / tal2;
                        }
                        else
                        {
                            Console.WriteLine("Fejl: Division med nul er ikke tilladt!");
                            gyldigRegneart = false;
                        }
                        break;
                    default:
                        Console.WriteLine("Ugyldig regneart! Brug +, -, * eller /");
                        gyldigRegneart = false;
                        break;
                }
                
                // Vis resultat hvis beregningen var gyldig
                if (gyldigRegneart)
                {
                    Console.WriteLine($"Resultat: {tal1} {regneart} {tal2} = {resultat}");
                }
                
                // Spørg om brugeren vil fortsætte
                Console.Write("\nVil du lave en ny beregning? (j/n): ");
                string svar = Console.ReadLine().ToLower();
                fortsæt = (svar == "j" || svar == "ja");
            }
            
            Console.WriteLine("Tak for at bruge lommeregneren!");
        }
    }
}