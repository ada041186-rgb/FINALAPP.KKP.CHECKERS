using CHECKERS.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CHECKERS.Convertor
{
    public class LastMoveHighlightConverter : IMultiValueConverter
    {
        private static readonly SolidColorBrush FromBrush =
            new(Color.FromArgb(120, 255, 220, 50));
        private static readonly SolidColorBrush ToBrush =
            new(Color.FromArgb(120, 50, 220, 255));
        private static readonly SolidColorBrush Transparent =
            new(Colors.Transparent);

        public object Convert(object[] values, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (values.Length < 3) return Transparent;
            if (values[0] is not int row || values[1] is not int col) return Transparent;
            if (values[2] is not Move last) return Transparent;

            if (last.From.Row == row && last.From.Column == col) return FromBrush;
            if (last.To.Row == row && last.To.Column == col) return ToBrush;
            return Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
                                    object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}