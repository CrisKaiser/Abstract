using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public class SmartTextBox : TextBox
    {
        // Statischer Konstruktor für den Style
        static SmartTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SmartTextBox),
                new FrameworkPropertyMetadata(typeof(SmartTextBox)));
        }

        public SmartTextBox(Point position)
        {
            InitializeTextBox(position);
            SetupEvents();
            IsReadOnly = false;
        }

        private void InitializeTextBox(Point position)
        {
            Width = 150;
            Background = Brushes.White;
            BorderBrush = Brushes.Black;
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Padding = new Thickness(5);
            TextWrapping = TextWrapping.Wrap;
            AcceptsReturn = true;
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            UpdateHeight();
        }

        public void updateHeight(Point position)
        {
            Margin = new Thickness(position.X, position.Y, 0, 0);
        }

        private void SetupEvents()
        {
            TextChanged += (s, e) => UpdateHeight();
            GotFocus += SmartTextBox_GotFocus;
            LostFocus += SmartTextBox_LostFocus;
        }

        private void SmartTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            BorderBrush = Brushes.Blue; 
        }

        private void SmartTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BorderBrush = Brushes.Black; 
        }

        private void UpdateHeight()
        {
            double fontSize = Math.Max(1, FontSize);
            double lineHeight = fontSize + 5;
            int lineCount = Math.Max(1, LineCount);
            double paddingTop = Math.Max(0, Padding.Top);
            double paddingBottom = Math.Max(0, Padding.Bottom);
            double newHeight = (lineCount * lineHeight) + paddingTop + paddingBottom;
            Height = Math.Max(1, newHeight);
        }

        public void RemoveFocus()
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(this), null);
            Keyboard.ClearFocus();
        }

    }
}