using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public partial class ProjektDetails : Window
    {
        public enum LayerMode
        {
            DefaultMode,    
            EditMode,       
            TranslateMode,  
            DeleteMode      
        };

        private LayerMode currentLayerMode = LayerMode.DefaultMode;

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
            this.PreviewMouseDown += MainWindow_PreviewMouseDown;
        }


        private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && !(element is SmartTextBox))
                {
                foreach (var child in this.PaperGrid.Children)
                {
                    if (child is SmartTextBox smartTextBox)
                    {
                        smartTextBox.RemoveFocus();
                    }
                }
            }
        }

        private void PaperGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (currentLayerMode != LayerMode.DefaultMode)
            {
                e.Handled = true;
            }
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
            SetFertigButtonVisibility(false);
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.clickPosition = Mouse.GetPosition(PaperGrid);
        }

        private void BtnFertig_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fertig geklickt!");
            currentLayerMode = LayerMode.DefaultMode;
            SetFertigButtonVisibility(false);
        }

        public void SetFertigButtonVisibility(bool isVisible)
        {
            BtnFertig.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MenuItem_EintragHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            var clickPos = this.clickPosition;
            var eintrag = new Eintrag(clickPos);
            PaperGrid.Children.Add(eintrag);
            eintrag.TextBox.Focus();
        }

        private void MenuItem_EintragBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird bearbeitet...");
            currentLayerMode = LayerMode.EditMode;
            SetFertigButtonVisibility(true);
        }

        private void MenuItem_EintragLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird gelöscht...");
            currentLayerMode = LayerMode.DeleteMode;
            SetFertigButtonVisibility(true);
        }

        private void MenuItem_EintragVerschieben_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eintrag wird verschoben...");
            currentLayerMode = LayerMode.TranslateMode;
            SetFertigButtonVisibility(true);
        }

        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

    }
}