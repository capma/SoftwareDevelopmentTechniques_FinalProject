using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MM.Utilities
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class NumberChildrenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numOfChildrenToCheck = int.Parse(value.ToString());

            if (numOfChildrenToCheck < 0 || numOfChildrenToCheck > 10)
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
