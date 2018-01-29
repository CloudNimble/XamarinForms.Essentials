using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinForms.Essentials.MarkupExtensions;

namespace XamarinForms.Essentials.Converters
{

    /// <summary>
    /// 
    /// </summary>
    public class IntegerIsNotZeroToBooleanConverter : ConvertibleMarkupExtension<IntegerIsNotZeroToBooleanConverter>, IValueConverter
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
            if (value == null) return false;
            if (value.GetType() != typeof(int))
            {
                //Trace.TraceWarning("The value passed to the IntegerIsNotZeroConverter is not an Integer. Returning \"False\".");
                return false;
            }

            bool flag = (int)value > 0;
            return flag;
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
