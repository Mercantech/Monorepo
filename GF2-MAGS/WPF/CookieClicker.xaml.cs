using System;
using System.IO;
using System.Media;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CookieClicker.xaml
    /// </summary>
    public partial class CookieClicker : Page
    {
        private int cookieCount = 0;
        private int grandmas = 0;
        private int factories = 0;
        private int mines = 0;

        private int grandmaCost = 100;
        private int factoryCost = 500;
        private int mineCost = 2000;

        private DispatcherTimer timer;
        private readonly string saveFilePath = "gamestate.json";

        private SoundPlayer clickSoundPlayer;

        public CookieClicker()
        {
            InitializeComponent();

            LoadGame();
            UpdateUI();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            cookieCount += grandmas * 1;
            cookieCount += factories * 5;
            cookieCount += mines * 20;

            UpdateUI();
            SaveGame();
        }

        private void CookieButton_Click(object sender, RoutedEventArgs e)
        {
            cookieCount++;
            UpdateUI();

            AnimateCookieClick(sender as Button);
        }

        private void BuyGrandma_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= grandmaCost)
            {
                cookieCount -= grandmaCost;
                grandmas++;
                grandmaCost = (int)(grandmaCost * 1.15); // Øger omkostningen med 15%
                UpdateUI();
                SaveGame();
            }
            else
            {
                MessageBox.Show("Du har ikke nok cookies til at købe en Grandma!", "Manglende Cookies", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BuyFactory_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= factoryCost)
            {
                cookieCount -= factoryCost;
                factories++;
                factoryCost = (int)(factoryCost * 1.15); // Øger omkostningen med 15%
                UpdateUI();
                SaveGame();
            }
            else
            {
                MessageBox.Show("Du har ikke nok cookies til at købe en Factory!", "Manglende Cookies", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BuyMine_Click(object sender, RoutedEventArgs e)
        {
            if (cookieCount >= mineCost)
            {
                cookieCount -= mineCost;
                mines++;
                mineCost = (int)(mineCost * 1.15); // Øger omkostningen med 15%
                UpdateUI();
                SaveGame();
            }
            else
            {
                MessageBox.Show("Du har ikke nok cookies til at købe en Mine!", "Manglende Cookies", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateUI()
        {
            cookieCountTextBlock.Text = cookieCount.ToString("N0");
            grandmaCountTextBlock.Text = $"Grandmas: {grandmas}";
            factoryCountTextBlock.Text = $"Factories: {factories}";
            mineCountTextBlock.Text = $"Mines: {mines}";

            BuyGrandmaButton.Content = $"Buy Grandma (Cost: {grandmaCost:N0})";
            BuyFactoryButton.Content = $"Buy Factory (Cost: {factoryCost:N0})";
            BuyMineButton.Content = $"Buy Mine (Cost: {mineCost:N0})";

            BuyGrandmaButton.IsEnabled = cookieCount >= grandmaCost;
            BuyFactoryButton.IsEnabled = cookieCount >= factoryCost;
            BuyMineButton.IsEnabled = cookieCount >= mineCost;
        }

        private void SaveGame()
        {
            try
            {
                var state = new GameState
                {
                    CookieCount = this.cookieCount,
                    Grandmas = this.grandmas,
                    Factories = this.factories,
                    Mines = this.mines,
                    GrandmaCost = this.grandmaCost,
                    FactoryCost = this.factoryCost,
                    MineCost = this.mineCost
                };
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(state, jsonOptions);
                File.WriteAllText(saveFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under gemning: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadGame()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    var json = File.ReadAllText(saveFilePath);
                    var state = JsonSerializer.Deserialize<GameState>(json);
                    if (state != null)
                    {
                        this.cookieCount = state.CookieCount;
                        this.grandmas = state.Grandmas;
                        this.factories = state.Factories;
                        this.mines = state.Mines;
                        this.grandmaCost = state.GrandmaCost;
                        this.factoryCost = state.FactoryCost;
                        this.mineCost = state.MineCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under indlæsning af spil: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AnimateCookieClick(Button button)
        {
            var scaleUp = new DoubleAnimation(1.0, 1.2, TimeSpan.FromMilliseconds(100));
            var scaleDown = new DoubleAnimation(1.2, 1.0, TimeSpan.FromMilliseconds(100)) { BeginTime = TimeSpan.FromMilliseconds(100) };

            var transform = new ScaleTransform(1.0, 1.0);
            button.RenderTransform = transform;
            button.RenderTransformOrigin = new Point(0.5, 0.5);
            transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleUp);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleUp);
            transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleDown);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleDown);
        }
    }

    // Klasse til at gemme spiltilstand
    public class GameState
    {
        public int CookieCount { get; set; }
        public int Grandmas { get; set; }
        public int Factories { get; set; }
        public int Mines { get; set; }
        public int GrandmaCost { get; set; }
        public int FactoryCost { get; set; }
        public int MineCost { get; set; }
    }
}
