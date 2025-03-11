using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public partial class ProjektDetails : Window
    {
        private Point _clickPosition; 

        public ProjektDetails(Projekt projekt)
        {
            InitializeComponent();
            ProjektNameText.Text = $"Projekt: {projekt.Name}";
            ErstellungsdatumText.Text = $"Erstellt am: {projekt.Erstellungsdatum.ToShortDateString()}";
        }

        private void BtnZurueck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickPosition = e.GetPosition(this); 
        }


        private void MenuItem_EintragHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new TextBox
            {
                Width = 150, 
                Background = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(_clickPosition.X, _clickPosition.Y, 0, 0), 
                Padding = new Thickness(5),
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            };

            textBox.Height = textBox.FontSize + textBox.Padding.Top + textBox.Padding.Bottom;

            textBox.TextChanged += (s, args) =>
            {
                double lineHeight = textBox.FontSize + 5; // +5 für Zeilenabstand
                int lineCount = textBox.LineCount;
                textBox.Height = (lineCount * lineHeight) + textBox.Padding.Top + textBox.Padding.Bottom;
            };

            ((Grid)this.Content).Children.Add(textBox);
        }

        private void MenuItem_NeuerCluster_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Neuer Cluster wird erstellt...");
            }

        private void MenuItem_ClusterBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cluster wird bearbeitet...");
        }

        private void MenuItem_ClusterLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cluster wird gelöscht...");
        }

        private void MenuItem_EintragBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird bearbeitet...");
        }

        private void MenuItem_EintragLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird gelöscht...");
        }
    }
}