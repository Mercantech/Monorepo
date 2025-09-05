using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;

namespace WPF
{
    /// <summary>
    /// Interaction logic for TypeRacer.xaml
    /// </summary>
    public partial class TypeRacer : Page
    {
        private List<string> texts = new List<string>
        {
            "Den hurtige brune ræv springer over den dovne hund.",
            "At programmere er som at skrive et digt; det kræver tålmodighed og kreativitet.",
            "C# er et moderne, objektorienteret programmeringssprog udviklet af Microsoft.",
            "WPF står for Windows Presentation Foundation og bruges til at lave desktop-applikationer."
        };

        private string currentText;
        private DateTime startTime;
        private bool isStarted = false;

        // Nye variabler til nøjagtighedsberegning
        private int totalKeystrokes = 0;
        private int correctKeystrokes = 0;
        private int previousInputLength = 0;

        public TypeRacer()
        {
            InitializeComponent();
            DisplayText.Document.Blocks.Clear();
            DisplayText.Document.Blocks.Add(new Paragraph(new Run("Klik på 'Start' for at begynde.")));
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Vælg en tilfældig tekst
            Random rand = new Random();
            int index = rand.Next(texts.Count);
            currentText = texts[index];

            // Vis teksten
            DisplayText.Document.Blocks.Clear();
            DisplayText.Document.Blocks.Add(new Paragraph(new Run(currentText)));

            // Nulstil inputfeltet
            InputTextBox.Text = "";
            InputTextBox.IsReadOnly = false;
            InputTextBox.Focus();

            // Nulstil resultater
            ResultPanel.Visibility = Visibility.Collapsed;
            LiveWPMText.Text = "0";
            LiveAccuracyText.Text = "100%";

            // Nulstil tastetryksvariabler
            totalKeystrokes = 0;
            correctKeystrokes = 0;
            previousInputLength = 0;

            // Start timeren
            startTime = DateTime.Now;
            isStarted = true;
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isStarted) return;

            // Øg totalKeystrokes baseret på ændringer i tekstlængden
            int changeInLength = InputTextBox.Text.Length - previousInputLength;
            totalKeystrokes += Math.Abs(changeInLength);
            previousInputLength = InputTextBox.Text.Length;

            // Opdatering af live WPM og nøjagtighed
            UpdateLiveStats();

            // Tjek om brugeren har færdiggjort teksten
            if (InputTextBox.Text == currentText)
            {
                // Stop timeren
                isStarted = false;
                InputTextBox.IsReadOnly = true;

                // Beregn endelige resultater
                TimeSpan duration = DateTime.Now - startTime;
                int wordCount = currentText.Split(' ').Length;
                double minutes = duration.TotalMinutes;
                int wpm = (int)(wordCount / minutes);

                // Beregn nøjagtighed
                double accuracy = ((double)correctKeystrokes / totalKeystrokes) * 100;
                if (accuracy < 0 || double.IsNaN(accuracy) || double.IsInfinity(accuracy)) accuracy = 0;

                // Vis endelige resultater
                FinalWPMText.Text = $"Skrivehastighed: {wpm} ord per minut";
                FinalAccuracyText.Text = $"Nøjagtighed: {accuracy:F2}%";
                ResultPanel.Visibility = Visibility.Visible;
            }
            else
            {
                // Opdatering af brugerens position og fejlmarkering
                HighlightText();
            }
        }

        private void UpdateLiveStats()
        {
            TimeSpan duration = DateTime.Now - startTime;
            double minutes = duration.TotalMinutes;

            if (minutes > 0)
            {
                // Beregn WPM
                int wordsTyped = InputTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                int wpm = (int)(wordsTyped / minutes);
                LiveWPMText.Text = wpm.ToString();
            }

            // Beregn antallet af korrekte tastetryk
            correctKeystrokes = 0;
            string userInput = InputTextBox.Text;
            int minLength = Math.Min(userInput.Length, currentText.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (currentText[i] == userInput[i])
                {
                    correctKeystrokes++;
                }
            }

            // Beregn nøjagtighed
            double accuracy = ((double)correctKeystrokes / totalKeystrokes) * 100;
            if (accuracy < 0 || double.IsNaN(accuracy) || double.IsInfinity(accuracy)) accuracy = 0;
            LiveAccuracyText.Text = $"{accuracy:F2}%";
        }

        private void HighlightText()
        {
            string userInput = InputTextBox.Text;

            // Nulstil formatering
            DisplayText.Document.Blocks.Clear();
            Paragraph para = new Paragraph();
            DisplayText.Document.Blocks.Add(para);

            int minLength = Math.Min(userInput.Length, currentText.Length);

            for (int i = 0; i < currentText.Length; i++)
            {
                Run run = new Run(currentText[i].ToString());

                if (i < minLength)
                {
                    if (currentText[i] == userInput[i])
                    {
                        run.Foreground = Brushes.Black;
                    }
                    else
                    {
                        run.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    run.Foreground = Brushes.Gray;
                }

                para.Inlines.Add(run);
            }
        }

        private void RetryButton_Click(object sender, RoutedEventArgs e)
        {
            // Nulstil til starttilstand
            DisplayText.Document.Blocks.Clear();
            DisplayText.Document.Blocks.Add(new Paragraph(new Run("Klik på 'Start' for at begynde.")));
            InputTextBox.Text = "";
            InputTextBox.IsReadOnly = true;
            ResultPanel.Visibility = Visibility.Collapsed;
            LiveWPMText.Text = "";
            LiveAccuracyText.Text = "";
            isStarted = false;

            // Nulstil tastetryksvariabler
            totalKeystrokes = 0;
            correctKeystrokes = 0;
            previousInputLength = 0;
        }

        private void NotionButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://mercantec.notion.site/TypeRacer-112dab5ca237808dafe7d15d8da2ea8a";
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
