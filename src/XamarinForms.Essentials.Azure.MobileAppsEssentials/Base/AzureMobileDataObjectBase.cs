using Microsoft.WindowsAzure.MobileServices;
using System;
using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials.Azure.MobileAppsEssentials
{

    /// <summary>
    /// The base object for storing data in Azure Mobile Apps. Has all of the fields required for syncing 
    /// already in place.
    /// </summary>
    public class AzureMobileDataObjectBase : DataObjectBase<string>
    {

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        [Version]
        public string AzureVersion { get; set; }

        /// <summary>
        /// The timestamp the object was created.
        /// </summary>
        [CreatedAt]
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The timestamp the object was last updated.
        /// </summary>
        [UpdatedAt]
        public DateTimeOffset DateUpdated { get; set; }

    }

}