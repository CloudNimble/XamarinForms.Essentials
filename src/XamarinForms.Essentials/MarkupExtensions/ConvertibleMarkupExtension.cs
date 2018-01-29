using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Essentials.MarkupExtensions
{

    /// <summary>
    /// A base class used to make awesome Converters with.
    /// </summary>
    /// <typeparam name="T">The type implementing IValueConverter to make awesome.</typeparam>
    public abstract class ConvertibleMarkupExtension<T> : IMarkupExtension
        where T : class, IValueConverter, new()
    {

        #region Private Members

        private static T _converter;

        #endregion

        #region MarkupExtension Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        #endregion

    }
}
