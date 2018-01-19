using System;
using XamarinForms.Essentials.Core;

namespace XamarinForms.Essentials.SampleApp.Models
{
    public class Item : DataObjectBase<Guid>
	{

		string text = string.Empty;
		public string Text
		{
			get { return text; }
			set { SetProperty(ref text, value); }
		}

		string description = string.Empty;
		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

	}

}