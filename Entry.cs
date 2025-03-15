using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public class Entry : Grid
    {
        private int heightKontrolle = 24; 
        public TextField TextBox { get; private set; }
        public ControlBar Kontrolle { get; private set; }

        private ProjectPage pDetail;

        private Point _translationStart;
        private double _originalX;
        private double _originalY;
        private bool _isTranslating;


        public double X
        {
            get => Margin.Left;
            set => Margin = new Thickness(value, Y, 0, 0);
        }

        public double Y
        {
            get => Margin.Top;
            set => Margin = new Thickness(X, value, 0, 0);
        }

        public Entry(Point position, ProjectPage pDetail)
        {
            InitializeLayout(position);
            SetupEvents();
            this.pDetail = pDetail;
            pDetail.eintragRegister(this);
        }

        private void InitializeLayout(Point position)
        {   
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); 

            TextBox = new TextField(new Point(0, 0))
            {
                Margin = new Thickness(0) 
            };

            Kontrolle = new ControlBar(heightKontrolle)
            {
                HorizontalAlignment = HorizontalAlignment.Left, 
                Margin = new Thickness(0, 0, 0, 5) 
            };

            Grid.SetRow(TextBox, 1); 
            Grid.SetRow(Kontrolle, 0); 

            Children.Add(TextBox);
            Children.Add(Kontrolle);
        }

        private void SetupEvents()
        {
            Kontrolle.LargerClicked += (s, e) => largerClickHandler();
            Kontrolle.SmallerClicked += (s, e) => smallerClickHandler();
            Kontrolle.CheckClicked += (s, e) => checkedHandler();
            Kontrolle.DeleteClicked += (s, e) => deleteHandler();
            Kontrolle.TranslateClicked += translateHandler;
        }

        private void largerClickHandler()
        {
            TextBox.increaseFontSize();
            TextBox.fontSizeChangeReceiver();
        }

        private void deleteHandler()
        {
            Delete();
        }

        private void translateHandler(object sender, RoutedEventArgs e)
        {
            if (_isTranslating) return;

            var window = Window.GetWindow(this);
            if (window == null) return;

            _translationStart = Mouse.GetPosition(window);
            _originalX = X;
            _originalY = Y;

            Mouse.Capture(this);
            _isTranslating = true;
            MouseMove += OnMouseMoveDuringTranslate;
            MouseLeftButtonUp += OnMouseUpAfterTranslate;
            e.Handled = true;
        }

        private void OnMouseMoveDuringTranslate(object sender, MouseEventArgs e)
        {
            if (!_isTranslating) return;

            var window = Window.GetWindow(this);
            if (window == null) return;

            Point currentPosition = e.GetPosition(window);
            double deltaX = currentPosition.X - _translationStart.X;
            double deltaY = currentPosition.Y - _translationStart.Y;

            X = _originalX + deltaX;
            Y = _originalY + deltaY;
        }

        private void OnMouseUpAfterTranslate(object sender, MouseButtonEventArgs e)
        {
            if (!_isTranslating) return;
            Mouse.Capture(null);
            _isTranslating = false;
            MouseMove -= OnMouseMoveDuringTranslate;
            MouseLeftButtonUp -= OnMouseUpAfterTranslate;
            e.Handled = true;
        }

        private void smallerClickHandler()
        {
            TextBox.decreaseFontSize();
        }

        private void checkedHandler()
        {
            TextBox.RemoveFocus();
            HideKontrolleiste();
        }

        private void HideKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Collapsed;
            TextBox.IsReadOnly = true;

        }

        public void ShowKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Visible;
            TextBox.IsReadOnly = false;
        }

        public void notifyOnStateUpdate()
        {
            if (pDetail.currentLayerMode == ProjectPage.LayerMode.DeleteMode)
            {
                Kontrolle.SetCurrentControlMode(ControlBar.ControlMode.DeleteMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else if (pDetail.currentLayerMode == ProjectPage.LayerMode.TranslateMode)
            {
                Kontrolle.SetCurrentControlMode(ControlBar.ControlMode.TranslateMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else if (pDetail.currentLayerMode == ProjectPage.LayerMode.EditMode)
            {
                Kontrolle.SetCurrentControlMode(ControlBar.ControlMode.EditMode);
                Kontrolle.Visibility = Visibility.Visible;
            }
            else
            {
                Kontrolle.Visibility = Visibility.Collapsed;
            }
        }

        public void Delete()
        {
            Children.Remove(TextBox);
            Children.Remove(Kontrolle);
            if (pDetail != null)
            {
                pDetail.eintraege.Remove(this);
            }
            TextBox = null;
            Kontrolle = null;
        }

    }
}