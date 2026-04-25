using System;
using System.Globalization;
using System.Windows.Data;

namespace CHECKERS.Convertor
{
    public class CellColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                int row = index / 8;
                int col = index % 8;
                return (row + col) % 2 != 0;
            }
            return false;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}