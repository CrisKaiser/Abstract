using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AbstractApp
{
    public partial class EditableTextBox : TextBox
    {
        public EditableTextBox(Point position)
        {
            this.Width = 150;
            this.Background = Brushes.White;
            this.BorderBrush = Brushes.Black;
            this.Margin = new Thickness(position.X, position.Y, 0, 0);
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.Padding = new Thickness(5);
            this.TextWrapping = TextWrapping.Wrap;
            this.AcceptsReturn = true;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            try
            {
                this.Style = (Style)Application.Current.FindResource("RoundedTextBoxStyle");
            }
            catch (ResourceReferenceKeyNotFoundException)
            {
                // Fallback, wenn der Style nicht gefunden wird.
            }

            this.Height = this.FontSize + this.Padding.Top + this.Padding.Bottom;
            this.TextChanged += EditableTextBox_TextChanged;
        }

        private void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double lineHeight = this.FontSize + 5;
            int lineCount = this.LineCount;
            this.Height = (lineCount * lineHeight) + this.Padding.Top + this.Padding.Bottom;
        }
    }
}
