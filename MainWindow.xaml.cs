using System.Windows;

namespace AbstractApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnProjektverwaltung_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagment projektverwaltungFenster = new ProjectManagment();
            projektverwaltungFenster.Show();
            this.Close(); // Hauptmenü schließen
        }

        private void BtnNeuesProjekt_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage neuesProjektFenster = new NewProjectPage();
            neuesProjektFenster.Show();
            this.Close(); // Hauptmenü schließen
        }

        private void BtnEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage einstellungen = new SettingsPage();
            einstellungen.Show();
        }

    }
}
