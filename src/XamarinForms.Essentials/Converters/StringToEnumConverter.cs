﻿using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinForms.Essentials.MarkupExtensions;


namespace XamarinForms.Essentials.Converters
{
    /// <summary>
    /// Converts an Integer Value into an Enumeration. 
    /// </summary>
    public class StringToEnumConverter : ConvertibleMarkupExtension<StringToEnumConverter>, IValueConverter
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
            if (value != null && value is string)
                return Enum.ToObject((Type)parameter, int.Parse((string)value));
            else
                return string.Empty;
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
            return ((int)value).ToString();
        }

    }

}