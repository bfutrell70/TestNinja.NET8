using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Fundamentals;
using Assert = NUnit.Framework.Assert;
using IgnoreAttribute = NUnit.Framework.IgnoreAttribute;
using Math = TestNinja.NET8.Fundamentals.Math;

namespace TestNinja.NET8.UnitTests
{
	[TestFixture]
	public class MathTests
	{
		private Math _math = default!;

		[SetUp]
		public void SetUp()
		{
			_math = new Math();
		}

		[Test]
		//[Ignore("Because I wanted to")]
		public void Add_WhenCalled_ReturnTheSumOfArguments()
		{
			var result = _math.Add(1, 2);

			Assert.That(result, Is.EqualTo(3));
		}

		[TestCase(1, 2, 2)]
		[TestCase(2, 1, 2)]
		[TestCase(2, 2, 2)]
		public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expected)
		{
			var result = _math.Max(a, b);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
