using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;

namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class MobileAppsSettings
    {

        #region Private Members

        private static readonly string AuthTokenDefault = string.Empty;
        private static readonly string LocalDatabaseNameDefault = "localcache.db";
        private static readonly string ServiceUrlDefault = "https://CONFIGURE-THIS-URL.azurewebsites.net";
        private static readonly string UserIdDefault = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault(nameof(AuthToken), AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue(nameof(AuthToken), value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);

        /// <summary>
        /// 
        /// </summary>
        public static string LocalDatabaseName
        {
            get { return AppSettings.GetValueOrDefault(nameof(LocalDatabaseName), LocalDatabaseNameDefault); }
            set { AppSettings.AddOrUpdateValue(nameof(LocalDatabaseName), value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<string, string> LoginParameters
        {
            get
            {
                var result = AppSettings.GetValueOrDefault(nameof(LoginParameters), null);
                return result != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(result) : new Dictionary<string, string>();
            }
            set
            {
                var result = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(nameof(LoginParameters), result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ServiceUrl
        {
            get { return AppSettings.GetValueOrDefault(nameof(ServiceUrl), ServiceUrlDefault); }
            set { AppSettings.AddOrUpdateValue(nameof(ServiceUrl), value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(nameof(UserId), UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(nameof(UserId), value); }
        }

        #endregion

    }

}