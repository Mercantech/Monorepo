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
