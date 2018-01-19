using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Settings;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TWebPayload"></typeparam>

    public class ReadOnlyRestDataStore<TItem, TId, TWebPayload> : DataStoreBase<TItem, TId>
        where TItem : class, IIdentifiable<TId>, new()
        where TWebPayload : class, IRestList<TItem>, new()
    {

        #region Private Members

        /// <summary>
        /// 
        /// </summary>
        protected internal HttpClient httpClient;

        /// <summary>
        /// 
        /// </summary>
        protected internal JsonSerializerSettings jsonEscapedSettings;

        /// <summary>
        /// 
        /// </summary>
        protected internal JsonSerializerSettings jsonIgnoreNullsSettings;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string AuthHeader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Identifier => "Items";

        /// <summary>
        /// 
        /// </summary>
        public virtual string ServiceUrl => string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public virtual string SettingsFieldName => $"Last{Identifier}Update";

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="ReadOnlyRestDataStore{TItem, TId, TWebPayload}"/>. /> 
        /// </summary>
        public ReadOnlyRestDataStore() : base()
        {
            httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
            jsonEscapedSettings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };
            jsonIgnoreNullsSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
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

            TimeSpan diff = DateTime.UtcNow.Subtract(CrossSettings.Current.GetValueOrDefault(SettingsFieldName, DateTime.MinValue));
            if (items.Count == 0 || diff.TotalDays > 1)
            {
                await PullLatestAsync(filter);
                await SyncAsync();
            }
            return IsInitialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> PullLatestAsync(string filter = "")
        {
            //RWM: Get the latest file from the service.
            if (!string.IsNullOrWhiteSpace(ServiceUrl) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format(ServiceUrl, Identifier, filter);
                var resultString = await httpClient.GetStringAsync(url);
                if (string.IsNullOrWhiteSpace(resultString)) return false;

                var results = JsonConvert.DeserializeObject<TWebPayload>(resultString, jsonEscapedSettings);
                if (results.Items.Count > 0)
                {
                    DropTable();
                    items.AddRange(results.Items);
                }
                CrossSettings.Current.AddOrUpdateValue(SettingsFieldName, DateTime.UtcNow);
                return true;
            }
            return false;
        }

        #endregion

    }

}
