using System.Globalization;
using System.Windows.Data;
using System;

namespace AbstractApp // Namespace muss exakt mit XAML übereinstimmen
{
    public class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double size)
            {
                // Füge 2000 Pixel Padding hinzu für Scrollbereich
                return size + 2000;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}