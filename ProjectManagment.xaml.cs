using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AbstractApp
{
    public partial class ProjectManagment : Window
    {
        private ObservableCollection<Projekt> _projekte;

        public ProjectManagment()
        {
            InitializeComponent();
            LadeProjekte();
        }

        private void LadeProjekte()
        {
            _projekte = new ObservableCollection<Projekt>(ProjektManager.LadeProjekte());

            ProjektListe.ItemsSource = _projekte;
        }

        private void BtnProjektOeffnen_Click(object sender, RoutedEventArgs e)
        {
            var ausgewaehltesProjekt = ProjektListe.SelectedItem as Projekt;
            if (ausgewaehltesProjekt == null)
            {
                MessageBox.Show("Bitte wähle ein Projekt aus.");
                return;
            }

            ProjectPage projektDetails = new ProjectPage(ausgewaehltesProjekt);
            projektDetails.Show();
            this.Close();
        }

        private void BtnProjektLoeschen_Click(object sender, RoutedEventArgs e)
        {
            var ausgewaehltesProjekt = ProjektListe.SelectedItem as Projekt;
            if (ausgewaehltesProjekt == null)
            {
                MessageBox.Show("Bitte wähle ein Projekt aus.");
                return;
            }

            var bestaetigung = MessageBox.Show(
                $"Möchtest du das Projekt '{ausgewaehltesProjekt.Name}' wirklich löschen?",
                "Projekt löschen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (bestaetigung == MessageBoxResult.Yes)
            {
                _projekte.Remove(ausgewaehltesProjekt);

                ProjektManager.SpeichereProjekte(_projekte.ToList());

                MessageBox.Show($"Projekt '{ausgewaehltesProjekt.Name}' wurde gelöscht.");
            }
        }

        private void BtnZurueck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}