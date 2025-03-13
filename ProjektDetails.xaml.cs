using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.AxHost;

namespace AbstractApp
{
    public partial class ProjektDetails : Window
    {
        
        private bool isDragging = false;
        private Point dragStart;
        private double startX, startY;

        public ProjektDetails(Projekt projekt)
        {
            InitializeComponent();
            ProjektNameText.Text = $"Projekt: {projekt.Name}";
            ErstellungsdatumText.Text = $"Erstellt am: {projekt.Erstellungsdatum.ToShortDateString()}";
        }

        private void PaperGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                isDragging = true;
                dragStart = e.GetPosition(this);
                PaperGrid.CaptureMouse();
            }
        }

        private void PaperGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPos = e.GetPosition(this);
                PaperTransform.X += currentPos.X - dragStart.X;
                PaperTransform.Y += currentPos.Y - dragStart.Y;
                dragStart = currentPos;
            }
        }

        private void PaperGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                isDragging = false;
                PaperGrid.ReleaseMouseCapture();
            }
        }

        private void BtnZurueck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PaperTransform.X = (ActualWidth - PaperGrid.ActualWidth) / 2;
            PaperTransform.Y = (ActualHeight - PaperGrid.ActualHeight) / 2;
        }

        private void MenuItem_EintragHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            var clickPos = Mouse.GetPosition(PaperGrid);

            TextBox textBox = new TextBox
            {
                Width = 150,
                Background = Brushes.White,
                BorderBrush = Brushes.Black,
                Margin = new Thickness(clickPos.X, clickPos.Y, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
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

            PaperGrid.Children.Add(textBox);
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