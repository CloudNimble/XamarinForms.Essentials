using System.Collections.Generic;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAnalyticsProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        void TrackEvent(string name, IDictionary<string, string> properties);

    }

}
