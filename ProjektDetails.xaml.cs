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
        private Point _clickPosition; 
        
        private bool isDragging = false;
        private Point startDragPoint;
        private double startX, startY;

        public ProjektDetails(Projekt projekt)
        {
            InitializeComponent();
            ProjektNameText.Text = $"Projekt: {projekt.Name}";
            ErstellungsdatumText.Text = $"Erstellt am: {projekt.Erstellungsdatum.ToShortDateString()}";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                isDragging = true;
                startDragPoint = e.GetPosition(this); // Position relativ zum Fenster
                startX = GridTransform.X;
                startY = GridTransform.Y;
                ((Grid)sender).CaptureMouse();
                e.Handled = true;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(this);
                GridTransform.X = startX + (currentPoint.X - startDragPoint.X);
                GridTransform.Y = startY + (currentPoint.Y - startDragPoint.Y);

                // Hintergrund neu zeichnen
                DraggableGrid.InvalidateVisual();
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                isDragging = false;
                ((Grid)sender).ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        private void BtnZurueck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _clickPosition = e.GetPosition(DraggableGrid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridTransform.X = -1000;
            GridTransform.Y = -1000;
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

            DraggableGrid.Children.Add(textBox);
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