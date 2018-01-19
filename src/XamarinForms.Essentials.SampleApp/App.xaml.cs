using XamarinForms.Essentials.SampleApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinForms.Essentials.SampleApp
{
	public partial class FormsApp : Application
	{
        public FormsApp()
		{
			InitializeComponent();

			SetMainPage();
		}

		public static void SetMainPage()
		{
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Xamarin.Forms.Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Xamarin.Forms.Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //MessagingCenter.Subscribe<MessagingCenterAlert>(this, "message", async (info) =>
            //{
            //    var task = Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

            //    if (task == null)
            //        return;

            //    await task;
            //    info?.OnCompleted?.Invoke();
            //});

            Microsoft.AppCenter.AppCenter.Start("ios=72317393-9e6a-4d14-856c-46c3db111728;android=a9b3e3ea-08e9-4d54-aaf2-8d04e5e6884f;",
                typeof(Analytics), typeof(Crashes));
        }

    }

}
