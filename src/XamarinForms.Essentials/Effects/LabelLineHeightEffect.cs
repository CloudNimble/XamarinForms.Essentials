using System;
using Xamarin.Forms;

namespace XamarinForms.Essentials
{

    /// <summary>
    /// The Placeholder
    /// </summary>
    public class LabelLineHeightEffect : RoutingEffect
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double LineHeight { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public LabelLineHeightEffect() : base("XamarinFormsEssentials.LabelLineHeightEffect")
        {
        }

        #endregion

    }

}