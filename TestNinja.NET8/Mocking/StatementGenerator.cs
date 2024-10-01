using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.NET8.Mocking
{
	public interface IStatementGenerator
	{
		string? SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
	}

	public class StatementGenerator : IStatementGenerator
	{
		public string? SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate)
		{
			var report = new HousekeeperStatementReport(housekeeperOid, statementDate);

			if (!report.HasData)
				return string.Empty;

			report.CreateDocument();

			var filename = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeperName));

			report.ExportToPdf(filename);

			return filename;
		}
	}
}
