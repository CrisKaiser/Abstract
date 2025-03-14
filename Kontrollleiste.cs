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