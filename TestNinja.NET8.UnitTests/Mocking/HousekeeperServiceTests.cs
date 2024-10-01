using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Mocking;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests.Mocking
{
	[TestFixture]
	public class HousekeeperServiceTests
	{
		private Mock<IStatementGenerator> _generator = default!;
		private Mock<IHousekeeperEmailService> _emailService = default!;
		private DateTime _statementDate = new DateTime(2024, 09, 30);
		private string? _statementFilename = default!;
		private Housekeeper _housekeeper = default!;

		private HousekeeperService _housekeeperService = default!;
		private Mock<IUnitOfWork> _unitOfWork = default!;
		private Mock<IXtraMessageBox> _messageBox = default!;

		[SetUp]
		public void Setup()
		{
			_housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
			_unitOfWork = new Mock<IUnitOfWork>();
			_unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper> { _housekeeper }.AsQueryable());

			_statementFilename = "filename";
			_generator = new Mock<IStatementGenerator>();
			_generator
				.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
				//.Returns(_statementFilename);
				// this is lazy evaluation
				.Returns(() => _statementFilename);

			_emailService = new Mock<IHousekeeperEmailService>();

			_messageBox = new Mock<IXtraMessageBox>();

			_housekeeperService = new HousekeeperService(_generator.Object, _emailService.Object, _unitOfWork.Object, _messageBox.Object);
		}

		// ---- Mosh's tests ----
		[Test]
		public void SendStatementEmails_WhenCalled_GenerateStatements()
		{
			// arrange

			// act
			var result = _housekeeperService.SendStatementEmails(_statementDate);

			// assert
			_generator.Verify(sg => sg.SaveStatement(
				_housekeeper.Oid,
				_housekeeper.FullName,
				_statementDate));
		}

		[TestCase(null)]
		[TestCase(" ")]
		[TestCase("")]
		public void SendStatementEmails_HousekeeperEmailIsNullOrEmpty_ShouldNotGenerateStatements(string? email)
		{
			// arrange
			_housekeeper.Email = email;

			// act
			var result = _housekeeperService.SendStatementEmails(_statementDate);

			// assert
			_generator.Verify(sg => sg.SaveStatement(
				_housekeeper.Oid,
				_housekeeper.FullName,
				_statementDate), Times.Never);
		}

		[Test]
		public void SendStatementEmails_WhenCalled_EmailTheStatement()
		{
			// arrange

			// act
			var result = _housekeeperService.SendStatementEmails(_statementDate);

			// assert
			VerifyEmailSent();
		}

		[TestCase(null)]
		[TestCase(" ")]
		[TestCase("")]
		public void SendStatementEmails_StatementFilenameIsNullOrEmpty_ShouldNotEmailTheStatement(string? filename)
		{
			// arrange
			_statementFilename = filename;
			//_generator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
			//	.Returns(() => filename);

			// act
			var result = _housekeeperService.SendStatementEmails(_statementDate);

			// assert
			VerifyEmailNotSent();
		}

		[Test]
		public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
		{
			// arrange
			_emailService.Setup(x => x.EmailFile(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>())).Throws<Exception>();

			// act
			_housekeeperService.SendStatementEmails(_statementDate);

			// assert
			VerifyMessageBoxDisplayed();
		}

		#region helper methods for assertions
		// NOTE - it seems a bit odd to have the test assertions in a separate method
		private void VerifyMessageBoxDisplayed()
		{
			_messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
		}

		private void VerifyEmailNotSent()
		{
			_emailService.Verify(sg => sg.EmailFile(
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>(),
				It.IsAny<string>()), Times.Never);
		}

		private void VerifyEmailSent()
		{
			_emailService.Verify(es => es.EmailFile(
				_housekeeper.Email!,
				_housekeeper.StatementEmailBody,
				_statementFilename!,
				It.IsAny<string>()));
		}
		#endregion

	}
}
