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
	public class FizzBuzzTests
	{
		/*
		 * FizzBuzz
		 * number divisible by 3: return "Fizz"
		 * number divisible by 5: return "Buzz"
		 * number divisible by 3 AND 5: return "FizzBuzz"
		 * otherwise return a string containing the number
		 * 
		 */
		
		[Test]
		public void GetOutput_ArgumentDivisibleOnlyByThree_ReturnsFizz()
		{
			var result = FizzBuzz.GetOutput(3);

			Assert.That(result, Is.EqualTo("Fizz").IgnoreCase);
		}

		[Test]
		public void GetOutput_ArgumentDivisibleOnlyByFive_ReturnsBuzz()
		{
			var result = FizzBuzz.GetOutput(5);

			Assert.That(result, Is.EqualTo("Buzz").IgnoreCase);
		}

		[Test]
		public void GetOutput_ArgumentDivisibleByThreeAndFive_ReturnsFizzBuzz()
		{
			var result = FizzBuzz.GetOutput(15);

			Assert.That(result, Is.EqualTo("FizzBuzz").IgnoreCase);
		}

		[Test]
		public void GetOutput_ArgumentNotDivisibleByThreeOrFive_ReturnsSameNumber()
		{
			var result = FizzBuzz.GetOutput(1);

			Assert.That(result, Is.EqualTo("1"));
		}
	}
}
