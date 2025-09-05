using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Wordle.xaml
    /// </summary>
    public partial class Wordle : Page
    {
        private List<string> wordList = new List<string>();
        private List<TextBlock> cells = new List<TextBlock>();
        private string targetWord;
        private int currentRow = 0;
        private bool gameOver = false;

        public Wordle()
        {
            InitializeComponent();
            _ = InitializeGame();
        }

        private async Task InitializeGame()
        {
            await LoadWordList();
            CreateBoard();
            StartNewGame();
        }


        private async Task LoadWordList()
        {
            try
            {
                string apiUrl = "https://opgaver.magsapi.com/Opgaver/Wordle";

                using (var client = new HttpClient())
                {
                    // Send GET-anmodning til API'et
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Læs svarindhold som en streng
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Parse JSON-responsen
                        var jsonDoc = JsonDocument.Parse(jsonResponse);

                        // Ekstraher "wordlist" arrayet
                        if (jsonDoc.RootElement.TryGetProperty("wordlist", out JsonElement wordListElement))
                        {
                            wordList = new List<string>();

                            foreach (var wordElement in wordListElement.EnumerateArray())
                            {
                                string word = wordElement.GetString().Trim();

                                // Sørg for, at ordet er præcis 5 bogstaver langt
                                if (word.Length == 5)
                                {
                                    wordList.Add(word.ToLower());
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("JSON-responsen indeholder ikke 'wordlist'-egenskaben.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kunne ikke hente ordlisten fra API'et. Statuskode: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved indlæsning af ordlisten fra API'et: " + ex.Message);
            }
        }



        private void CreateBoard()
        {
            // Find UniformGrid fra XAML
            UniformGrid grid = GameGrid;

            // Ryd eksisterende celler
            grid.Children.Clear();
            cells.Clear();

            // Opret 30 celler (6 rækker * 5 kolonner)
            for (int i = 0; i < 30; i++)
            {
                TextBlock cell = new TextBlock
                {
                    Text = "",
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(2),
                    Background = Brushes.LightGray
                };
                Border border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Child = cell
                };
                grid.Children.Add(border);
                cells.Add(cell);
            }
        }

        private void StartNewGame()
        {
            if (wordList.Count == 0)
            {
                MessageBox.Show("Ordlisten er tom. Kan ikke starte spillet.");
                return;
            }

            // Vælg et tilfældigt ord
            Random rand = new Random();
            targetWord = wordList[rand.Next(wordList.Count)].ToUpper();

            // Nulstil spilvariabler
            currentRow = 0;
            gameOver = false;
            MessageTextBlock.Text = "";
            InputTextBox.Text = "";
            InputTextBox.IsEnabled = true;

            // Ryd brættet
            foreach (var cell in cells)
            {
                cell.Text = "";
                cell.Background = Brushes.LightGray;
            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !gameOver)
            {
                string guess = InputTextBox.Text.ToUpper();
                if (guess.Length != 5)
                {
                    MessageTextBlock.Text = "Ordet skal være 5 bogstaver.";
                    return;
                }

                if (!wordList.Contains(guess.ToLower()))
                {
                    MessageTextBlock.Text = "Ordet er ikke i ordlisten.";
                    return;
                }

                // Opdater brættet
                UpdateBoard(guess);
                InputTextBox.Text = "";
            }
        }

        private void UpdateBoard(string guess)
        {
            char[] guessChars = guess.ToCharArray();
            char[] targetChars = targetWord.ToCharArray();
            Brush[] colors = new Brush[5];

            // Først markér alle bogstaver som grå
            for (int i = 0; i < 5; i++)
            {
                colors[i] = Brushes.Gray;
            }

            // Find grønne bogstaver (korrekt bogstav og placering)
            for (int i = 0; i < 5; i++)
            {
                if (guessChars[i] == targetChars[i])
                {
                    colors[i] = Brushes.Green;
                    targetChars[i] = '_'; // Fjern bogstavet fra targetChars
                    guessChars[i] = '-';  // Markér som allerede matchet
                }
            }

            // Find gule bogstaver (korrekt bogstav, forkert placering)
            for (int i = 0; i < 5; i++)
            {
                if (guessChars[i] != '-')
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (guessChars[i] == targetChars[j])
                        {
                            colors[i] = Brushes.Gold;
                            targetChars[j] = '_'; // Fjern bogstavet fra targetChars
                            break;
                        }
                    }
                }
            }

            // Opdater cellerne i den aktuelle række
            for (int i = 0; i < 5; i++)
            {
                int cellIndex = currentRow * 5 + i;
                cells[cellIndex].Text = guess[i].ToString();
                cells[cellIndex].Background = colors[i];
            }

            // Tjek for sejr eller nederlag
            if (guess == targetWord)
            {
                MessageTextBlock.Text = "Du har vundet!";
                gameOver = true;
                InputTextBox.IsEnabled = false;
            }
            else if (currentRow == 5)
            {
                MessageTextBlock.Text = $"Du har tabt! Ordet var: {targetWord}";
                gameOver = true;
                InputTextBox.IsEnabled = false;
            }
            else
            {
                currentRow++;
                MessageTextBlock.Text = ""; // Ryd eventuelle tidligere meddelelser
            }
        }

        private void StartNewGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        // Beholder din eksisterende NotionButton_Click metode, hvis du har den i din XAML
        private void NotionButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://mercantec.notion.site/Wordle-112dab5ca23780fa81e6e44c3b97da4f";
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der opstod en fejl ved åbning af linket: " + ex.Message);
            }
        }
    }
}
