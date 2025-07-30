using System;
using System.Globalization;
using System.Windows.Data;

namespace DesktopApp.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If either value or parameter is null, they can't be equal.
            if (value == null || parameter == null)
            {
                return false;
            }
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value is true, return the parameter; otherwise, do nothing.
            if (value is bool isChecked && isChecked)
            {
                return parameter;
            }
            return Binding.DoNothing;
        }
    }
}