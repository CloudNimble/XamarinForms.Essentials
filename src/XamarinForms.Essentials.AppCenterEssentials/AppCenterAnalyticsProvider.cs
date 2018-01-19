using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinForms.Essentials.AppCenterEssentials;
using XamarinForms.Essentials.Core;

[assembly: Dependency(typeof(AppCenterAnalyticsProvider))]
[assembly: Dependency(typeof(IAnalyticsProvider))]
namespace XamarinForms.Essentials.AppCenterEssentials
{

    /// <summary>
    /// 
    /// </summary>
    public class AppCenterAnalyticsProvider : IAnalyticsProvider
    {

        /// <summary>
        /// 
        /// </summary>
        public AppCenterAnalyticsProvider()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            Analytics.TrackEvent(name, properties);
        }

    }

}