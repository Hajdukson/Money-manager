using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace UsersPanel
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;

            if (isVisible)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
