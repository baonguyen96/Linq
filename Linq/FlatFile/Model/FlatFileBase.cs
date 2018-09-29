using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Linq.FlatFile.Model
{
	abstract partial class FlatFileBase
	{
		protected List<Record> Records { get; set; }
		protected char Delimiter { get; set; }

		protected FlatFileBase(string path, char delimiter, string filler = "")
		{
			Records = new List<Record>();
			Delimiter = delimiter;
			PopulateRecords(path, delimiter, filler);
		}

		private void PopulateRecords(string path, char delimiter, string filler)
		{
			var allLines = File.ReadLines(path).ToArray();
			var headers = allLines[0].Split(delimiter);
			var content = allLines.Skip(1).ToArray();

			foreach (var line in content)
			{
				var record = new Record();
				var currentLineTokens = line.Split(delimiter);

				for (var i = 0; i < headers.Length; i++)
				{
					record.AddNewItem(headers[i], string.IsNullOrWhiteSpace(currentLineTokens[i]) ? filler : currentLineTokens[i]);
				}

				Records.Add(record);
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append(string.Join(",", Records[0].Items.Select(i => i.Key)));
			stringBuilder.Append("\r\n");
			
			foreach (var record in Records)
			{
				stringBuilder.Append(string.Join(",", record.Items.Select(i => i.Value)));
				stringBuilder.Append("\r\n");
			}

			return stringBuilder.ToString();
		}
	}
}