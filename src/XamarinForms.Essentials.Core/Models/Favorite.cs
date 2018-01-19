using System;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class Favorite<TItem> : DataObjectBase<Guid>
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public TItem Thing { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Favorite() : base()
        {
            Id = new Guid();
        }

        /// <summary>
        /// 
        /// </summary>
        public Favorite(TItem thing) : this()
        {
            Thing = thing;
        }

        #endregion

    }

}