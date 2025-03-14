using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbstractApp
{
    public class Eintrag : Grid
    {
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
            // Positionierung des gesamten Eintrags
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            // Grid-Layout mit zwei Zeilen und einer Spalte
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // Für die Kontrollleiste
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // Für die TextBox

            // SmartTextBox erstellen
            TextBox = new SmartTextBox(new Point(0, 0))
            {
                Margin = new Thickness(0) // Margin der Textbox zurücksetzen
            };

            // Kontrollleiste erstellen
            Kontrolle = new Kontrollleiste()
            {
                HorizontalAlignment = HorizontalAlignment.Right, // Rechtsbündig ausrichten
                Margin = new Thickness(0, 0, 0, 5) // Optional: Abstand nach unten
            };

            // Elemente ins Grid einfügen
            Grid.SetRow(TextBox, 1); // TextBox in der zweiten Zeile
            Grid.SetRow(Kontrolle, 0); // Kontrollleiste in der ersten Zeile

            Children.Add(TextBox);
            Children.Add(Kontrolle);
        }

        private void SetupEvents()
        {
            Kontrolle.LargerClicked += (s, e) => TextBox.FontSize += 1;
            Kontrolle.SmallerClicked += (s, e) => TextBox.FontSize = Math.Max(1, TextBox.FontSize - 1);
            Kontrolle.CheckClicked += (s, e) => TextBox.RemoveFocus();
        }
    }
}