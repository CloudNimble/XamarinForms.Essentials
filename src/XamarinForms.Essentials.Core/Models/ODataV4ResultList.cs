using Newtonsoft.Json;
using System.Collections.Generic;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// A container for deserializing an OData v4 result and its associated metadata.
    /// </summary>
    /// <typeparam name="T">The type of Items in the OData payload.</typeparam>
    public class ODataV4ResultList<T> : IRestList<T>
    {

        /// <summary>
        /// Maps to the "@odata.context" property. 
        /// </summary>
        [JsonProperty("@odata.context")]
        public string MetadataReferenceLink { get; set; }

        /// <summary>
        /// Maps to the "odata.count" property.
        /// </summary>
        /// <remarks>
        /// A mismatch between <see cref="ExpectedItemCount"/> and <see cref="Items.Count"/> can indicate an issue with deserialization.
        /// </remarks>
        [JsonProperty("@odata.count")]
        public string ExpectedItemCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("value")]
        public List<T> Items { get; set; }

    }

}