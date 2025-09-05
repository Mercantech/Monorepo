namespace Hjemmet
{
    public class RockPaperScissors
    {
        public void Start()
        {
            // Initialiserer random generator til computerens valg
            Random random = new Random();
            // Mulige valg
            string[] options = { "sten", "saks", "papir" };
            // Emoji mapping for valgene
            Dictionary<string, string> emojis = new Dictionary<string, string>
            {
                { "sten", "🗿" },
                { "saks", "✂️" },
                { "papir", "📄" }
            };

            // Pointtællere
            int playerScore = 0;
            int computerScore = 0;
            int round = 1;
            bool playAgain = true;
        
            Console.Clear();
            Console.WriteLine("🎲 Velkommen til 'Sten, Saks, Papir'! 🎲");
            Console.WriteLine("Du spiller mod computeren. Først til 3 point vinder spillet.\n");

            // Spil-loopet fortsætter indtil en af parterne har 3 point eller brugeren vil stoppe
            while (playAgain)
            {
                Console.WriteLine($"\nRunde {round}");
                Console.WriteLine($"Stilling: Du {playerScore} - Computer {computerScore}");
                Console.Write("Vælg (sten/saks/papir): ");
                string playerChoice = Console.ReadLine().ToLower().Trim();

                // Tjekker om brugerens valg er gyldigt
                if (!options.Contains(playerChoice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ugyldigt valg. Skriv 'sten', 'saks' eller 'papir'.");
                    Console.ResetColor();
                    continue;
                }

                // Computerens valg
                string computerChoice = options[random.Next(options.Length)];
                // Vis brugerens og computerens valg med emoji
                Console.WriteLine($"Du vælger: {playerChoice} {emojis[playerChoice]}");
                Console.WriteLine($"Computeren vælger: {computerChoice} {emojis[computerChoice]}");

                // Afgør runden
                if (playerChoice == computerChoice)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Uafgjort! 🤝");
                    Console.ResetColor();
                }
                else if (
                    (playerChoice == "sten" && computerChoice == "saks")
                    || (playerChoice == "saks" && computerChoice == "papir")
                    || (playerChoice == "papir" && computerChoice == "sten")
                )
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Du vinder runden! 🏆");
                    Console.ResetColor();
                    playerScore++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Computeren vinder runden! 💻");
                    Console.ResetColor();
                    computerScore++;
                }

                round++;

                // Tjek om nogen har vundet spillet
                if (playerScore == 3 || computerScore == 3)
                {
                    Console.WriteLine();
                    if (playerScore == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tillykke! Du har vundet spillet! 🥳");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Computeren har vundet spillet. Prøv igen! 😢");
                        Console.ResetColor();
                    }

                    Console.Write("Vil du spille igen? (ja/nej): ");
                    string again = Console.ReadLine().ToLower().Trim();
                    if (again == "ja")
                    {
                        // Nulstil point og runde
                        playerScore = 0;
                        computerScore = 0;
                        round = 1;
                        Console.Clear();
                        Console.WriteLine("Nyt spil starter!");
                    }
                    else
                    {
                        playAgain = false;
                    }
                }
            }

            Console.WriteLine(
                "\nTak for spillet! Tryk på en tast for at vende tilbage til menuen... 🎲"
            );
            Console.ReadKey();
        }
    }
}
