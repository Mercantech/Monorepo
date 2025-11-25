using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Banko Plade Scraping Demo ===\n");

            // Opret Chrome driver
            var chromeOptions = new ChromeOptions();
            // Fjern headless for at se hvad der sker (valgfrit)
            // chromeOptions.AddArgument("--headless");
            
            ChromeDriver? driver = null;
            
            try
            {
                driver = new ChromeDriver(chromeOptions);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                // Naviger til banko siden
                Console.WriteLine("1. Åbner banko siden...");
                driver.Navigate().GoToUrl("https://mercantech.github.io/Banko/");
                Thread.Sleep(1000); // Vent på at siden loader

                // Indtast plade ID
                string plateId = "12345"; // Eksempel ID
                Console.WriteLine($"2. Indtaster ID: {plateId}");
                var inputField = driver.FindElement(By.Id("tekstboks"));
                inputField.Clear();
                inputField.SendKeys(plateId);

                // Klik på "Generer plader" knappen
                Console.WriteLine("3. Klikker på 'Generer plader' knappen...");
                var generateButton = driver.FindElement(By.Id("knap"));
                generateButton.Click();
                Thread.Sleep(2000); // Vent på at pladen bliver genereret

                // Scrape alle tal fra de 3 rækker
                Console.WriteLine("4. Scraper tal fra pladen...\n");
                var allNumbers = new List<int>();

                // Gennemgå hver række (1, 2, 3)
                for (int row = 1; row <= 3; row++)
                {
                    var rowNumbers = new List<int>();
                    
                    // Gennemgå hver kolonne (1-9)
                    for (int col = 1; col <= 9; col++)
                    {
                        try
                        {
                            // Celle ID format: p1{row}{col} (f.eks. p111, p112, osv.)
                            string cellId = $"p1{row}{col}";
                            var cell = driver.FindElement(By.Id(cellId));
                            string cellText = cell.Text.Trim();
                            
                            // Hvis cellen indeholder et tal, tilføj det
                            if (!string.IsNullOrEmpty(cellText) && int.TryParse(cellText, out int number))
                            {
                                rowNumbers.Add(number);
                                allNumbers.Add(number);
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            // Celle findes ikke eller er tom - fortsæt
                            continue;
                        }
                    }
                    
                    // Vis rækken
                    rowNumbers.Sort();
                    Console.WriteLine($"Række {row}: {string.Join(", ", rowNumbers)}");
                }

                // Vis alle tal
                allNumbers.Sort();
                Console.WriteLine($"\nAlle tal på pladen: {string.Join(", ", allNumbers)}");
                Console.WriteLine($"\nTotal antal tal: {allNumbers.Count}");

                Console.WriteLine("\n✓ Scraping færdig!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Fejl: {ex.Message}");
            }
            finally
            {
                // Hold browseren åben i 5 sekunder så man kan se resultatet
                Console.WriteLine("\nLukker browser om 5 sekunder...");
                Thread.Sleep(5000);
                
                driver?.Quit();
                driver?.Dispose();
            }
        }
    }
}
