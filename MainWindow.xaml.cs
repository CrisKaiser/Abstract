using System.Windows;

namespace AbstractApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonProjectManagement_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagment projectManagmentPage = new ProjectManagment();
            projectManagmentPage.Show();
            this.Close(); 
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            NewProjectPage newProjectPage = new NewProjectPage();
            newProjectPage.Show();
            this.Close(); 
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            settingsPage.Show();
        }

    }
}
