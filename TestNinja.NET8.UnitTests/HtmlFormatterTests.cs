using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Fundamentals;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests
{
	[TestFixture]
	public class HtmlFormatterTests
	{
		[Test]
		public void FormatAsBold_WhenCalled_ShouldEnclodeTheStringWithStrongElement()
		{
			var formatter = new HtmlFormatter();

			var result = formatter.FormatAsBold("abc");

			// specific
			Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

			// more general - useful for error messages
			// string comparisons are case sensitive by default - add .IgnoreCase 
			//   to ignore case
			Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
			Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
			Assert.That(result, Does.Contain("abc").IgnoreCase);

		}
	}
}
