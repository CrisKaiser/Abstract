using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AbstractApp
{
    public partial class SettingsPage : Window
    {
        public SettingsPage()
        {
            InitializeComponent();
            StorageLocation();
        }

        private void StorageLocation()
        {
            string storageLocation = Properties.Settings.Default.Speicherort;
            if (!string.IsNullOrEmpty(storageLocation))
            {
                SpeicherortTextBox.Text = storageLocation;
            }
        }

        private void BtnSpeicherortAuswaehlen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SpeicherortTextBox.Text = dialog.SelectedPath;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
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