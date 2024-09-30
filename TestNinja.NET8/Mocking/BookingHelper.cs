using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.NET8.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookings)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

			// from https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
			var overlappingBooking =
                bookings.GetActiveBookings(booking.Id).FirstOrDefault(
                    b => booking.ArrivalDate < b.DepartureDate && b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; } = default!;
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; } = default!;
    }
}