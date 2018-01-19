using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TRestList"></typeparam>
    public class CachedReadOnlyRestDataStore<TItem, TId, TRestList> : ReadOnlyRestDataStore<TItem, TId, TRestList>
        where TItem : class, IIdentifiable<TId>, new()
        where TRestList : class, IRestList<TItem>, new()
    {

        #region Private Members

        private IAnalyticsProvider analyticsProvider;
        private IFolder localCacheFolder;
        private IFile localCacheFile;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual string CacheFileName => $"{Identifier}-Cache.txt";

        /// <summary>
        /// 
        /// </summary>
        public virtual Func<Task<string>> AlternateCacheHelper => async () => await Task.FromResult("");

        /// <summary>
        /// 
        /// </summary>
        public virtual string CacheFolderName => "Cache";

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CachedReadOnlyRestDataStore() : base()
        {
            analyticsProvider = DependencyService.Get<IAnalyticsProvider>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> InitializeAsync(string filter = "")
        {
            if (IsInitialized) return true;

            //RWM: This is the process:
            //     1) Check for the latest updated version in the local cache.
            //     2) If that doesn't exist, pull the version out of the assembly.
            //     3) If the version in the assembly is not found, OR the version is > X old, get from the service.
            if (!string.IsNullOrWhiteSpace(AuthHeader))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", AuthHeader);
            }
            string contents = string.Empty;
            ExistenceCheckResult fileExists = ExistenceCheckResult.NotFound;

            try
            {
                localCacheFolder = await FileSystem.Current.LocalStorage.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
                fileExists = await localCacheFolder.CheckExistsAsync(CacheFileName);
            }
            catch (Exception ex)
            {
                if (analyticsProvider != null)
                {
                    analyticsProvider.TrackEvent("CacheFileCheckException", new Dictionary<string, string>
                    {
                        ["Identifier"] = Identifier,
                        ["CacheFileName"] = CacheFileName,
                        ["Exception"] = ex.ToString()
                    });
                }
            }

            //RWM: if we don't have a favorites file, that's fine. We'll make one when we save.
            if (Identifier == "FavoriteItem" && fileExists == ExistenceCheckResult.NotFound) return true;

            try
            {
                if (fileExists == ExistenceCheckResult.NotFound && AlternateCacheHelper != null)
                {
                    contents = await AlternateCacheHelper();
                }
                else
                {
                    localCacheFile = await localCacheFolder.GetFileAsync(CacheFileName);
                    contents = await localCacheFile.ReadAllTextAsync();
                }

                if (!string.IsNullOrWhiteSpace(contents))
                {
                    var results = JsonConvert.DeserializeObject<List<TItem>>(contents, jsonEscapedSettings);
                    if (results.Count > 0)
                    {
                        items.AddRange(results);
                    }
                }
                //RWM: Don't worry about an "else" here. If we're still at 0 items after this we'll pull from the web.
            }
            catch (Exception ex)
            {
                if (analyticsProvider != null)
                {
                    analyticsProvider.TrackEvent("LocalCacheException", new Dictionary<string, string>
                    {
                        ["Identifier"] = Identifier,
                        ["Exception"] = ex.ToString()
                    });
                }
                return IsInitialized = false;
            }

            return await base.InitializeAsync(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>When saving, this method ignores null values to reduce file size.</remarks>
        public override async Task<bool> SyncAsync()
        {
            //RWM: Serialize the table to a file.
            if (localCacheFolder == null)
            {
                //RWM: Shouldn't happen but better safe than sorry.
                localCacheFolder = await FileSystem.Current.LocalStorage.CreateFolderAsync(CacheFolderName, CreationCollisionOption.OpenIfExists);
            }
            if (localCacheFile == null)
            {
                //RWM: Shouldn't happen but better safe than sorry.
                localCacheFile = await localCacheFolder.CreateFileAsync(CacheFileName, CreationCollisionOption.ReplaceExisting);
            }
            var contents = JsonConvert.SerializeObject(items, Formatting.None, jsonIgnoreNullsSettings);
            await localCacheFile.WriteAllTextAsync(contents);
            return true;
        }

        #endregion

    }

}