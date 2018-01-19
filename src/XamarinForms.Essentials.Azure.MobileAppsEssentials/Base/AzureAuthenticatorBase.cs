using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AzureAuthenticatorBase : IAzureAuthenticator
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual void ClearCookies()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="provider"></param>
        /// <param name="paramameters"></param>
        /// <returns></returns>
        public abstract Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> paramameters = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual async Task<bool> RefreshUser(IMobileServiceClient client)
        {
            try
            {
                var user = await client.RefreshUserAsync();

                if (user != null)
                {
                    client.CurrentUser = user;
                    MobileAppsSettings.AuthToken = user.MobileServiceAuthenticationToken;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to refresh user: " + ex);
            }

            return false;
        }
    }
}
