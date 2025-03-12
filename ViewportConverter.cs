using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace AbstractApp // Namespace muss exakt mit XAML übereinstimmen
{
    public class ViewportConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values[0] is TranslateTransform transform)
                {
                    return new Rect(
                        -transform.X % 20,
                        -transform.Y % 20,
                        20,
                        20);
                }
                return new Rect(0, 0, 20, 20);
            }
            catch
            {
                return new Rect(0, 0, 20, 20);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}