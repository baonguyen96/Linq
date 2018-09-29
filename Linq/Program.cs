using System;
using Linq.FlatFile;
using Linq.SoundCode;

namespace Linq
{
	class Program
	{
		static void Main(string[] args)
		{
			var flatFile = new BattingFlatFile(@"../../FlatFile/Resources/Batting.csv", ',', "0");
			flatFile.Problem10();
		}
	}
}
