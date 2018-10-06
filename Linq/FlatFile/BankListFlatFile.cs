using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Linq.FlatFile.Model;
using MoreLinq;

namespace Linq.FlatFile
{
	class BankListFlatFile : FlatFileBase
	{
		public BankListFlatFile(string path, char delimiter, string filler = "") : base(path, delimiter, filler)
		{
		}

		public void Problem1()
		{
			// What are the column names?
			var result =
				from item in Records[0].Items
				select item.Key;

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r}{Delimiter}")));
		}

		public void Problem2()
		{
			// How many States (ST) are represented in this data set?
			var result =
				(from record in Records
				 select record.GetItemWithKey("st").Value)
				.Distinct()
				.Count();

			Console.WriteLine(result);
		}

		public void Problem3()
		{
			// Get list of states
			var result = Records.Select(i => i.GetItemWithKey("st").Value).Distinct().OrderBy(s => s);
			Console.WriteLine(string.Join("\n", result.Select((s, i) => $"{(i + 1) + ".",-5}{s}")));
		}

		public void Problem4()
		{
			// Top 5 states with the most failed banks
			var result = Records
			             .GroupBy(r => r.GetItemWithKey("st").Value)
			             .Select(g => new
			             {
				             state = g.Key,
				             count = g.Count()
			             })
			             .OrderByDescending(g => g.count)
			             .Take(5);

			Console.WriteLine(string.Join("\n", result.Select(r => $"{r.state}: {r.count}")));
		}

		public void Problem5()
		{
			// What are the top 5 acquiring institutions?
			var result = Records
			             .Where(r => r.GetItemWithKey("acquiring institution").Value != "No Acquirer")
			             .GroupBy(r => r.GetItemWithKey("acquiring institution").Value)
			             .Select(g => new
			             {
				             institution = g.Key,
				             count = g.Count()
			             })
			             .OrderByDescending(g => g.count)
			             .Take(5);

			Console.WriteLine(string.Join("\n", result.Select(r => $"{r.institution}: {r.count}")));
		}

		public void Problem6()
		{
			// How many banks has the State Bank of Texas acquired? How many of them were actually in Texas?
			var result = Records
			             .Where(r => r.GetItemWithKey("acquiring institution").Value == "State Bank of Texas")
			             .Select(r => new
			             {
				             name = r.GetItemWithKey("bank name").Value,
				             state = r.GetItemWithKey("st").Value
			             })
			             .OrderBy(b => b.name);

			Console.WriteLine(string.Join("\n", result.Select(r => $"{r.name}: {r.state}")));
		}

		public void Problem7()
		{
			// What is the most common city in California for a bank to fail in?
			var result = Records
			             .Where(r => r.GetItemWithKey("st").Value == "CA")
			             .GroupBy(r => r.GetItemWithKey("city").Value)
			             .OrderByDescending(g => g.Count())
			             .Select(g => g.Key)
			             .First();

			Console.WriteLine(result);
		}

		public void Problem8()
		{
			// How many failed banks don't have the word \"Bank\" in their name?
			var result = Records.Count(r => !r.GetItemWithKey("bank name").Value.Contains("Bank"));
			Console.WriteLine(result);
		}

		public void Problem9()
		{
			// How many bank names start with the letter 's' ?
			var result = Records.Count(r => r.GetItemWithKey("bank name").Value.StartsWith("S"));
			Console.WriteLine(result);
		}

		public void Problem10()
		{
			// How many CERT values are above 20000 ?
			var result = Records.Count(r => int.Parse(r.GetItemWithKey("cert").Value) > 20000);
			Console.WriteLine(result);
		}

		public void Problem11()
		{
			// How many bank names consist of just two words?
			var result = Records.Where(r => r.GetItemWithKey("bank name").Value.Trim().Split(' ').Length == 2)
			                    .Select(s => s.GetItemWithKey("bank name").Value)
			                    .OrderBy(s => s);
			Console.WriteLine(string.Join("\n", result.Select((s, i) => $"{(i + 1) + ".",-5}{s}")));
		}

		public void Problem12()
		{
			// How many banks closed in the year 2008?
			var result = Records.Count(r => DateTime.Parse(r.GetItemWithKey("closing date").Value).Year == 2008);
			Console.WriteLine(result);
		}
	}
}