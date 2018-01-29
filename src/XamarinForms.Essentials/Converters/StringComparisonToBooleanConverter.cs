using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinForms.Essentials.MarkupExtensions;

namespace XamarinForms.Essentials.Converters
{

    /// <summary>
    /// 
    /// </summary>
    public class StringComparisonToBooleanConverter : ConvertibleMarkupExtension<StringComparisonToBooleanConverter>, IValueConverter
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
            if (value.GetType() != typeof(string))
                return false;

            return (value as string) == (parameter as string);
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
