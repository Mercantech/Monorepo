using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Taxa.xaml
    /// </summary>
    public partial class Taxa : Page
    {
        public Taxa()
        {
            InitializeComponent();
        }

        private void CalculatePrice_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(kmTextBox.Text, out double km))
            {
                double baseFare = 75;
                double costPerKm = 2;
                double vomitCharge = vomitCheckBox.IsChecked == true ? 1500 : 0;
                double discount = 0;

                if (km > 200)
                {
                    discount = 0.30;
                }
                else if (km > 150)
                {
                    discount = 0.20;
                }
                else if (km > 100)
                {
                    discount = 0.10;
                }

                double kmCost = km * costPerKm * (1 - discount);
                double totalCost = baseFare + kmCost + vomitCharge;

                resultTextBlock.Text = $"Prisen for turen er: {totalCost} kr.";
            }
            else
            {
                resultTextBlock.Text = "Indtast venligst et gyldigt antal km.";
            }
        }
        private void NotionButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://mercantec.notion.site/Taxa-103dab5ca23780459c95fdbf758740f8?pvs=4";
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Der opstod en fejl ved åbning af linket: " + ex.Message);
            }
        }
    }
}
