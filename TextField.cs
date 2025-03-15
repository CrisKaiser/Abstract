using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AbstractApp
{
    public class TextField : TextBox
    {
        private int fontSizeDefault = 12;
        private int fontSizeMin = 10;
        private int fontSizeMax = 22;
        private int textFieldWidthStd = 200;

        static TextField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TextField),
                new FrameworkPropertyMetadata(typeof(TextField)));
        }

        public TextField(Point position)
        {
            InitializeTextField(position);
            SetupEvents();
            IsReadOnly = false;
        }

        private void InitializeTextField(Point position)
        {
            Background = Brushes.White;
            BorderBrush = Brushes.Black;
            Margin = new Thickness(position.X, position.Y, 0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Padding = new Thickness(5);
            TextWrapping = TextWrapping.Wrap;
            AcceptsReturn = true;
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            FontSize = fontSizeDefault;
            UpdateTextFieldHeight();
            UpdateTextFieldWidth();
        }

        public void UpdateTextFieldPosition(Point position)
        {
            Margin = new Thickness(position.X, position.Y, 0, 0);
        }

        private void SetupEvents()
        {
            TextChanged += (s, e) => UpdateTextFieldHeight();
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

        private void UpdateTextFieldHeight()
        {
            double fontSize = Math.Max(1, FontSize);
            double lineHeight = fontSize + 5.0/fontSizeDefault * fontSize;
            int lineCount = Math.Max(1, LineCount);
            double paddingTop = Math.Max(0, Padding.Top);
            double paddingBottom = Math.Max(0, Padding.Bottom);
            double newHeight = (lineCount * lineHeight) + paddingTop + paddingBottom;
            Height = Math.Max(1, newHeight);
        }

        private void UpdateTextFieldWidth()
        {
            Width = textFieldWidthStd / fontSizeDefault * FontSize;
        }

        public void FontSizeChangeReceiver()
        {
            UpdateTextFieldHeight();
            UpdateTextFieldWidth();
        }

        public void RemoveFocus()
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(this), null);
            Keyboard.ClearFocus();
        }

        public void decreaseFontSize()
        {
            if (FontSize > fontSizeMin)
                FontSize -= 1;
                FontSizeChangeReceiver();
        }

        public void increaseFontSize()
        {
            if (FontSize < fontSizeMax)
                FontSize += 1;
                FontSizeChangeReceiver();
        }

    }
}