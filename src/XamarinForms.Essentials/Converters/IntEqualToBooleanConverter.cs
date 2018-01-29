using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinForms.Essentials.MarkupExtensions;

namespace XamarinForms.Essentials.Converters
{

    /// <summary>
    /// Converts an Integer Value into a Visibility Enumeration based on the passed-in parameter. 
    /// </summary>
    public class IntEqualToBooleanConverter : ConvertibleMarkupExtension<IntEqualToBooleanConverter>, IValueConverter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;

            int valueToCompare = (int)value;
            int referenceValue = int.Parse((string)parameter);

            return valueToCompare == referenceValue ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}