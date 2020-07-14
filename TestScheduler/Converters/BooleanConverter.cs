using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TestScheduler.Converters
{
    public class BooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}
