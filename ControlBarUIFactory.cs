using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace AbstractApp
{
    public static class ControlBarUIFactory
    {

        public static Button CreateButtonLarger()
        {
            return CreateIconButton("M4 0L0 6H8L4 0Z", Brushes.Gray);
        }

        public static Button CreateButtonSmaller()
        {
            return CreateIconButton("M4 6L0 0H8L4 6Z", Brushes.Gray);
        }

        public static Button CreateButtonCheck()
        {
            return CreateIconButton("M2 5L4.5 8L9 2L8 1L4.5 6L3 4L2 5Z", Brushes.Green);
        }

        public static Button CreateButtonDelete()
        {
            Button button = CreateIconButton("M0 0 L8 8 M8 0 L0 8", Brushes.Red);
            button.Width = 16;
            button.Height = 16;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(0, 0, 0, 0);
            return button;
        }

        public static Button CreateButtonTranslate()
        {
            Button button = CreateIconButton(
                "M4 0 L4 8 M0 4 L8 4 M4 0 L2 2 M4 0 L6 2 M4 8 L2 6 M4 8 L6 6 M0 4 L2 6 M0 4 L2 2 M8 4 L6 2 M8 4 L6 6",
                Brushes.Blue);

            button.Width = 16;
            button.Height = 16;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Margin = new Thickness(0);
            return button;
        }

        public static Button CreateIconButton(string pathData, Brush color, double width = 16, double height = 16)
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
                },
                Width = width,
                Height = height
            };
        }

        public static Grid CreateEditModeGrid()
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            return grid;
        }
    }
}
