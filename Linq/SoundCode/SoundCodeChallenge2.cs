using System;
using System.Linq;
using MoreLinq;

namespace Linq.SoundCode
{
	class SoundCodeChallenge2
	{
		public static void Problem1()
		{
			var score = new[] {10, 5, 0, 8, 10, 1, 4, 0, 10, 1};
			var sum = score.OrderByDescending(i => i).Take(score.Length - 3).Sum(i => i);
			Console.WriteLine(sum);
			Console.WriteLine();
		}

		public static void Problem2()
		{
			var moves = from row in Enumerable.Range('a', 8)
			            from col in Enumerable.Range('1', 8)
			            let dx = Math.Abs(row - 'c')
			            let dy = Math.Abs(col - '6')
			            where dx == dy && dx != 0
			            select $"{(char) row}{(char) col}";

			foreach (var m in moves)
			{
				Console.WriteLine(m);
			}

		}


		public static void Problem3()
		{
			var samples = new[]
			{
				0, 6, 12, 18, 24, 30, 36, 42, 48, 53, 58, 63, 68, 72, 77, 80, 84, 87, 90, 92, 95, 96, 98, 99, 99, 100,
				99, 99, 98, 96, 95, 92, 90, 87, 84, 80, 77, 72, 68, 63, 58, 53, 48, 42, 36, 30, 24, 18, 12, 6, 0, -6,
				-12, -18, -24, -30, -36, -42, -48, -53, -58, -63, -68, -72, -77, -80, -84, -87, -90, -92, -95, -96, -98,
				-99, -99, -100, -99, -99, -98, -96, -95, -92, -90, -87, -84, -80, -77, -72, -68, -63, -58, -53, -48,
				-42, -36, -30, -24, -18, -12, -6
			};

			var every5 = samples.AsEnumerable().Select((value, index) => new {value, index})
				.Where(t => (t.index + 1) % 5 == 0).Select(t => t.value);

			foreach (var i in every5)
			{
				Console.Write($"{i}, ");
			}

			Console.WriteLine();
		}


		public static void Problem4()
		{
			var votes = "Yes,Yes,No,Yes,No,Yes,No,No,No,Yes,Yes,Yes,Yes,No,Yes,No,No,Yes,Yes".Split(',');
			var yes = votes.Count(s => s == "Yes");
			var no = votes.Count(s => s == "No");
			Console.WriteLine(yes - no);
			Console.WriteLine();
		}

		public static void Problem5()
		{
			var animals = "Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea Pig,Dog".Split(',');
			var animalGroup = animals.GroupBy(a => (a != "Dog" && a != "Cat") ? "Other" : a);

			foreach (var g in animalGroup)
			{
				Console.WriteLine($"{g.Key,-20}{g.Count()}");
			}
		}


		public static void Problem6()
		{
			var rs = from token in "A5B10CD3".GroupAdjacent(char.IsDigit) select token;

			foreach (var r in rs)
			{
				Console.WriteLine("New group:");
				foreach (var c in r)
				{
					Console.WriteLine($"   {c}");
				}
			}
		}

	}
}