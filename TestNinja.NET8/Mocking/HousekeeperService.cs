using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TestNinja.NET8.Mocking
{
    // changed to HousekeeperService from HousekeeperHelper
    // a service is high-level and iteracts with multiple objects
    public class HousekeeperService
    {
        private readonly IHousekeeperEmailService _emailService = default!;
        private readonly IStatementGenerator _generator = default!;

        // Mosh made an interface of UnitOfWork because all housekeepers are returned - no underlying logic
        private readonly IUnitOfWork _unitOfWork = default!;
		private readonly IXtraMessageBox _messageBox;

		public HousekeeperService(
            IStatementGenerator generator, 
            IHousekeeperEmailService emailService, 
            IUnitOfWork unitOfWork,
            IXtraMessageBox messageBox)
        {
            _emailService = emailService;
            _generator = generator;
            _unitOfWork = unitOfWork;
			_messageBox = messageBox;
		}

        public bool SendStatementEmails(DateTime statementDate)
        {
            var housekeepers = _unitOfWork.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = _generator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

				if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailService.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));

                }
                catch (Exception e)
                {
                    _messageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);

                    return false;
                }
            }

            return true;
        }

    }

    public enum MessageBoxButtons
    {
        OK
    }

	public interface IXtraMessageBox
	{
		void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
	}

	public class XtraMessageBox : IXtraMessageBox
	{
		public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
		{
		}
	}

	public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; } = default!;
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; } = default!;
		public static string EmailPassword { get; set; } = default!;
		public static string EmailFromEmail { get; set; } = default!;
		public static string EmailFromName { get; set; } = default!;
	}

    public class Housekeeper
    {
        public string? Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; } = default!;
        public string StatementEmailBody { get; set; } = default!;
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}