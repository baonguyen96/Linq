using System;
using System.Linq;
using System.Net.Sockets;
using Linq.FlatFile.Model;
using MoreLinq.Extensions;

namespace Linq.FlatFile
{
	class BattingFlatFile : FlatFileBase
	{
		public BattingFlatFile(string path, char delimiter, string filler = "") : base(path, delimiter, filler)
		{
		}

		public void Problem3()
		{
			// How many players have hit 40 or more HRs in one single season? (Number only)
			var result =
				(from player in
				 (
					 (from record in Records
					  group record by new
					  {
						  playerId = record.GetItemWithKey("playerID").Value,
						  yearId = record.GetItemWithKey("yearID").Value
					  }
					  into g
					  select new
					  {
						  g.Key.playerId,
						  g.Key.yearId,
						  hr = g.Sum(_ => int.Parse(_.GetItemWithKey("HR").Value))
					  })
				 )
				 where player.hr >= 40
				 group player by player.playerId
				 into g
				 select g.Key).Count();

			Console.WriteLine(result);
		}

		public void Problem4()
		{
			// How many players have hit 600 or more HRs for their career? (Dataframe with name and player first and last name only and amount of HRs)
			var result =
				from player in
				(
					from record in Records
					group record by new
					{
						id = record.GetItemWithKey("playerID").Value,
						firstName = record.GetItemWithKey("nameFirst").Value,
						lastName = record.GetItemWithKey("nameLast").Value
					}
					into g
					select new
					{
						g.Key.id,
						g.Key.firstName,
						g.Key.lastName,
						hr = g.Sum(_ => int.Parse(_.GetItemWithKey("HR").Value))
					})
				where player.hr >= 600
				orderby player.hr descending
				select player;

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.hr}")));
		}


		public void Problem5()
		{
			// How many unique players have hit 40 2Bs or more, 10 3Bs or more, 200 Hits or more, and 30 HRs or more in one season? (Number Only)
			var result =
				(from player in (
					 from record in Records
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerID").Value,
						 yearId = record.GetItemWithKey("yearID").Value
					 }
					 into g
					 select new
					 {
						 g.Key.playerId,
						 twoB = g.Sum(_ => int.Parse(_.GetItemWithKey("2B").Value)),
						 threeB = g.Sum(_ => int.Parse(_.GetItemWithKey("3B").Value)),
						 hit = g.Sum(_ => int.Parse(_.GetItemWithKey("H").Value)),
						 hr = g.Sum(_ => int.Parse(_.GetItemWithKey("HR").Value))
					 }
				 )
				 where player.twoB >= 40 && player.threeB >= 10 && player.hit >= 200 && player.hr >= 30
				 group player by player.playerId
				 into g
				 select g.Count()).Count();

			Console.WriteLine(result);
		}

		public void Problem6()
		{
			// How many players seasons have had 100 or more SBs? (Dataframe, first name, last name, yearID, number of SBs, ordered from descending SBs )
			var result =
				from player in
				(
					from record in Records
					group record by new
					{
						playerId = record.GetItemWithKey("playerId").Value,
						yearId = record.GetItemWithKey("yearId").Value,
						firstName = record.GetItemWithKey("namefirst").Value,
						lastName = record.GetItemWithKey("namelast").Value
					}
					into g
					select new
					{
						g.Key.firstName,
						g.Key.lastName,
						g.Key.yearId,
						sb = g.Sum(_ => int.Parse(_.GetItemWithKey("sb").Value))
					}
				)
				where player.sb >= 100
				orderby player.sb descending
				select player;

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.yearId}{Delimiter}{r.sb}")));
		}


		public void Problem7()
		{
			// How many players in the 1960s have hit more than 200 HRs? (Dataframe, first name, last name, number of HRs, ordered descending by HR amount)
			var result =
				from player in
				(
					from record in Records
					let year = record.GetItemWithKey("yearid").Value
					where int.Parse(year) >= 1960 && int.Parse(year) <= 1969
					group record by new
					{
						playerId = record.GetItemWithKey("playerid").Value,
						firstName = record.GetItemWithKey("namefirst").Value,
						lastName = record.GetItemWithKey("namelast").Value
					}
					into g
					select new
					{
						g.Key.firstName,
						g.Key.lastName,
						hr = g.Sum(_ => int.Parse(_.GetItemWithKey("hr").Value))
					}
				)
				where player.hr > 200
				orderby player.hr descending
				select player;

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.hr}")));
		}


		public void Problem8()
		{
			// Who has hit the most HRs in history? (Dataframe, first name, last name, HRs)
			var result =
				(from player in
				 (
					 from record in Records
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.firstName,
						 g.Key.lastName,
						 hr = g.Sum(_ => int.Parse(_.GetItemWithKey("hr").Value))
					 }
				 )
				 orderby player.hr descending
				 select player).FirstOrDefault();

			Console.WriteLine(
				string.Join("\n", $"{result.firstName}{Delimiter}{result.lastName}{Delimiter}{result.hr}"));
		}


		public void Problem9()
		{
			// Who had the most hits (H) in the 1970s? (Dataframe)
			var result =
				(from player in
				 (
					 from record in Records
					 let year = int.Parse(record.GetItemWithKey("yearid").Value)
					 where year >= 1970 && year <= 1979
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.firstName,
						 g.Key.lastName,
						 hit = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value))
					 }
				 )
				 orderby player.hit descending
				 select player).FirstOrDefault();

			Console.WriteLine(string.Join("\n",
				$"{result.firstName}{Delimiter}{result.lastName}{Delimiter}{result.hit}"));
		}


		public void Problem10()
		{
			// Top 5 highest OBP (on base percentage) with at least 500 PAs in 1977?  (Dataframe, first name, last name, OBP, ordered descending by OBP)
			// ?
			var result =
				(from player in
				 (
					 from record in Records
					 where int.Parse(record.GetItemWithKey("yearid").Value) == 1977
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.firstName,
						 g.Key.lastName,
						 pa = g.Sum(_ => int.Parse(_.GetItemWithKey("ab").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("bb").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("hbp").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("sf").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("sh").Value)),
						 obpTop = (
							 g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value)) +
							 g.Sum(_ => int.Parse(_.GetItemWithKey("bb").Value)) +
							 g.Sum(_ => int.Parse(_.GetItemWithKey("hbp").Value))
						 ),

						 obpBottom =
						 (
							 g.Sum(_ => int.Parse(_.GetItemWithKey("ab").Value)) +
							 g.Sum(_ => int.Parse(_.GetItemWithKey("bb").Value)) +
							 g.Sum(_ => int.Parse(_.GetItemWithKey("hbp").Value)) +
							 g.Sum(_ => int.Parse(_.GetItemWithKey("sf").Value))
						 )
					 }
				 )
				 where player.pa >= 500
				 let obp = player.obpBottom == 0 ? 0 : ((double) player.obpTop / player.obpBottom) * 100
				 orderby obp descending
				 select new
				 {
					 player.firstName,
					 player.lastName,
					 obp
				 }).Take(5);

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.obp:#.##}%")));
		}


		public void Problem11()
		{
			// Top 8 highest averages in 2013 with at least 300 PAs? (Dataframe, first name, last name, average, descending by average)
			var result =
				(from player in
				 (
					 from record in Records
					 where int.Parse(record.GetItemWithKey("yearid").Value) == 2013
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.playerId,
						 g.Key.firstName,
						 g.Key.lastName,
						 pa = g.Sum(_ => int.Parse(_.GetItemWithKey("ab").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("bb").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("hbp").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("sf").Value)) +
						      g.Sum(_ => int.Parse(_.GetItemWithKey("sh").Value)),
						 baTop = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value)),
						 baBottom = g.Sum(_ => int.Parse(_.GetItemWithKey("ab").Value))
					 }
				 )
				 where player.pa > 300
				 let ba = player.baBottom == 0 ? 0 : ((double) player.baTop / player.baBottom) * 100
				 orderby ba descending
				 select new
				 {
					 player.firstName,
					 player.lastName,
					 ba
				 }).Take(8);

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.ba:#.##}%")));
		}

		public void Problem12()
		{
			// Leaders in hits from 1940 up to and including 1949. (Dataframe, first name, last name, number of hits) (top 5 players most hit in the time frame)
			var result =
				(from player in
				 (
					 from record in Records
					 let year = int.Parse(record.GetItemWithKey("yearid").Value)
					 where year >= 1940 && year <= 1949
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.firstName,
						 g.Key.lastName,
						 hit = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value))
					 }
				 )
				 orderby player.hit descending
				 select player).Take(5);

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.hit}")));
		}


		public void Problem13()
		{
			// Who led MLB with the most hits the most times?  And how many times?  (Dataframe, Number of hits)
			var result =
				(from a in
				 (
					 // player with best hit per year
					 from player in
					 (
						 // hit per year per player
						 from record in Records
						 group record by new
						 {
							 playerId = record.GetItemWithKey("playerid").Value,
							 yearId = record.GetItemWithKey("yearid").Value
						 }
						 into g
						 select new
						 {
							 g.Key.playerId,
							 g.Key.yearId,
							 hit = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value))
						 }
					 )
					 group player by player.yearId
					 into g
					 select g.OrderByDescending(x => x.hit).First()
				 )
				 group a by a.playerId
				 into g
				 orderby g.Count() descending
				 select new
				 {
					 playerId = g.Key,
					 count = g.Count()
				 }).First();

			Console.WriteLine(string.Join("\n", $"{result.playerId}{Delimiter}{result.count}"));
		}


		public void Problem14()
		{
			// Which players have played the most games for their careers?  Top 5 first name, last name, descending by games played presented as a dataframe
			var result =
				(from player in
				 (
					 from record in Records
					 group record by new
					 {
						 playerId = record.GetItemWithKey("playerid").Value,
						 firstName = record.GetItemWithKey("namefirst").Value,
						 lastName = record.GetItemWithKey("namelast").Value
					 }
					 into g
					 select new
					 {
						 g.Key.firstName,
						 g.Key.lastName,
						 games = g.Sum(_ => int.Parse(_.GetItemWithKey("g").Value))
					 }
				 )
				 orderby player.games descending
				 select player).Take(5);

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.games}")));
		}

		public void Problem15()
		{
			// How many players have had more 3000 or more hits for their careers while also hitting 500 or more HRs?  Just a number is okay here
			var result =
				(from player in
				 (
					 from record in Records
					 group record by record.GetItemWithKey("playerid").Value
					 into g
					 select new
					 {
						 g.Key,
						 hit = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value)),
						 hr = g.Sum(_ => int.Parse(_.GetItemWithKey("hr").Value)),
					 }
				 )
				 where player.hit >= 3000 && player.hr >= 500
				 select player).Count();

			Console.WriteLine(result);
		}

		public void Problem16()
		{
			// How many HRs were hit during the entire 1988 season?  Just a number is okay here
			var result =
				(from record in Records
				 where record.GetItemWithKey("yearid").Value == "1988"
				 select int.Parse(record.GetItemWithKey("hr").Value)).Sum();

			Console.WriteLine(result);
		}

		public void Problem17()
		{
			// Please filter out and show me the top 3 average seasons by Wade Boggs during his career in seasons in which he had at least 500 or more ABs.  Dataframe, first name, last name, average, descending by average
			var result =
				(from player in
				 (
					 from record in Records
					 where record.GetItemWithKey("namefirst").Value == "Wade" &&
					       record.GetItemWithKey("namelast").Value == "Boggs" &&
					       int.Parse(record.GetItemWithKey("ab").Value) >= 500
					 group record by record.GetItemWithKey("yearid").Value
					 into g
					 select new
					 {
						 year = g.Key,
						 baTop = g.Sum(_ => int.Parse(_.GetItemWithKey("h").Value)),
						 baBottom = g.Sum(_ => int.Parse(_.GetItemWithKey("ab").Value))
					 }
				 )
				 let ba = player.baBottom == 0 ? 0 : ((double) player.baTop / player.baBottom) * 100
				 orderby ba descending
				 select new
				 {
					 firstName = "Wade",
					 lastName = "Boggs",
					 player.year,
					 ba
				 }).Take(3);

			Console.WriteLine(string.Join("\n",
				result.Select(r => $"{r.firstName}{Delimiter}{r.lastName}{Delimiter}{r.year}{Delimiter}{r.ba:#.##}%")));
		}
	}
}