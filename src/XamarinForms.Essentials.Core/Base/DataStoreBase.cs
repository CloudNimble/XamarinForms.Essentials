using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// A simple <see cref="IDataStore{TItem, TId}"/> implementation using an internal list as a backing store.
    /// </summary>
    /// <typeparam name="TItem">The type of the item we'll be storing.</typeparam>
    /// <typeparam name="TId">The type of the <see cref="IIdentifiable{T}.Id" /> property.</typeparam>
    public class DataStoreBase<TItem, TId> : IDataStore<TItem, TId> where TItem : class, IIdentifiable<TId>, new()
    {

        #region Private Members

        /// <summary>
        /// 
        /// </summary>
        protected internal List<TItem> items;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public DataStoreBase()
        {
            items = new List<TItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void DropTable()
        {
            items = new List<TItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TItem>> GetAllAsync(string filter = "", bool forceRefresh = false)
        {
            await InitializeAsync();
            if (forceRefresh) await PullLatestAsync(filter);
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TItem> GetAsync(TId id)
        {
            await InitializeAsync();
            return items.FirstOrDefault(c => Equals(c.Id, id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> InitializeAsync(string filter = "")
        {
            return await Task.FromResult(IsInitialized = true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(TItem item)
        {
            await InitializeAsync();
            items.Add(item);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> PullLatestAsync(string filter = "")
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<bool> RemoveAsync(TItem item)
        {
            await InitializeAsync();
            items.Remove(item);
            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> SyncAsync()
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(TItem item)
        {
            await InitializeAsync();
            var existingItem = await GetAsync(item.Id);
            if (existingItem == null) return false;

            var index = items.IndexOf(existingItem);
            if (index < 0) return false;

            items[index] = item;
            return true;
        }

    }

}