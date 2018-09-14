using System;
using System.Globalization;
using System.Linq;
using MoreLinq;

namespace Linq.SoundCode
{
	class SoundCodeChallenge1
	{
		public static void Problem1()
		{
			var players = string.Join(", ",
				"Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert"
					.Split(',').Select((s, i) => $"{i + 1}.{s.Trim()}").ToArray());
			Console.WriteLine(players);
			Console.WriteLine();
		}


		public static void Problem2()
		{
			var playersOrderByAge =
				from player in
					"Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988"
						.Split(';').Select(s => s.Trim())
				let tokens = player.Split(',')
				orderby DateTime.ParseExact(tokens[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture) descending
				select new
				{
					name = tokens[0],
					age = (DateTime.Now - DateTime.ParseExact(tokens[1].Trim(),
						       "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days / 356.25
				};
			foreach (var p in playersOrderByAge)
			{
				Console.WriteLine($"{p.name,-30}{p.age}");
			}

			Console.WriteLine();
		}


		public static void Problem3()
		{
			var durations = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27".Split(',').Select(s => s.Trim());
			var sum = (from duration in durations
			           select TimeSpan.Parse("0:" + duration)).Aggregate((t1, t2) => t1 + t2);
			Console.WriteLine(sum);
		}


		public static void Problem4()
		{
			var coordinates = from x in new[] {0, 1, 2} from y in new[] {0, 1, 2} select new {x, y};
			foreach (var c in coordinates)
			{
				Console.WriteLine(c);
			}

			Console.WriteLine();
		}


		public static void Problem5()
		{
			var timeSwim = "00:45,01:32,02:18,03:01,03:44,04:31,05:19,06:01,06:47,07:35".Split(',')
				.Select((f, s) => new
				{
					first = TimeSpan.Parse("0:" + f),
					second = TimeSpan.Parse("0:" + s)
				}).Select(t => t.second - t.first);

			foreach (var t in timeSwim)
			{
				Console.WriteLine(t);
			}

			Console.WriteLine();
		}


		public static void Problem6()
		{
			var posNum =
				(from n in "2,5,7-10,11,17-18".Split(',')
				 let r = n.Split('-')
				 let first = int.Parse(r[0])
				 let last = int.Parse(r[r.Length - 1])
				 select (
					 from m in Enumerable.Range(first, last - first + 1)
					 select m
				 ))
				.Flatten();

			foreach (var n in posNum)
			{
				Console.WriteLine(n);
			}

			Console.WriteLine();
		}
	}
}