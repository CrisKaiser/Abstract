using System;
using System.Linq;
using System.Windows;

namespace AbstractApp
{
    public partial class NewProjectPage : Window
    {
        public NewProjectPage()
        {
            InitializeComponent();
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string projektName = ProjektNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(projektName))
            {
                MessageBox.Show("Bitte gib einen Projektnamen ein.");
                return;
            }

            // Überprüfe, ob ein Speicherort festgelegt ist
            string speicherort = Properties.Settings.Default.Speicherort;
            if (string.IsNullOrEmpty(speicherort))
            {
                MessageBox.Show("Bitte lege zuerst einen Speicherort in den Einstellungen fest.");
                SettingsPage einstellungen = new SettingsPage();
                einstellungen.Show();
                return;
            }

            // Überprüfe, ob der Projektname bereits existiert
            var projekte = ProjektManager.LadeProjekte();
            if (projekte.Any(p => p.Name.Equals(projektName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ein Projekt mit diesem Namen existiert bereits.");
                return;
            }

            // Füge das neue Projekt hinzu
            ProjektManager.NeuesProjektHinzufuegen(projektName);
            MessageBox.Show($"Projekt '{projektName}' wurde gespeichert.");

            ProjektNameTextBox.Text = ""; // Eingabe leeren
        }

        private void BtnZurueck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
