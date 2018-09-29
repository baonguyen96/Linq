using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.FlatFile.Model
{
	
	public partial class FlatFileBase
	{
		protected class Record
		{
			public List<Item> Items { get; set; }

			public Record()
			{
				Items = new List<Item>();
			}

			public void AddNewItem(string key, string value)
			{
				Items.Add(new Item(key, value));
			}

			public Item GetItemWithKey(string key)
			{
				return Items.FirstOrDefault(i => string.Equals(i.Key, key, StringComparison.CurrentCultureIgnoreCase));
			}
		}

		protected class Item
		{
			public Item(string key, string value)
			{
				Key = key;
				Value = value;
			}

			public string Key { get; set; }
			public string Value { get; set; }
		}

	}
}
