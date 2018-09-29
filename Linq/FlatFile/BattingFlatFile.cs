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
	}
}