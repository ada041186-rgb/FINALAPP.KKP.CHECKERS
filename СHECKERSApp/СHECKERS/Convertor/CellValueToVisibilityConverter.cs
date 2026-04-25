using CHECKERS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CHECKERS.Convertor
{
    public class CellValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is CellValueEnum e && e != CellValueEnum.Empty
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }
        public object? ConvertBack(object v, Type t, object p, CultureInfo c) => null;
    }
}
