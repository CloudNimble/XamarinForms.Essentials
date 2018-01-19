using System.Collections.Generic;

namespace XamarinForms.Essentials.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRestList<T>
    {

        /// <summary>
        /// 
        /// </summary>
        List<T> Items { get; set; }

    }

}