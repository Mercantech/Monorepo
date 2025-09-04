using System;
using System.Collections.Generic;
using System.Linq;

namespace Teori.LINQ
{
    public class LINQExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== LINQ Eksempel ===");
            var tal = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var ligeTal = tal.Where(x => x % 2 == 0).ToList();

            Console.WriteLine("Lige tal i listen:");
            foreach (var t in ligeTal)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine("Tryk på en tast for at vende tilbage til hovedmenuen...");
            Console.ReadKey();
        }
    }
}
