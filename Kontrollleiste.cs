using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AbstractApp
{
    public class Kontrollleiste : Border
    {
        public event RoutedEventHandler LargerClicked;
        public event RoutedEventHandler SmallerClicked;
        public event RoutedEventHandler CheckClicked;
        public event RoutedEventHandler TranslateClicked;
        public event RoutedEventHandler DeleteClicked;

        public int heightKontrolle;
        

        public enum ControlMode
        {
            EditMode,
            TranslateMode,
            DeleteMode
        }

        private ControlMode CurrentMode;

        public Kontrollleiste(int heightKontrolle)
        {
            this.heightKontrolle = heightKontrolle;
            CurrentMode = ControlMode.EditMode;
            InitializeBaseUI();
            UpdateUI();
        }

        private void InitializeBaseUI()
        {
            Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            CornerRadius = new CornerRadius(3);
            Padding = new Thickness(3);
            Width = 80;
            Height = heightKontrolle;
        }

        public void setCurrentMode(ControlMode mode)
        {
            CurrentMode = mode;
            UpdateUI();
        }

        private void UpdateUI()
        {
            switch (CurrentMode)
            {
                case ControlMode.EditMode:
                    Child = CreateEditModeContent();
                    updateWidth(80);
                    break;
                case ControlMode.TranslateMode:
                    Child = CreateTranslateModeContent();
                    updateWidth(24);
                    break;
                case ControlMode.DeleteMode:
                    Child = CreateDeleteModeContent();
                    updateWidth(24);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void updateWidth(int _width)
        {
            Width = _width;
        }


        private UIElement CreateEditModeContent()
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            var btnLarger = CreateIconButton("M4 0L0 6H8L4 0Z", Brushes.Gray);
            var btnSmaller = CreateIconButton("M4 6L0 0H8L4 6Z", Brushes.Gray);
            var btnCheck = CreateIconButton("M2 5L4.5 8L9 2L8 1L4.5 6L3 4L2 5Z", Brushes.Green);

            btnLarger.Click += (s, e) => LargerClicked?.Invoke(this, e);
            btnSmaller.Click += (s, e) => SmallerClicked?.Invoke(this, e);
            btnCheck.Click += (s, e) => CheckClicked?.Invoke(this, e);

            Grid.SetColumn(btnSmaller, 0);
            Grid.SetColumn(btnLarger, 1);
            Grid.SetColumn(btnCheck, 2);

            grid.Children.Add(btnSmaller);
            grid.Children.Add(btnLarger);
            grid.Children.Add(btnCheck);

            return grid;
        }

        private UIElement CreateTranslateModeContent()
        {
            var button = CreateIconButton(
                "M4 0 L4 8 M0 4 L8 4 M4 0 L2 2 M4 0 L6 2 M4 8 L2 6 M4 8 L6 6 M0 4 L2 6 M0 4 L2 2 M8 4 L6 2 M8 4 L6 6",
                Brushes.Blue);

            button.Width = 16;
            button.Height = 16;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(0);

            button.PreviewMouseLeftButtonDown += (s, e) =>
            {
                TranslateClicked?.Invoke(this, e);
                e.Handled = true;
            };

            return button;
        }


        private UIElement CreateDeleteModeContent()
        {
            var button = CreateIconButton("M0 0 L8 8 M8 0 L0 8", Brushes.Red);
            button.Width = 16; 
            button.Height = 16; 
            button.HorizontalAlignment = HorizontalAlignment.Left; 
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(0, 0, 0, 0); 
            button.Click += (s, e) => DeleteClicked?.Invoke(this, e);
            return button;
        }


        private Button CreateIconButton(string pathData, Brush color)
        {
            return new Button
            {
                Template = (ControlTemplate)Application.Current.Resources["IconButtonTemplate"],
                Content = new Path
                {
                    Data = Geometry.Parse(pathData),
                    Stroke = color,
                    StrokeThickness = 2,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.Uniform
                }
            };
        }
    }
}