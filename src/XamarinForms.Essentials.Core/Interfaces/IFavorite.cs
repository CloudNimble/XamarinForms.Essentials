using System;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IFavorite<TItem> : IIdentifiable<Guid> 
    {

        /// <summary>
        /// 
        /// </summary>
        TItem Thing { get; set; }

    }

}