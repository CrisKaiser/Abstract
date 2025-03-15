using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public partial class ProjectPage : Window
    {
        public enum LayerMode
        {
            DefaultMode,
            EditMode,
            TranslateMode,
            DeleteMode
        };

        private bool isEditing = false;

        public LayerMode currentLayerMode = LayerMode.DefaultMode;

        private bool isDragging = false;
        private Point dragStart;
        private double startX, startY;

        private Point clickPosition;

        private double zoomFactor = 1.0;
        private readonly double zoomStep = 0.01;
        private readonly double minZoom = 0.2;
        private readonly double maxZoom = 5.0;


        public List<Entry> eintraege = new List<Entry>();

        public ProjectPage(Projekt projekt)
        {
            InitializeComponent();
            ProjektNameText.Text = $"Projekt: {projekt.Name}";
            ErstellungsdatumText.Text = $"Erstellt am: {projekt.Erstellungsdatum.ToShortDateString()}";

            this.MouseWheel += Window_MouseWheel;
            this.PreviewMouseDown += MainWindow_PreviewMouseDown;
        }


        private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && !(element is TextField))
                {
                foreach (var eintrag in eintraege)
                {
                    eintrag.TextBox.RemoveFocus();
                }
            }
        }

        private void checkIsEditing()
        {
            isEditing = false;
            foreach (var eintrag in eintraege)
            {
                if (eintrag.TextBox.IsReadOnly == false)
                {
                    isEditing = true;
                }
            }
        }

        private void PaperGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            checkIsEditing();
            if (currentLayerMode != LayerMode.DefaultMode || isEditing == true)
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
            PaperGrid.Width = ProjectSettings.GridWidth;
            PaperGrid.Height = ProjectSettings.GridHeight;

            PaperTransform.X = (ActualWidth - PaperGrid.Width) / 2;
            PaperTransform.Y = (ActualHeight - PaperGrid.Height) / 2;

            SetFertigButtonVisibility(false);
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.clickPosition = Mouse.GetPosition(PaperGrid);
        }

        private void BtnFertig_Click(object sender, RoutedEventArgs e)
        {
            setLayerMode(LayerMode.DefaultMode);
            SetFertigButtonVisibility(false);
        }

        public void SetFertigButtonVisibility(bool isVisible)
        {
            BtnFertig.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MenuItem_EintragHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            var clickPos = this.clickPosition;
            var eintrag = new Entry(clickPos, this);
            PaperGrid.Children.Add(eintrag);
            eintrag.TextBox.Focus();
        }

        private void MenuItem_EintragBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            setLayerMode(LayerMode.EditMode);
            SetFertigButtonVisibility(true);
        }

        private void MenuItem_EintragLoeschen_Click(object sender, RoutedEventArgs e)
        {
            setLayerMode(LayerMode.DeleteMode);
            SetFertigButtonVisibility(true);
        }

        private void MenuItem_EintragVerschieben_Click(object sender, RoutedEventArgs e)
        {
            setLayerMode(LayerMode.TranslateMode);
            SetFertigButtonVisibility(true);
        }

        private void setLayerMode(LayerMode layerMode) {
            currentLayerMode = layerMode;

            foreach (var e in eintraege)
            {
                e.notifyOnStateUpdate();
            }
        }

        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
        public void eintragRegister(Entry e)
        {
            if (!eintraege.Contains(e)){
                eintraege.Add(e);
            }
        }

    }
}