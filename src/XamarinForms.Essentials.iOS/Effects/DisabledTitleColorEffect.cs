using System;
using System.Diagnostics;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(XamarinForms.Essentials.iOS.DisabledTitleColorEffect), "DisabledTitleColorEffect")]
namespace XamarinForms.Essentials.iOS
{

    /// <summary>
    /// 
    /// </summary>
    public class DisabledTitleColorEffect : PlatformEffect
    {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            try
            {
                var effect = (Essentials.DisabledTitleColorEffect)Element.Effects.FirstOrDefault(e => e is Essentials.DisabledTitleColorEffect);
                var btn = (UIButton)Control;

                btn.SetTitleColor(effect.DisabledColor.ToUIColor(), UIControlState.Disabled);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetached()
        {
        }

    }
}
