using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace MVVM.Life.Converters
{
    public class DecimalToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var aliveBrush = App.Current.Resources["AliveBrush"] as SolidColorBrush;
            var deadBrush = App.Current.Resources["DeadBrush"] as SolidColorBrush;

            if (value is decimal)
            {
                if ((decimal)value > 0)
                    return aliveBrush;
                return deadBrush;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
