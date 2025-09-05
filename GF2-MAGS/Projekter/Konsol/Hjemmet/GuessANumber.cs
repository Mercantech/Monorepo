namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            // Initialiserer random generator
            Random random = new Random();
            // Definerer intervallet for det hemmelige tal
            int min = 1;
            int max = 100;
            // Genererer et tilfældigt tal mellem min og max (inklusive)
            int secretNumber = random.Next(min, max + 1);

            // Variabel til at holde styr på antallet af forsøg
            int attempts = 0;
            // Variabel til at holde brugerens gæt
            int guess = 0;
            // Flag for om tallet er gættet
            bool guessedCorrectly = false;

            Console.Clear();
            Console.WriteLine("Velkommen til 'Gæt et tal'!");
            Console.WriteLine($"Jeg tænker på et tal mellem {min} og {max}.");
            Console.WriteLine("Kan du gætte hvilket?");
            Console.WriteLine();

            // Spil-loopet fortsætter indtil brugeren gætter rigtigt
            while (!guessedCorrectly)
            {
                Console.Write("Indtast dit gæt: ");
                string input = Console.ReadLine();
                // Tjekker om input er et gyldigt tal
                if (int.TryParse(input, out guess))
                {
                    attempts++; // Øger antallet af forsøg

                    if (guess < min || guess > max)
                    {
                        Console.WriteLine($"Dit gæt skal være mellem {min} og {max}.");
                        continue;
                    }

                    if (guess < secretNumber)
                    {
                        Console.WriteLine("For lavt! Prøv igen.");
                    }
                    else if (guess > secretNumber)
                    {
                        Console.WriteLine("For højt! Prøv igen.");
                    }
                    else
                    {
                        // Brugeren har gættet rigtigt
                        guessedCorrectly = true;
                        Console.WriteLine($"Tillykke! Du gættede rigtigt på {attempts} forsøg.");
                    }
                }
                else
                {
                    // Hvis input ikke er et tal
                    Console.WriteLine("Ugyldigt input. Skriv et helt tal.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Tryk på en tast for at vende tilbage til menuen...");
            Console.ReadKey();
        }
    }
}
