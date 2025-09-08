using System;

namespace Opgaver
{
    public class ControlFlow
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(
                @"Velkommen til opgaver omkring Control Flow med if, else if og else, 
            Switch og Ternary operator!"
            );
            If1();
            If2();

            Switch1();
            Ternary1();

            MiniProjektQuiz();
            MiniProjektKarakterFeedback();
        }

        public static void If1()
        {
            Console.WriteLine(
                "Lav et program som tjekker om en given værdi er højere eller lavere end 18"
            );
            // Lav opgaven herunder!
            Console.Write("Indtast en værdi: ");
            string input = Console.ReadLine();
            int værdi = int.Parse(input);
            
            // If-else statement til at tjekke om værdien er højere eller lavere end 18
            if (værdi > 18)
            {
                Console.WriteLine($"{værdi} er højere end 18");
            }
            else if (værdi < 18)
            {
                Console.WriteLine($"{værdi} er lavere end 18");
            }
            else
            {
                Console.WriteLine($"{værdi} er lig med 18");
            }
        }

        public static void If2()
        {
            Console.WriteLine("Lav et program som tjekker om en given værdi er lige eller ulige");
            // Lav opgaven herunder!
            Console.Write("Indtast et tal: ");
            string input = Console.ReadLine();
            int tal = int.Parse(input);
            
            // Modulo operator (%) giver resten ved division
            // Hvis resten er 0 når vi dividerer med 2, er tallet lige
            if (tal % 2 == 0)
            {
                Console.WriteLine($"{tal} er et lige tal");
            }
            else
            {
                Console.WriteLine($"{tal} er et ulige tal");
            }
        }

        public static void Switch1()
        {
            Console.WriteLine("Lav et program som tjekker ugedagen baseret på et tal (1-7)");
            // Lav opgaven herunder!
            Console.Write("Indtast et tal mellem 1-7 for ugedag: ");
            string input = Console.ReadLine();
            int dag = int.Parse(input);
            
            // Switch statement til at tjekke ugedag
            switch (dag)
            {
                case 1:
                    Console.WriteLine("Mandag");
                    break;
                case 2:
                    Console.WriteLine("Tirsdag");
                    break;
                case 3:
                    Console.WriteLine("Onsdag");
                    break;
                case 4:
                    Console.WriteLine("Torsdag");
                    break;
                case 5:
                    Console.WriteLine("Fredag");
                    break;
                case 6:
                    Console.WriteLine("Lørdag");
                    break;
                case 7:
                    Console.WriteLine("Søndag");
                    break;
                default:
                    Console.WriteLine("Ugyldigt tal! Indtast et tal mellem 1-7");
                    break;
            }
        }

        public static void Ternary1()
        {
            Console.WriteLine("Lav et program som tjekker om en given værdi er lige eller ulige med ternary operator");
            // Lav opgaven herunder!
            Console.Write("Indtast et tal: ");
            string input = Console.ReadLine();
            int tal = int.Parse(input);
            
            // Ternary operator: condition ? valueIfTrue : valueIfFalse
            // Kortere måde at skrive if-else på én linje
            string resultat = (tal % 2 == 0) ? "lige" : "ulige";
            Console.WriteLine($"{tal} er et {resultat} tal");
        }

        public static void MiniProjektQuiz()
        {
            Console.WriteLine("\nMini-projekt: Simpelt quiz-spil (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                "Lav et program, der stiller brugeren tre spørgsmål (du vælger selv spørgsmål og svar)."
            );
            Console.WriteLine("Brugeren skal indtaste sit svar til hvert spørgsmål.");
            Console.WriteLine(
                "Programmet skal tjekke, om svaret er rigtigt eller forkert, og til sidst udskrive, hvor mange rigtige brugeren fik."
            );
            Console.WriteLine(
                "Tip: Brug variabler til at gemme point og svar, og if/else til at tjekke svarene."
            );
            // Lav opgaven herunder!
            
            int point = 0; // Tæller for rigtige svar
            
            // Spørgsmål 1
            Console.WriteLine("\nSpørgsmål 1: Hvad er hovedstaden i Danmark?");
            Console.Write("Dit svar: ");
            string svar1 = Console.ReadLine();
            if (svar1.ToLower() == "københavn")
            {
                Console.WriteLine("Rigtigt!");
                point++;
            }
            else
            {
                Console.WriteLine("Forkert! Det rigtige svar er København.");
            }
            
            // Spørgsmål 2
            Console.WriteLine("\nSpørgsmål 2: Hvor mange ben har en edderkop?");
            Console.Write("Dit svar: ");
            string svar2 = Console.ReadLine();
            if (svar2 == "8")
            {
                Console.WriteLine("Rigtigt!");
                point++;
            }
            else
            {
                Console.WriteLine("Forkert! Det rigtige svar er 8.");
            }
            
            // Spørgsmål 3
            Console.WriteLine("\nSpørgsmål 3: Hvad er 5 + 3?");
            Console.Write("Dit svar: ");
            string svar3 = Console.ReadLine();
            if (svar3 == "8")
            {
                Console.WriteLine("Rigtigt!");
                point++;
            }
            else
            {
                Console.WriteLine("Forkert! Det rigtige svar er 8.");
            }
            
            // Vis resultat
            Console.WriteLine($"\nDu fik {point} ud af 3 rigtige!");
            
            // Feedback baseret på score
            if (point == 3)
            {
                Console.WriteLine("Perfekt! Du er en quiz-mester!");
            }
            else if (point >= 2)
            {
                Console.WriteLine("Godt klaret!");
            }
            else
            {
                Console.WriteLine("Øv dig lidt mere og prøv igen!");
            }
        }

        public static void MiniProjektKarakterFeedback()
        {
            Console.WriteLine("\nMini-projekt: Karakter-feedback (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                "Lav et program, hvor brugeren indtaster en karakter (fx 12, 10, 7, 4, 02, 00 eller -3)."
            );
            Console.WriteLine(
                @"Programmet skal give en passende feedback baseret på karakteren, 
            fx 'Super flot!', 'Godt klaret', 'Du kan gøre det bedre' osv."
            );
            Console.WriteLine("Brug if/else eller switch til at vælge feedbacken.");

            Console.WriteLine(
                @"Ekstra opgave: Lav så man indtaster flere karaktere 
            for en bruger og man regner gennemsnittet ud."
            );
            // Lav opgaven herunder!
            
            Console.Write("Hvor mange karakterer vil du indtaste? ");
            int antalKarakterer = int.Parse(Console.ReadLine());
            
            int sumKarakterer = 0;
            
            // Loop gennem alle karakterer
            for (int i = 1; i <= antalKarakterer; i++)
            {
                Console.Write($"Indtast karakter {i}: ");
                int karakter = int.Parse(Console.ReadLine());
                sumKarakterer += karakter;
                
                // Giv feedback for hver karakter
                string feedback = karakter switch
                {
                    12 => "Fremragende! Topkarakter!",
                    10 => "Super flot arbejde!",
                    7 => "Godt klaret!",
                    4 => "Bestået - du kan gøre det bedre!",
                    2 => "Ikke bestået - øv dig mere!",
                    0 => "Ikke bestået - du skal arbejde hårdere!",
                    -3 => "Meget dårligt - du mangler at forstå det grundlæggende!",
                    _ => "Ugyldig karakter! Brug 12, 10, 7, 4, 02, 00 eller -3"
                };
                
                Console.WriteLine($"Feedback: {feedback}");
            }
            
            // Beregn og vis gennemsnit
            double gennemsnit = (double)sumKarakterer / antalKarakterer;
            Console.WriteLine($"\nDit gennemsnit er: {gennemsnit:F2}");
            
            // Samlet feedback baseret på gennemsnit
            if (gennemsnit >= 10)
            {
                Console.WriteLine("Fantastisk gennemsnit! Du klarer dig rigtig godt!");
            }
            else if (gennemsnit >= 7)
            {
                Console.WriteLine("Godt gennemsnit! Fortsæt det gode arbejde!");
            }
            else if (gennemsnit >= 4)
            {
                Console.WriteLine("Okay gennemsnit. Der er plads til forbedring!");
            }
            else
            {
                Console.WriteLine("Du skal arbejde hårdere for at forbedre dine karakterer!");
            }
        }
    }
}
