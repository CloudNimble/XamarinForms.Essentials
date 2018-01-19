using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem">The type of the item we're pulling from the service and caching.</typeparam>
    /// <typeparam name="TId">They type of the object's ID.</typeparam>
    public class ODataV4ReadOnlyDataStore<TItem, TId> : ReadOnlyRestDataStore<TItem, TId, ODataV4ResultList<TItem>> 
        where TItem : class, IIdentifiable<TId>, new()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem">The type of the item we're pulling from the service and caching.</typeparam>
    /// <typeparam name="TId">They type of the object's ID.</typeparam>
    public class ODataV4CachedReadOnlyDataStore<TItem, TId> : CachedReadOnlyRestDataStore<TItem, TId, ODataV4ResultList<TItem>>
        where TItem : class, IIdentifiable<TId>, new()
    {

    }

}