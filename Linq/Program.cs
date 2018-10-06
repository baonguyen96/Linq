using Linq.FlatFile;

namespace Linq
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var flatFile = new BankListFlatFile(@"../../FlatFile/Resources/BankList.csv", '~', "0");
			flatFile.Problem12();
		}
	}
}
