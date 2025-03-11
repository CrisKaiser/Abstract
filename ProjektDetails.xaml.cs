using System.Linq;
using System.Windows;

namespace AbstractApp
{
    public partial class ProjektDetails : Window
    {
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
    }
}