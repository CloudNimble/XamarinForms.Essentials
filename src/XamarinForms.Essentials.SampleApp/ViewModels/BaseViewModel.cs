using XamarinForms.Essentials.SampleApp.Models;
using Xamarin.Forms;
using XamarinForms.Essentials.Core;
using System;

namespace XamarinForms.Essentials.SampleApp.ViewModels
{
    public class BaseViewModel : ObservableObject
	{
		/// <summary>
		/// Get the azure service instance
		/// </summary>
		public IDataStore<Item, Guid> DataStore => DependencyService.Get<IDataStore<Item, Guid>>();

		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
	}
}

