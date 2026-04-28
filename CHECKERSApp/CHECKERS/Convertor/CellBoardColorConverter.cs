using CHECKERS.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CHECKERS.Convertor
{
    public class CellBoardColorConverter : IMultiValueConverter
    {
        private static readonly SolidColorBrush Dark = new(Color.FromRgb(101, 56, 27));
        private static readonly SolidColorBrush Light = new(Color.FromRgb(240, 217, 181));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is int row && values[1] is int col)
                return (row + col) % 2 != 0 ? Dark : Light;
            return Dark;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}