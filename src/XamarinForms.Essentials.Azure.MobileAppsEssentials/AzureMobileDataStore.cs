using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Essentials.Azure.MobileAppsEssentials;
using XamarinForms.Essentials.Core;

[assembly: Dependency(typeof(AzureMobileDataStore<AzureMobileDataObjectBase>))]
[assembly: Dependency(typeof(IDataStore<AzureMobileDataObjectBase, string>))]
namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AzureMobileDataStore<T> : IDataStore<T, string> where T : AzureMobileDataObjectBase
    {

        #region Private Members

        IMobileServiceSyncTable<T> itemsTable;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public MobileServiceAuthenticationProvider AuthProvider => MobileServiceAuthenticationProvider.Twitter;

        /// <summary>
        /// 
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MobileServiceClient MobileService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseAuthentication => false;
        
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public void DropTable()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(string filter = "", bool forceRefresh = false)
        {
            await InitializeAsync(filter);
            if (forceRefresh)
            {
                await PullLatestAsync(filter);
            }

            return await itemsTable.ToEnumerableAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            await InitializeAsync();
            await PullLatestAsync();
            var items = await itemsTable.Where(s => s.Id == id).ToListAsync();

            if (items == null || items.Count == 0)
                return default(T);

            return items[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T item)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await itemsTable.InsertAsync(item);
            await SyncAsync();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T item)
        {
            await InitializeAsync();
            await itemsTable.UpdateAsync(item);
            await SyncAsync();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(T item)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await itemsTable.DeleteAsync(item);
            await SyncAsync();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitializeAsync(string filter = "")
        {
            if (IsInitialized) return true;

            AzureAuthenticationHandler<T> handler = null;

            if (UseAuthentication)
                handler = new AzureAuthenticationHandler<T>();

            MobileService = new MobileServiceClient(MobileAppsSettings.ServiceUrl, handler)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings
                {
                    CamelCasePropertyNames = true
                }
            };

            if (UseAuthentication && !string.IsNullOrWhiteSpace(MobileAppsSettings.AuthToken) && !string.IsNullOrWhiteSpace(MobileAppsSettings.UserId))
            {
                MobileService.CurrentUser = new MobileServiceUser(MobileAppsSettings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = MobileAppsSettings.AuthToken;
            }

            var store = new MobileServiceSQLiteStore(MobileAppsSettings.LocalDatabaseName);
            store.DefineTable<T>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            itemsTable = MobileService.GetSyncTable<T>();

            return IsInitialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PullLatestAsync(string filter = "")
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to pull items, we are offline");
                return false;
            }
            try
            {
                await itemsTable.PullAsync($"all{typeof(T).Name}", itemsTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull items, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to sync items, we are offline");
                return false;
            }
            try
            {
                await MobileService.SyncContext.PushAsync();
                if (!(await PullLatestAsync().ConfigureAwait(false)))
                    return false;
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult == null)
                {
                    Debug.WriteLine("Unable to sync, that is alright as we have offline capabilities: " + exc);
                    return false;
                }
                foreach (var error in exc.PushResult.Errors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
                return false;
            }

            return true;
        }

        #endregion

    }

}