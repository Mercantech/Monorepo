using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Blazor.Services;

public class BankoScrapingService
{
    private readonly ILogger<BankoScrapingService> _logger;

    public BankoScrapingService(ILogger<BankoScrapingService> logger)
    {
        _logger = logger;
    }

    public async Task<BankoPlate> ScrapePlateAsync(string plateId, Action<string>? onProgress = null)
    {
        ChromeDriver? driver = null;
        try
        {
            // Opsæt Chrome options
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless"); // Kør i baggrunden
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-gpu");

            // Opret driver
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Naviger til siden
            string message = "Navigerer til https://mercantech.github.io/Banko/";
            _logger.LogInformation(message);
            onProgress?.Invoke(message);
            driver.Navigate().GoToUrl("https://mercantech.github.io/Banko/");

            // Vent på at siden er loaded
            await Task.Delay(1000);

            // Find input-feltet og indtast ID
            message = $"Indtaster ID: {plateId}";
            _logger.LogInformation(message);
            onProgress?.Invoke(message);
            var inputField = driver.FindElement(By.Id("tekstboks"));
            inputField.Clear();
            inputField.SendKeys(plateId);

            // Find og klik på "Generer plader" knappen
            message = "Klikker på Generer plader knappen";
            _logger.LogInformation(message);
            onProgress?.Invoke(message);
            var generateButton = driver.FindElement(By.Id("knap"));
            generateButton.Click();

            // Vent på at pladen bliver genereret
            message = "Venter på at pladen bliver genereret...";
            _logger.LogInformation(message);
            onProgress?.Invoke(message);
            await Task.Delay(2000); // Giv tid til JavaScript at køre

            // Scrape tallene fra tabellen
            var plate = new BankoPlate(plateId);
            
            // Scrape hver række (1, 2, 3)
            for (int row = 1; row <= 3; row++)
            {
                var rowNumbers = new List<int>();
                
                // Scrape hver kolonne (1-9)
                for (int col = 1; col <= 9; col++)
                {
                    try
                    {
                        // Celle ID format: p1{row}{col} (f.eks. p111, p112, osv.)
                        string cellId = $"p1{row}{col}";
                        var cell = driver.FindElement(By.Id(cellId));
                        
                        // Hent tekst fra cellen
                        string cellText = cell.Text.Trim();
                        
                        // Hvis cellen ikke er tom, konverter til tal
                        if (!string.IsNullOrEmpty(cellText) && int.TryParse(cellText, out int number))
                        {
                            rowNumbers.Add(number);
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        // Celle findes ikke eller er tom - ignorer
                        continue;
                    }
                }
                
                // Sorter talene og tilføj til den rigtige række
                rowNumbers.Sort();
                
                if (row == 1)
                    plate.Row0 = rowNumbers;
                else if (row == 2)
                    plate.Row1 = rowNumbers;
                else
                    plate.Row2 = rowNumbers;
            }

            int totalNumbers = plate.Row0.Count + plate.Row1.Count + plate.Row2.Count;
            message = $"Scraping færdig. Fundet {totalNumbers} tal";
            _logger.LogInformation(message);
            onProgress?.Invoke(message);
            
            return plate;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fejl under scraping af plade");
            throw;
        }
        finally
        {
            // Luk driver
            driver?.Quit();
            driver?.Dispose();
        }
    }
}

public class BankoPlate
{
    public string Id { get; set; } = string.Empty;
    public List<int> Row0 { get; set; } = new List<int>();
    public List<int> Row1 { get; set; } = new List<int>();
    public List<int> Row2 { get; set; } = new List<int>();

    public BankoPlate(string id)
    {
        Id = id;
        Row0 = new List<int>();
        Row1 = new List<int>();
        Row2 = new List<int>();
    }
}

