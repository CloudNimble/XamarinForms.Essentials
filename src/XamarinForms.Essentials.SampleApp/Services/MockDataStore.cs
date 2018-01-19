using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinForms.Essentials.SampleApp.Models;
using Xamarin.Forms;
using XamarinForms.Essentials.Core;

[assembly: Dependency(typeof(XamarinForms.Essentials.SampleApp.Services.MockDataStore))]
namespace XamarinForms.Essentials.SampleApp.Services
{
	public class MockDataStore : DataStoreBase<Item, Guid>
	{

		public override async Task<bool> InitializeAsync(string filter = "")
		{
			if (IsInitialized)
				return true;

			items = new List<Item>();
			var _items = new List<Item>
			{
				new Item { Id = Guid.NewGuid(), Text = "Buy some cat food", Description="The cats are hungry. So we should feed them. No, I'm serious. They need to eat, "
                 + "otherwise they are going to tear apart the couches. Please don't forget, or you're cleaning up after them."},
				new Item { Id = Guid.NewGuid(), Text = "Learn F#", Description="Seems like a functional idea"},
				new Item { Id = Guid.NewGuid(), Text = "Learn to play guitar", Description="Noted"},
				new Item { Id = Guid.NewGuid(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
				new Item { Id = Guid.NewGuid(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
				new Item { Id = Guid.NewGuid(), Text = "Finish a todo list", Description="Done"},
			};

			foreach (Item item in _items)
			{
				items.Add(item);
			}

            return await base.InitializeAsync(filter);
		}
	}
}
