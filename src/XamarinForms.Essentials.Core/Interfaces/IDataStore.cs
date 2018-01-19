using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IDataStore<TItem, TId>
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        //string Identifier { get; }

        bool IsInitialized { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        void DropTable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<IEnumerable<TItem>> GetAllAsync(string filter = "", bool forceRefresh = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TItem> GetAsync(TId id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> InitializeAsync(string filter = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(TItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> PullLatestAsync(string filter = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(TItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TItem item);

    }

}