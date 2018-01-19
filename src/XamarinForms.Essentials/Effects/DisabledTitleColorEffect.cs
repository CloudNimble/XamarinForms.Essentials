using System;
using Xamarin.Forms;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// The Placeholder
    /// </summary>
    public class DisabledTitleColorEffect : RoutingEffect
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Color DisabledColor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public DisabledTitleColorEffect() : base("XamarinFormsEssentials.DisabledTitleColorEffect")
        {
        }

        #endregion

    }

}