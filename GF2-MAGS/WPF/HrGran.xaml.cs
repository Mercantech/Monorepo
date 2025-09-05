using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Interaction logic for HrGran.xaml
    /// </summary>
    public partial class HrGran : Page
    {
        public HrGran()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lengthTextBox.Text, out double length) && double.TryParse(widthTextBox.Text, out double width))
            {
                double area = length * width;
                string treeType = (treeTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (treeType == null)
                {
                    resultTextBlock.Text = "Vælg venligst en type af træer.";
                    return;
                }

                double treeCost = 0;
                double salePrice = 0;
                double treesPerM2 = 0;
                double soilPreparationCost = 0;
                double expectedLoss = 0;

                switch (treeType)
                {
                    case "Ædelgran":
                        treeCost = 15;
                        salePrice = 75;
                        treesPerM2 = 1;
                        soilPreparationCost = 5;
                        expectedLoss = 0.10;
                        break;
                    case "Nordmannsgran":
                        treeCost = 13;
                        salePrice = 85;
                        treesPerM2 = 1;
                        soilPreparationCost = 8;
                        expectedLoss = 0.15;
                        break;
                    case "Rødgran":
                        treeCost = 10;
                        salePrice = 45;
                        treesPerM2 = 2;
                        soilPreparationCost = 4;
                        expectedLoss = 0.20;
                        break;
                }

                double numberOfTrees = area * treesPerM2;
                double totalTreeCost = numberOfTrees * treeCost;
                double totalSoilPreparationCost = area * soilPreparationCost;
                double totalCost = totalTreeCost + totalSoilPreparationCost;

                double expectedSurvivingTrees = numberOfTrees * (1 - expectedLoss);
                double totalRevenue = expectedSurvivingTrees * salePrice;

                resultTextBlock.Text = $"Antal træer der kan plantes: {numberOfTrees}\n" +
                                       $"Total omkostning: {totalCost} kr.\n" +
                                       $"Forventet afkast: {totalRevenue} kr.";
            }
            else
            {
                resultTextBlock.Text = "Indtast venligst gyldige værdier for længde og bredde.";
            }
        }
    }
}
