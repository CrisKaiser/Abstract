using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbstractApp
{
    public class Eintrag : Grid
    {
        private int heightKontrolle = 24; 
        public SmartTextBox TextBox { get; private set; }
        public Kontrollleiste Kontrolle { get; private set; }

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

        public Eintrag(Point position)
        {
            InitializeLayout(position);
            SetupEvents();
        }

        private void InitializeLayout(Point position)
        {   
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); 

            TextBox = new SmartTextBox(new Point(0, 0))
            {
                Margin = new Thickness(0) 
            };

            Kontrolle = new Kontrollleiste(heightKontrolle)
            {
                HorizontalAlignment = HorizontalAlignment.Right, 
                Margin = new Thickness(0, 0, 0, 5) 
            };

            Grid.SetRow(TextBox, 1); 
            Grid.SetRow(Kontrolle, 0); 

            Children.Add(TextBox);
            Children.Add(Kontrolle);
        }

        private void SetupEvents()
        {
            Kontrolle.LargerClicked += (s, e) => TextBox.FontSize += 1;
            Kontrolle.SmallerClicked += (s, e) => TextBox.FontSize = Math.Max(1, TextBox.FontSize - 1);
            Kontrolle.CheckClicked += (s, e) => checkedHandler();
        }

        private void checkedHandler()
        {
            TextBox.RemoveFocus();
            HideKontrolleiste();
        }

        private void HideKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Collapsed;
            
        }

        public void ShowKontrolleiste()
        {
            Kontrolle.Visibility = Visibility.Visible;
        }

    }
}