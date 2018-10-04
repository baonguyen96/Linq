using Linq.FlatFile;

namespace Linq
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var flatFile = new BattingFlatFile(@"../../FlatFile/Resources/Batting.csv", ',', "0");
			flatFile.Problem25();
		}
	}
}
