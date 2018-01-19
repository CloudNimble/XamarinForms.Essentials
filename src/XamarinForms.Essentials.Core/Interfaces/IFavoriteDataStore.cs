using System;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    public interface IFavoriteDataStore<TItem> : IDataStore<Favorite<TItem>, Guid>
    {
        //Add additional interface methods
        //specific to this data store.
    }

}