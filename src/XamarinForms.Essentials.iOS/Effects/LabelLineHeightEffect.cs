using Foundation;
using System;
using System.Diagnostics;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportEffect(typeof(XamarinForms.Essentials.iOS.LabelLineHeightEffect), "LabelLineHeightEffect")]
namespace XamarinForms.Essentials.iOS
{

    /// <summary>
    /// 
    /// </summary>
    public class LabelLineHeightEffect : PlatformEffect
    {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            try
            {
                Debug.WriteLine("LabelLineHeight hit.");
                if (Debugger.IsAttached) Debugger.Break();

                var effect = (Essentials.LabelLineHeightEffect)Element.Effects.FirstOrDefault(e => e is Essentials.LabelLineHeightEffect);
                if (effect != null)
                {
                    var label = (UILabel)Control;
                    var attributedString = (NSMutableAttributedString)label.AttributedText;

                    NSRange range;
                    var paragraphStyle = (NSMutableParagraphStyle)attributedString.GetAttribute(UIStringAttributeKey.ParagraphStyle, 0, out range);
                    paragraphStyle.LineSpacing = (nfloat)effect.LineHeight;

                    //label.AttributedText = attributed


                    Debug.WriteLine("Value set.");
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            Debug.WriteLine("LabelLineHeight hit.");
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == nameof(UILabel.Text))
            {

            }
        }

    }
}
