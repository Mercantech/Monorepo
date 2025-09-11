using System.Runtime.InteropServices;

namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            Console.WriteLine("Gæt et tal er ikke implementeret endnu.");

            // Random Number

            Random random = new Random();
            int numberToGuess = random.Next(1, 1000000000);

            Console.WriteLine($"Psssst, du skal gætte {numberToGuess}");

            Console.ReadKey();
        }
    }
}
