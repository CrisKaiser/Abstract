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
        public TextField textField { get; private set; }
        public ControlBar controlBar { get; private set; }

        private ProjectPage projectPage;

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

        public Entry(Point position, ProjectPage projectPage)
        {
            InitializeEntryLayout(position);
            SetupEvents();
            this.projectPage = projectPage;
            projectPage.eintragRegister(this);
        }

        private void InitializeEntryLayout(Point position)
        {   
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); 

            textField = new TextField(new Point(0, 0))
            {
                Margin = new Thickness(0) 
            };

            controlBar = new ControlBar(heightKontrolle)
            {
                HorizontalAlignment = HorizontalAlignment.Left, 
                Margin = new Thickness(0, 0, 0, 5) 
            };

            Grid.SetRow(textField, 1); 
            Grid.SetRow(controlBar, 0); 

            Children.Add(textField);
            Children.Add(controlBar);
        }

        private void SetupEvents()
        {
            controlBar.LargerClicked += LargerButtonClickHandler;
            controlBar.SmallerClicked += SmallerButtonClickHandler;
            controlBar.CheckClicked += CheckButtonClickHandler;
            controlBar.DeleteClicked += DeleteButtonClickHandler;
            controlBar.TranslateClicked += TranslateButtonClickHandler;
        }

        private void LargerButtonClickHandler(object sender, RoutedEventArgs e)
        {
            textField.increaseFontSize();
            textField.FontSizeChangeReceiver();
        }

        private void DeleteButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void SmallerButtonClickHandler(object sender, RoutedEventArgs e)
        {
            textField.decreaseFontSize();
        }

        private void CheckButtonClickHandler(object sender, RoutedEventArgs e)
        {
            textField.RemoveFocus();
            HideControlBar();
        }

        private void TranslateButtonClickHandler(object sender, RoutedEventArgs e)
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

            Console.WriteLine(currentPosition.X);

            //hier gehts dann weiter 
            X = GlobalMethods.Clamp(_originalX + deltaX, 0, ProjectSettings.GridWidth - 195);
            Y = GlobalMethods.Clamp(_originalY + deltaY, 0, ProjectSettings.GridWidth - 40);
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

        private void HideControlBar()
        {
            controlBar.Visibility = Visibility.Collapsed;
            textField.IsReadOnly = true;

        }

        public void ShowControlBar()
        {
            controlBar.Visibility = Visibility.Visible;
        }

        public void NotifyOnStateUpdate()
        {
            if (projectPage.currentLayerMode == ProjectPage.LayerMode.DeleteMode)
            {
                controlBar.SetCurrentControlMode(ControlBar.ControlMode.DeleteMode);
                ShowControlBar();
            }
            else if (projectPage.currentLayerMode == ProjectPage.LayerMode.TranslateMode)
            {
                controlBar.SetCurrentControlMode(ControlBar.ControlMode.TranslateMode);
                ShowControlBar();
            }
            else if (projectPage.currentLayerMode == ProjectPage.LayerMode.EditMode)
            {
                controlBar.SetCurrentControlMode(ControlBar.ControlMode.EditMode);
                ShowControlBar();
                textField.IsReadOnly = false;
            }
            else
            {
                HideControlBar();
            }
        }

        public void Delete()
        {
            Children.Remove(textField);
            Children.Remove(controlBar);
            if (projectPage != null)
            {
                projectPage.eintraege.Remove(this);
            }
            textField = null;
            controlBar = null;
        }

    }
}