using CHECKERS.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CHECKERS.Convertor
{
    public class CellValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                CellValueEnum.WhiteChecker => Brushes.WhiteSmoke,
                CellValueEnum.BlackChecker => Brushes.Gray,
                CellValueEnum.WhiteKing => Brushes.Gold,
                CellValueEnum.BlackKing => Brushes.DarkRed,
                _ => Brushes.Transparent
            };
        }
        public object? ConvertBack(object v, Type t, object p, CultureInfo c) => null;
    }

}