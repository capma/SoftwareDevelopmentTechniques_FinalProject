using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MM.Utilities
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class CheckedOutRoomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCheckedOut = (bool)value;

            if (isCheckedOut)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
