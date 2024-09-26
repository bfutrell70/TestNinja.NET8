using TestNinja.NET8.Fundamentals;

namespace TestNinja.NET8.UnitTests
{
	[TestClass]
	public class ReservationTests
	{
		[TestMethod]
		public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
		{
			// arrange - initialize object to test
			var reservation = new Reservation();

			// act
			var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

			// assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanBeCancelledBy_UserIsSame_ReturnsTrue()
		{
			// arrange - initialize object to test
			var user = new User();
			var reservation = new Reservation { MadeBy = user };

			// act
			var result = reservation.CanBeCancelledBy(user);

			// assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanBeCancelledBy_UserIsNotSameAndNotAdmin_ReturnsFalse()
		{
			// arrange - initialize object to test
			var reservation = new Reservation { MadeBy = new User() };

			// act
			var result = reservation.CanBeCancelledBy(new User());

			// assert
			Assert.IsFalse(result);
		}
	}
}