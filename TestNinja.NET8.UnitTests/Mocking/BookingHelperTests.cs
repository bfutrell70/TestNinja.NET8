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
	public class BookingHelper_OverlappingBookngsExist_Tests
	{
		private Mock<IBookingRepository> _repository = default!;
		private Booking _existingBooking = default!;

		[SetUp]
		public void Setup()
		{
			_existingBooking = new Booking
			{
				Id = 2,
				ArrivalDate = ArriveOn(2017, 1, 15),
				DepartureDate = DepartOn(2017, 1, 20),
				Reference = "a",
			};

			_repository = new Mock<IBookingRepository>();
			_repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
			{
				_existingBooking
			}.AsQueryable());
		}

		// Mosh's tests
		[Test]
		public void BookingStartsAndFinishesBeforeanExistingBooking_ReturnsEmptyString()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
				DepartureDate = Before(_existingBooking.ArrivalDate),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.Empty);
		}

		[Test]
		public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.ArrivalDate),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingsReference()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.DepartureDate),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.ArrivalDate),
				DepartureDate = Before(_existingBooking.DepartureDate),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void BookingStartsInTheMiddleOfAnExistingBookingButFinishesAfter_ReturnsExistingBookingsReference()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.DepartureDate),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.EqualTo(_existingBooking.Reference));
		}

		[Test]
		public void BookingStartsAndFinishesAfterExistingBooking_ReturnsEmptyString()
		{
			// arrange


			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = After(_existingBooking.DepartureDate),
				DepartureDate = After(_existingBooking.DepartureDate, days: 2),
			}, _repository.Object);

			// assert
			Assert.That(result, Is.Empty);
		}

		[Test]
		public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
		{
			// arrange

			// act
			var result = BookingHelper.OverlappingBookingsExist(new Booking
			{
				Id = 1,
				ArrivalDate = Before(_existingBooking.ArrivalDate),
				DepartureDate = After(_existingBooking.ArrivalDate),
				Status = "Cancelled"
			}, _repository.Object);

			// assert
			Assert.That(result, Is.Empty);
		}

		private DateTime ArriveOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 14, 0, 0);
		}

		private DateTime DepartOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 10, 0, 0);
		}

		private DateTime Before(DateTime dateTime, int days = 1)
		{
			return dateTime.AddDays(-days);
		}

		private DateTime After(DateTime dateTime, int days = 1)
		{
			return dateTime.AddDays(days);
		}
	}
}
