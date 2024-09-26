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
	public class DemeritPointsCalculatorTests
	{
		// speed is less than 0 throws an exception
		// speed is more than the max speed throws an exception
		// speed less than or equal to the speed limit (65) returns 0
		// speed greater than the speed limit (65):
		//		calculates demerit points by 1 point per 5km/h over the speed limit

		[TestCase(-1)]
		[TestCase(301)]
		public void CalculateDemeritPoints_SpeedOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
		{
			// arrange
			var calculator = new DemeritPointsCalculator();

			// act/assert
			Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateDemeritPoints(speed));
		}

		[TestCase(0,0)]
		[TestCase(64,0)]
		[TestCase(65,0)]
		[TestCase(66, 0)]
		[TestCase(70, 1)]
		[TestCase(75, 2)]
		public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoint(int speed, int expected)
		{
			// arrange
			var calculator = new DemeritPointsCalculator();

			// act
			var result = calculator.CalculateDemeritPoints(speed);

			// assert
			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
