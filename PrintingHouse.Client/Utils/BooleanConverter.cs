namespace PrintingHouse.Client
{
    using System;
    using System.Windows.Data;

    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var test = (bool?)value;
            var result = bool.Parse((string)parameter);

            if (test == result)
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = bool.Parse((string)parameter);
            return result;
        }
    }
}
