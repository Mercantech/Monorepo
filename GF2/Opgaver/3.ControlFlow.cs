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
                "Lav et program som tjekker om en værdi er højere eller lavere end 18. Brug if/else. Værdien kan du skrive fast i koden eller hente med Console.ReadLine()."
            );
            // TODO: Lav opgave 1 herunder!
        }

        public static void If2()
        {
            Console.WriteLine("Lav et program som tjekker om en værdi er lige eller ulige. Brug if og else.");
            // TODO: Lav opgave 2 herunder!
        }

        public static void Switch1()
        {
            Console.WriteLine("Lav et program som tjekker om en værdi er lige eller ulige – men denne gang ved at bruge switch.");
            // TODO: Lav opgave 3 herunder!
        }

        public static void Ternary1()
        {
            Console.WriteLine("Lav et program som tjekker om en værdi er lige eller ulige – brug ternary operator ( ? : ).");
            // TODO: Lav opgave 4 herunder!
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
            // TODO: Lav opgave 5 herunder!
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
            // TODO: Lav opgave 6 herunder!
        }
    }
}
