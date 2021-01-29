using System;
using System.Globalization;
using System.Windows.Data;

namespace RPIFun.WinClient.Converters
{
    public class BusyStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isBusy = (bool)value;
            if (isBusy)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
