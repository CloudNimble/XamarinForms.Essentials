using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://github.com/xamarin/xamarin-forms-samples/blob/master/Behaviors/EventToCommandBehavior/EventToCommandBehavior/Converters/SelectedItemEventArgsToSelectedItemConverter.cs
    /// </remarks>
    public class ItemTappedEventArgsToItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemTappedEventArgs;
            return eventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
