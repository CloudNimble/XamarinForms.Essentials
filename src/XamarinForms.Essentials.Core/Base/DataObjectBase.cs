using Newtonsoft.Json;
using System;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// An observable object that comes from an external data source and contains an identifying property.
    /// </summary>
    /// <typeparam name="TId">The type for the Id property.</typeparam>
    public class DataObjectBase<TId> : ObservableObject, IIdentifiable<TId>
    {

        #region Properties

        /// <summary>
        /// The identifier for the object.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public TId Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public DataObjectBase()
        {
            Id = default(TId);
        }

        #endregion

    }

}