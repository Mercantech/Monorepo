using System;

namespace Teori
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Teori Eksempler ===");
                Console.WriteLine("1. JSON Eksempler");
                Console.WriteLine("2. SQL Eksempler (Under udvikling)");
                Console.WriteLine("3. LINQ Eksempler (Under udvikling)");
                Console.WriteLine("4. Afslut");
                Console.Write("\nVælg et eksempel (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            JSON.JsonExample.Run();
                            break;
                        case 2:
                            SQL.SQLExample.Run();
                            break;
                        case 3:
                            LINQ.LINQExample.Run();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Ugyldigt valg. Prøv igen.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Indtast venligst et gyldigt tal.");
                }
            }
        }
    }
}
