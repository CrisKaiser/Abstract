using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AbstractApp
{
    public class Kontrollleiste : Border
    {
        public event RoutedEventHandler LargerClicked;
        public event RoutedEventHandler SmallerClicked;
        public event RoutedEventHandler CheckClicked;

        public int heightKontrolle;

        public Kontrollleiste(int heightKontrolle)
        {
            this.heightKontrolle = heightKontrolle;
            InitializeUI();
        }

        private void InitializeUI()
        {
            Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            CornerRadius = new CornerRadius(3);
            Padding = new Thickness(3);
            Width = 80;
            Height = this.heightKontrolle;

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // Pfeil-Buttons
            var btnLarger = CreateIconButton("M7 4L0 8 0 0 7 4", Brushes.Gray);
            var btnSmaller = CreateIconButton("M0 4L7 0 7 8 0 4", Brushes.Gray);
            var btnCheck = CreateIconButton("M0 5.5L3 9 10 0 9 0 3 7.5 1 4 0 5.5", Brushes.Green);

            btnLarger.Click += (s, e) => LargerClicked?.Invoke(this, e);
            btnSmaller.Click += (s, e) => SmallerClicked?.Invoke(this, e);
            btnCheck.Click += (s, e) => CheckClicked?.Invoke(this, e);

            Grid.SetColumn(btnSmaller, 0);
            Grid.SetColumn(btnLarger, 1);
            Grid.SetColumn(btnCheck, 2);

            grid.Children.Add(btnSmaller);
            grid.Children.Add(btnLarger);
            grid.Children.Add(btnCheck);

            Child = grid;
        }

        private Button CreateIconButton(string pathData, Brush color)
        {
            var button = new Button
            {
                Template = (ControlTemplate)Application.Current.Resources["IconButtonTemplate"],
                Content = new Path
                {
                    Data = Geometry.Parse(pathData),
                    Stroke = color,
                    StrokeThickness = 2,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
            return button;
        }

    }
}