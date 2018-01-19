
using Foundation;
using System.Reflection;
using UIKit;

namespace XamarinForms.Essentials.SampleApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new FormsApp());

            var appCenter = typeof(Microsoft.AppCenter.AppCenter);
            Assembly.Load(appCenter.FullName);

			return base.FinishedLaunching(app, options);
		}
	}
}
