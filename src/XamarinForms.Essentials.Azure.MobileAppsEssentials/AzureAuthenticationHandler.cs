using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AzureAuthenticationHandler<T> : DelegatingHandler where T: AzureMobileDataObjectBase
    {

        #region Private Members

        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private static bool isReauthenticating = false;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static MobileServiceAuthenticationProvider ProviderType =>  MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;

        #endregion

        #region Method Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Clone the request in case we need to send it again
            var clonedRequest = await CloneRequest(request);
            var response = await base.SendAsync(clonedRequest, cancellationToken);

            //If the token is expired or is invalid, then we need to either refresh the token or prompt the user to log back in
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (isReauthenticating)
                    return response;

                var client = DependencyService.Get<IDataStore<T, string>>() as AzureMobileDataStore<T>;

                string authToken = client.MobileService.CurrentUser.MobileServiceAuthenticationToken;
                await semaphore.WaitAsync();
                //In case two threads enter this method at the same time, only one should do the refresh (or re-login), the other should just resend the request with an updated header.
                if (authToken != client.MobileService.CurrentUser.MobileServiceAuthenticationToken)  // token was already renewed
                {
                    semaphore.Release();
                    return await ResendRequest(client.MobileService, request, cancellationToken);
                }

                isReauthenticating = true;
                bool gotNewToken = false;
                try
                {

                    gotNewToken = await RefreshToken(client.MobileService);


                    //Otherwise if refreshing the token failed or Facebook\Twitter is being used, prompt the user to log back in via the login screen
                    if (!gotNewToken)
                    {
                        gotNewToken = await Login(client.MobileService);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Unable to refresh token: " + e);
                }
                finally
                {
                    isReauthenticating = false;
                    semaphore.Release();
                }


                if (gotNewToken)
                {
                    if (!request.RequestUri.OriginalString.Contains("/.auth/me"))   //do not resend in this case since we're not using the return value of auth/me
                    {
                        //Resend the request since the user has successfully logged in and return the response
                        return await ResendRequest(client.MobileService, request, cancellationToken);
                    }
                }
            }

            return response;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> ResendRequest(IMobileServiceClient client, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Clone the request
            var clonedRequest = await CloneRequest(request);

            // Set the authentication header
            clonedRequest.Headers.Remove("X-ZUMO-AUTH");
            clonedRequest.Headers.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);

            // Resend the request
            return await base.SendAsync(clonedRequest, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task<bool> RefreshToken(IMobileServiceClient client)
        {
            var authentication = DependencyService.Get<IAzureAuthenticator>();
            if (authentication == null)
            {
                throw new InvalidOperationException("Make sure the ServiceLocator has an instance of IAuthenticator.");
            }

            try
            {
                return await authentication.RefreshUser(client);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Unable to refresh user: " + e);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task<bool> Login(IMobileServiceClient client)
        {
            var authentication = DependencyService.Get<IAzureAuthenticator>();
            if (authentication == null)
            {
                throw new InvalidOperationException("Make sure the ServiceLocator has an instance of IAuthenticator.");
            }

            var accountType = ProviderType;
            var parameters = MobileAppsSettings.LoginParameters;
            try
            {
                var user = await authentication.LoginAsync(client, accountType, parameters);
                if (user != null)
                    return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<HttpRequestMessage> CloneRequest(HttpRequestMessage request)
        {
            var result = new HttpRequestMessage(request.Method, request.RequestUri);
            foreach (var header in request.Headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }

            if (request.Content != null && request.Content.Headers.ContentType != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var mediaType = request.Content.Headers.ContentType.MediaType;
                result.Content = new StringContent(requestBody, Encoding.UTF8, mediaType);
                foreach (var header in request.Content.Headers)
                {
                    if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Content.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            return result;
        }

        #endregion

    }

}