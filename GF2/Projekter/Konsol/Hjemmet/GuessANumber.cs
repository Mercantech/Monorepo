using System.Runtime.InteropServices;

namespace Hjemmet
{
    public class GuessANumber
    {
        public void Start()
        {
            {
                Console.WriteLine("Gæt et tal mellem 1 og 10.");
                Random num = new Random();
                int answer = num.Next(1, 11); // 1–10 inklusiv
                int guess = 0;
                int attempts = 0;

                while (guess != answer)
                {
                    Console.Write("Indtast dit gæt: ");
                    string? input = Console.ReadLine();

                    if (input != null && int.TryParse(input, out guess))
                    {
                        attempts++;
                        if (guess < answer)
                        {
                            Console.WriteLine("For lavt! Prøv igen.");
                        }
                        else if (guess > answer)
                        {
                            Console.WriteLine("For højt! Prøv igen.");
                        }
                        else
                        {
                            Console.WriteLine($"Tillykke! Du gættede rigtigt på {attempts} forsøg.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt input. Indtast venligst et tal mellem 1 og 10.");
                    }
                }

                while (true)
                {
                    Console.Write("Spille igen? (ja/nej): ");
                    string? playAgain = Console.ReadLine()?.ToLower();
                    if (playAgain == "ja")
                    {
                        answer = num.Next(1, 11);
                        guess = 0;
                        attempts = 0;
                        Console.WriteLine("Ny runde! Gæt et tal mellem 1 og 10.");
                    }
                    else if (playAgain == "nej")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt input. Skriv 'ja' eller 'nej'.");
                    }

                    while (guess != answer)
                    {
                        Console.Write("Indtast dit gæt: ");
                        string? input = Console.ReadLine();

                        if (input != null && int.TryParse(input, out guess))
                        {
                            attempts++;
                            if (guess < answer)
                            {
                                Console.WriteLine("For lavt! Prøv igen.");
                            }
                            else if (guess > answer)
                            {
                                Console.WriteLine("For højt! Prøv igen.");
                            }
                            else
                            {
                                Console.WriteLine($"Tillykke! Du gættede rigtigt på {attempts} forsøg.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ugyldigt input. Indtast venligst et tal mellem 1 og 10.");
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Start");
                Console.ReadKey();
            }
        }
    }
}
