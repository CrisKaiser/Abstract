using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AbstractApp
{
    public partial class Einstellungen : Window
    {
        public Einstellungen()
        {
            InitializeComponent();
            LadeSpeicherort();
        }

        private void LadeSpeicherort()
        {
            // Lade den gespeicherten Speicherort (falls vorhanden)
            string speicherort = Properties.Settings.Default.Speicherort;
            if (!string.IsNullOrEmpty(speicherort))
            {
                SpeicherortTextBox.Text = speicherort;
            }
        }

        private void BtnSpeicherortAuswaehlen_Click(object sender, RoutedEventArgs e)
        {
            // Öffne einen Dialog zur Auswahl des Speicherorts
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SpeicherortTextBox.Text = dialog.SelectedPath;
            }
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // Speichere den ausgewählten Speicherort
            if (!string.IsNullOrEmpty(SpeicherortTextBox.Text))
            {
                Properties.Settings.Default.Speicherort = SpeicherortTextBox.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("Speicherort wurde gespeichert.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte wähle einen Speicherort aus.");
            }
        }
    }
}