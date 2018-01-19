using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAzureAuthenticator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="provider"></param>
        /// <param name="paramameters"></param>
        /// <returns></returns>
        Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> paramameters = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<bool> RefreshUser(IMobileServiceClient client);

        /// <summary>
        /// 
        /// </summary>
        void ClearCookies();

    }

}