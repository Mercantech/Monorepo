using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Teori.JSON
{
    /// <summary>
    /// Book klassen repræsenterer en bog i systemet
    /// Den bruges til at gemme information om en enkelt bog
    /// Visual Studio gemmer JSON filen i mappen "bin/Debug/net9.0"
    /// Visual Studio Code gemmer JSON filen i samme mappe som vores Program.cs
    /// </summary>
    public class Book
    {
        // Unikt ID for hver bog
        public int Id { get; set; }

        // Bogens titel
        public string Title { get; set; }

        // Bogens forfatter
        public string Author { get; set; }

        // Året bogen blev udgivet
        public int Year { get; set; }
    }

    /// <summary>
    /// Hovedprogrammet der håndterer bogsystemet
    /// Implementerer CRUD operationer (Create, Read, Update, Delete)
    /// </summary>
    public class JsonExample
    {
        // Sti til JSON filen hvor data gemmes
        private static readonly string JsonFilePath = "books.json";

        // Liste der holder alle bøger i hukommelsen
        private static List<Book> books = new List<Book>();

        /// <summary>
        /// Starter JSON eksemplet
        /// </summary>
        public static void Run()
        {
            // Indlæs eksisterende bøger fra JSON filen
            LoadBooks();

            // Hovedmenu loop - kører indtil brugeren vælger at afslutte
            while (true)
            {
                Console.WriteLine("\nBook Management System");
                Console.WriteLine("1. View all books");
                Console.WriteLine("2. Add a new book");
                Console.WriteLine("3. Update a book");
                Console.WriteLine("4. Delete a book");
                Console.WriteLine("5. Return to main menu");
                Console.Write("Choose an option: ");

                // Læs brugerens valg og konverter til tal
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    // Håndter brugerens valg med en switch statement
                    switch (choice)
                    {
                        case 1:
                            ViewAllBooks();
                            break;
                        case 2:
                            AddBook();
                            break;
                        case 3:
                            UpdateBook();
                            break;
                        case 4:
                            DeleteBook();
                            break;
                        case 5:
                            return; // Returner til hovedmenuen
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Indlæser bøger fra JSON filen
        /// Hvis filen ikke findes, oprettes en tom liste
        /// </summary>
        static void LoadBooks()
        {
            Console.WriteLine("Loading books from " + Path.GetFullPath(JsonFilePath));
            if (File.Exists(JsonFilePath))
            {
                // Læs hele JSON filen
                string jsonString = File.ReadAllText(JsonFilePath);
                // Konverter JSON til en liste af Book objekter
                books = JsonSerializer.Deserialize<List<Book>>(jsonString) ?? new List<Book>();
            }
        }

        /// <summary>
        /// Gemmer den aktuelle liste af bøger til JSON filen
        /// Bruger pretty-printing for bedre læsbarhed
        /// </summary>
        static void SaveBooks()
        {
            // Konverter bøger til JSON format med indrykning
            string jsonString = JsonSerializer.Serialize(
                books,
                new JsonSerializerOptions { WriteIndented = true }
            );
            // Gem JSON til filen
            File.WriteAllText(JsonFilePath, jsonString);
        }

        /// <summary>
        /// Viser alle bøger i systemet
        /// Viser en besked hvis der ikke er nogen bøger
        /// </summary>
        static void ViewAllBooks()
        {
            Console.WriteLine("Loading books from " + Path.GetFullPath(JsonFilePath));

            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            // Gennemgå og vis hver bog
            foreach (var book in books)
            {
                Console.WriteLine(
                    $"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Year: {book.Year}"
                );
            }
        }

        /// <summary>
        /// Tilføjer en ny bog til systemet
        /// Genererer automatisk et nyt ID
        /// </summary>
        static void AddBook()
        {
            // Indsamling af bog information fra brugeren
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author name: ");
            string author = Console.ReadLine();
            Console.Write("Enter publication year: ");

            // Validering af årstal
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                // Generer nyt ID (højeste eksisterende ID + 1)
                int newId = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
                // Opret og tilføj ny bog
                books.Add(
                    new Book
                    {
                        Id = newId,
                        Title = title,
                        Author = author,
                        Year = year
                    }
                );
                // Gem ændringerne
                SaveBooks();
                Console.WriteLine("Book added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid year format.");
            }
        }

        /// <summary>
        /// Opdaterer en eksisterende bog
        /// Brugeren kan vælge at beholde eksisterende værdier
        /// </summary>
        static void UpdateBook()
        {
            Console.Write("Enter book ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Find bogen med det angivne ID
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    // Opdater titel hvis brugeren indtaster en ny
                    Console.Write("Enter new title (press Enter to keep current): ");
                    string title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                        book.Title = title;

                    // Opdater forfatter hvis brugeren indtaster en ny
                    Console.Write("Enter new author (press Enter to keep current): ");
                    string author = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(author))
                        book.Author = author;

                    // Opdater årstal hvis brugeren indtaster et nyt
                    Console.Write("Enter new year (press Enter to keep current): ");
                    string yearInput = Console.ReadLine();
                    if (int.TryParse(yearInput, out int year))
                        book.Year = year;

                    // Gem ændringerne
                    SaveBooks();
                    Console.WriteLine("Book updated successfully!");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        /// <summary>
        /// Sletter en bog fra systemet
        /// Brugeren skal angive ID på bogen der skal slettes
        /// </summary>
        static void DeleteBook()
        {
            Console.Write("Enter book ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Find bogen med det angivne ID
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    // Fjern bogen fra listen
                    books.Remove(book);
                    // Gem ændringerne
                    SaveBooks();
                    Console.WriteLine("Book deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }
}
