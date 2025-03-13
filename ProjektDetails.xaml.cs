using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public partial class ProjektDetails : Window
    {
        
        private bool isDragging = false;
        private Point dragStart;
        private double startX, startY;

        private Point clickPosition;

        private double zoomFactor = 1.0;
        private readonly double zoomStep = 0.01;
        private readonly double minZoom = 0.2;  
        private readonly double maxZoom = 5.0;

        public ProjektDetails(Projekt projekt)
        {
            InitializeComponent();
            ProjektNameText.Text = $"Projekt: {projekt.Name}";
            ErstellungsdatumText.Text = $"Erstellt am: {projekt.Erstellungsdatum.ToShortDateString()}";

            this.MouseWheel += Window_MouseWheel;
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

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point mousePosBeforeZoom = e.GetPosition((UIElement)PaperGrid.Parent);

            double oldZoom = zoomFactor;
            zoomFactor += e.Delta > 0 ? zoomStep : -zoomStep;
            zoomFactor = Clamp(zoomFactor, minZoom, maxZoom);

            double scaleRatio = zoomFactor / oldZoom;

            PaperTransform.X = (PaperTransform.X - mousePosBeforeZoom.X) * scaleRatio + mousePosBeforeZoom.X;
            PaperTransform.Y = (PaperTransform.Y - mousePosBeforeZoom.Y) * scaleRatio + mousePosBeforeZoom.Y;

            PaperScale.ScaleX = zoomFactor;
            PaperScale.ScaleY = zoomFactor;
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

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.clickPosition = Mouse.GetPosition(PaperGrid);
        }

        private void MenuItem_EintragHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            var clickPos = this.clickPosition;
            var textBox = new SmartTextBox(clickPos);
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

        private void MenuItem_EintragVerschieben_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird verschoben...");
        }

        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

    }
}