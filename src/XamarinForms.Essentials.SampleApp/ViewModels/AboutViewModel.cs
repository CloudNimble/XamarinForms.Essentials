using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Essentials.SampleApp.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{

        #region Private Members

        private bool isEnabled = true;

        #endregion


        public bool Enabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }


        public AboutViewModel()
		{
			Title = "About";

			OpenWebCommand = new RelayCommand(c => OpenUrl(), c => CanOpenUrl(), this);
        }

		/// <summary>
		/// Command to open browser to xamarin.com
		/// </summary>
		public ICommand OpenWebCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public void OpenUrl() => Enabled = !Enabled;

        //public void OpenUrl() => Device.OpenUri(new Uri("https://xamarin.com/platform"));



        public bool CanOpenUrl() => Enabled;

    }

}