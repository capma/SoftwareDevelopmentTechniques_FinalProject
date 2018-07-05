using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MM.Utilities
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numOfPeopleToCheck = int.Parse(value.ToString());

            if (numOfPeopleToCheck < 1 || numOfPeopleToCheck > 10)
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
