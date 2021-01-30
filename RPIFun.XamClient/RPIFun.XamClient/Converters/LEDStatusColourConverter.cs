using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RPIFun.XamClient.Converters
{
    public class LEDStatusColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool lEdStatus = (bool)value;
            if (lEdStatus)
            {
                //return "red";
                return Xamarin.Forms.Brush.Red;
            }
            else
            {
                return Xamarin.Forms.Brush.DarkRed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
