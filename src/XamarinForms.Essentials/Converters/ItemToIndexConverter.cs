using System;
using Xamarin.Forms;
using XamlEssentials.MarkupExtensions;

namespace XamlEssentials.Converters
{

    /// <summary>
    /// 
    /// </summary>
    public class ListBoxItemToIndexConverter : ConvertibleMarkupExtension<ListBoxItemToIndexConverter>, IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
#if WINRT
            string language
#else
            System.Globalization.CultureInfo culture
#endif
)
        {
            var item = (ListBoxItem)value;
            var lb = (ListBox)ItemsControl.ItemsControlFromItemContainer(item);
#if WINRT81
            return lb.IndexFromContainer(item);
#else
            return lb.ItemContainerGenerator.IndexFromContainer(item) + 1;
#endif
        }

        public object ConvertBack(object value, Type targetType, object parameter,
#if WINRT
            string language
#else
            System.Globalization.CultureInfo culture
#endif
)
        {
            return null;
        }
    }
}
