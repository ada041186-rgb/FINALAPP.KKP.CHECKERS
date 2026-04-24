using СHECKERS.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace СHECKERS.Conventor
{
    public class CellValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellValueEnum cellValue)
            {
                return cellValue switch
                {
                    CellValueEnum.WhiteChecker => Brushes.White,
                    CellValueEnum.BlackChecker => Brushes.Black,
                    _ => Brushes.Transparent
                };
            }
            return Brushes.Transparent;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }

    public class CellValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellValueEnum cellValue && cellValue != CellValueEnum.Empty)
                return System.Windows.Visibility.Visible;
            return System.Windows.Visibility.Collapsed;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}