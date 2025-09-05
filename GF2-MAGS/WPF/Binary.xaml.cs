using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Binary.xaml
    /// </summary>
    public partial class Binary : Page
    {
        public Binary()
        {
            InitializeComponent();
        }

        // Event handler for konvertering fra binær til decimal
        private void ConvertToDecimal_Click(object sender, RoutedEventArgs e)
        {
            string binaryInput = BinaryInput.Text;
            string[] binaryNumbers = binaryInput.Split('.');

            if (binaryNumbers.Length != 4)
            {
                MessageBox.Show("Indtast venligst fire binære tal adskilt af punktummer.");
                return;
            }

            string decimalResult = "";

            foreach (string binary in binaryNumbers)
            {
                if (binary.Length != 8 || !IsBinary(binary))
                {
                    MessageBox.Show("Hvert binært tal skal være 8 cifre langt og kun bestå af 0 og 1.");
                    return;
                }

                int decimalNumber = BinaryToDecimal(binary);
                decimalResult += decimalNumber.ToString() + ".";
            }

            // Fjern det sidste punktum
            decimalResult = decimalResult.TrimEnd('.');

            DecimalOutput.Text = "Decimal: " + decimalResult;
        }

        // Event handler for konvertering fra decimal til binær
        private void ConvertToBinary_Click(object sender, RoutedEventArgs e)
        {
            string decimalInput = DecimalInput.Text;
            string[] decimalNumbers = decimalInput.Split('.');

            if (decimalNumbers.Length != 4)
            {
                MessageBox.Show("Indtast venligst fire decimaltal adskilt af punktummer.");
                return;
            }

            string binaryResult = "";

            foreach (string decimalStr in decimalNumbers)
            {
                if (!int.TryParse(decimalStr, out int decimalNumber))
                {
                    MessageBox.Show("Indtast venligst gyldige heltal.");
                    return;
                }

                if (decimalNumber < 0 || decimalNumber > 255)
                {
                    MessageBox.Show("Hvert tal skal være mellem 0 og 255.");
                    return;
                }

                string binary = DecimalToBinary(decimalNumber);
                binaryResult += binary + ".";
            }

            // Fjern det sidste punktum
            binaryResult = binaryResult.TrimEnd('.');

            BinaryOutput.Text = "Binær: " + binaryResult;
        }

        // Metode til at konvertere binær til decimal uden indbyggede funktioner
        private int BinaryToDecimal(string binary)
        {
            int decimalNumber = 0;
            int exponent = 0;

            // Gå igennem binærtal fra højre mod venstre
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1')
                {
                    decimalNumber += Power(2, exponent);
                }
                exponent++;
            }

            return decimalNumber;
        }

        // Metode til at konvertere decimal til binær uden indbyggede funktioner
        private string DecimalToBinary(int decimalNumber)
        {
            string binary = "";

            do
            {
                int remainder = decimalNumber % 2;
                binary = remainder + binary;
                decimalNumber = decimalNumber / 2;
            } while (decimalNumber > 0);

            // Sørg for, at det binære tal er 8 cifre langt
            while (binary.Length < 8)
            {
                binary = "0" + binary;
            }

            return binary;
        }

        // Hjælpemetode til potensberegning uden Math.Pow
        private int Power(int baseNum, int exponent)
        {
            int result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= baseNum;
            }
            return result;
        }

        // Metode til at tjekke om en streng kun indeholder 0 og 1
        private bool IsBinary(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }

        // Behold din eksisterende NotionButton_Click metode
        private void NotionButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://mercantec.notion.site/Bin-r-Kodeomformer-112dab5ca237807fb757e18a0b3ba76f";
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
