using NUnit.Framework;
using TestNinja.NET8.Fundamentals;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests
{
	[TestFixture]
	public class ReservationTests
	{
		[Test]
		public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
		{
			// arrange - initialize object to test
			var reservation = new Reservation();

			// act
			var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

			// assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void CanBeCancelledBy_UserIsSame_ReturnsTrue()
		{
			// arrange - initialize object to test
			var user = new User();
			var reservation = new Reservation { MadeBy = user };

			// act
			var result = reservation.CanBeCancelledBy(user);

			// assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void CanBeCancelledBy_UserIsNotSameAndNotAdmin_ReturnsFalse()
		{
			// arrange - initialize object to test
			var reservation = new Reservation { MadeBy = new User() };

			// act
			var result = reservation.CanBeCancelledBy(new User());

			// assert
			Assert.That(result, Is.False);
		}
	}
}