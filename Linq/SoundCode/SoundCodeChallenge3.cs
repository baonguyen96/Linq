using System;
using System.Linq;
using MoreLinq;

namespace Linq.SoundCode
{
	class SoundCodeChallenge3
	{
		public static void Problem1()
		{
			var sales = new[]
				{1, 2, 1, 1, 0, 3, 1, 0, 0, 2, 4, 1, 0, 0, 0, 0, 2, 1, 0, 3, 1, 0, 0, 0, 6, 1, 3, 0, 0, 0};
			var longestSequece =
				MoreEnumerable.GroupAdjacent(sales, s => s == 0 ? "N" : "Y").Where(g => g.Key == "N")
					.Max(g => g.Count());
			Console.WriteLine(longestSequece);
			Console.WriteLine();
		}

		public static void Problem2()
		{
			var fullHouse =
				from hand in ("4♣ 5♦ 6♦ 7♠ 10♥;1♣ Q♥ 10♠ Q♠ 10♦;6♣ 6♥ 6♠ A♠ 6♦;2♣ 3♥ 3♠ 2♠ 2♦;2♣ 3♣ 4♣ 5♠ 6♠"
					.Split(';')
					.Select(x => x))
				where (
					hand.Split(' ').GroupBy(x => new string(x.Take(x.Length - 1).ToArray()))
						.All(g => g.Count() == 3 || g.Count() == 2)
				)
				select hand;

			foreach (var i in fullHouse)
			{
				Console.WriteLine(i);
			}
		}


		public static void Problem3()
		{
			var christmasDay = from year in Enumerable.Range(2018, 10)
			                   select new
			                   {
				                   Year = year,
				                   Day = DateTime.Parse($"12/25/{year}").DayOfWeek
			                   };
			foreach (var v in christmasDay)
			{
				Console.WriteLine(v);
			}

			Console.WriteLine();
		}

		public static void Problem4()
		{
			var anagrams =
				from s in (
					from word in "parts,traps,arts,rats,starts,tarts,rat,art,tar,tars,stars,stray".Split(',')
					select new
					{
						original = word,
						sorted = string.Concat(word.OrderBy(c => c))
					})
				where s.sorted == string.Concat("star".OrderBy(c => c))
				select s;

			foreach (var a in anagrams)
			{
				Console.WriteLine(a);
			}

			Console.WriteLine();
		}

		public static void Problem5()
		{
			var initials =
				from x in (
					from name in
						"Santi Cazorla, Per Mertesacker, Alan Smith, Thierry Henry, Alex Song, Paul Merson, Alexis Sánchez, Robert Pires, Dennis Bergkamp, Sol Campbell"
							.Split(',').Select(s => s.Trim())
					select new
					{
						il = string.Join("", name.Split(' ').Select(s => s[0])),
						fullName = name
					})
				orderby x.il, x.fullName
				group x by x.il
				into g
				where g.Count() > 1
				select new
				{
					il = g.Key,
					c = g.ToList()
				};

			foreach (var i in initials)
			{
				Console.WriteLine(i.il);

				foreach (var fn in i.c)
				{
					Console.WriteLine($"   {fn.fullName}");
				}
			}
		}

		public static void Problem6()
		{
			throw new Exception("stuck");
		}
	}
}