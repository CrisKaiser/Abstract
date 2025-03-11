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
            Projektverwaltung projektverwaltungFenster = new Projektverwaltung();
            projektverwaltungFenster.Show();
            this.Close(); // Hauptmenü schließen
        }

        private void BtnNeuesProjekt_Click(object sender, RoutedEventArgs e)
        {
            NeuesProjekt neuesProjektFenster = new NeuesProjekt();
            neuesProjektFenster.Show();
            this.Close(); // Hauptmenü schließen
        }

        private void BtnEinstellungen_Click(object sender, RoutedEventArgs e)
        {
            Einstellungen einstellungen = new Einstellungen();
            einstellungen.Show();
        }

    }
}
