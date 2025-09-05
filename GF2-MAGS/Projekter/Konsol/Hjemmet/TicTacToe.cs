namespace Hjemmet
{
    // Klasse der repræsenterer Tic-Tac-Toe brættet
    public class Board
    {
        private char[] grid = new char[9];

        // Initialiserer brættet med tal 0-8
        public Board()
        {
            for (int i = 0; i < 9; i++)
                grid[i] = (char)('0' + i);
        }

        // Viser brættet i konsollen
        public void Print()
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < 3; j++)
                {
                    int idx = i * 3 + j;
                    char c = grid[idx];
                    // Hvis feltet er X eller O, vis det, ellers vis tallet
                    Console.Write(c == 'X' || c == 'O' ? c : (char)('0' + idx));
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("---+---+---");
            }
            Console.WriteLine();
        }

        // Forsøger at placere et symbol på brættet. Returnerer true hvis det lykkes.
        public bool Place(int pos, char symbol)
        {
            if (pos < 0 || pos > 8)
                return false;
            if (grid[pos] == 'X' || grid[pos] == 'O')
                return false;
            grid[pos] = symbol;
            return true;
        }

        // Tjekker om en spiller har vundet
        public bool CheckWin(char symbol)
        {
            int[,] winPatterns = new int[,]
            {
                {0,1,2}, {3,4,5}, {6,7,8}, // rækker
                {0,3,6}, {1,4,7}, {2,5,8}, // kolonner
                {0,4,8}, {2,4,6}           // diagonaler
            };
            for (int i = 0; i < winPatterns.GetLength(0); i++)
            {
                if (grid[winPatterns[i,0]] == symbol &&
                    grid[winPatterns[i,1]] == symbol &&
                    grid[winPatterns[i,2]] == symbol)
                    return true;
            }
            return false;
        }

        // Tjekker om brættet er fyldt (uafgjort)
        public bool IsFull()
        {
            for (int i = 0; i < 9; i++)
                if (grid[i] != 'X' && grid[i] != 'O')
                    return false;
            return true;
        }
    }

    public class TicTacToeGame
    {
        private Board board;
        private char currentPlayer;

        public TicTacToeGame()
        {
            board = new Board();
            currentPlayer = 'X';
        }

        // Skifter spiller
        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        // Hovedspil-loopet
        public void Play()
        {
            bool gameEnded = false;
            while (!gameEnded)
            {
                Console.Clear();
                Console.WriteLine("Tic-Tac-Toe");
                board.Print();
                Console.WriteLine($"Spiller {currentPlayer}, det er din tur.");
                int pos;
                while (true)
                {
                    Console.Write("Vælg et felt (0-8): ");
                    if (!int.TryParse(Console.ReadLine(), out pos) || pos < 0 || pos > 8)
                    {
                        Console.WriteLine("Ugyldigt felt. Prøv igen.");
                        continue;
                    }
                    if (!board.Place(pos, currentPlayer))
                    {
                        Console.WriteLine("Feltet er allerede optaget. Prøv igen.");
                        continue;
                    }
                    break;
                }
                // Tjek for vinder
                if (board.CheckWin(currentPlayer))
                {
                    Console.Clear();
                    board.Print();
                    Console.WriteLine($"Spiller {currentPlayer} vinder! 🎉");
                    gameEnded = true;
                }
                else if (board.IsFull())
                {
                    Console.Clear();
                    board.Print();
                    Console.WriteLine("Uafgjort! Ingen vinder denne gang.");
                    gameEnded = true;
                }
                else
                {
                    SwitchPlayer();
                }
            }
            Console.WriteLine("Tryk på en tast for at vende tilbage til menuen...");
            Console.ReadKey();
        }
    }

    public class TicTacToe
    {
        public void Start()
        {
            // Starter et nyt spil
            TicTacToeGame game = new TicTacToeGame();
            game.Play();
        }
    }
}
