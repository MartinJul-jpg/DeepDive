using DeepDive.Models;

namespace DeepDive.Repository
{
    public static class BookingRepository
    {
        private static List<Booking> bookings = new List<Booking>();

        public static List<Booking> GetAllBookings()
        {
            List<int> list = new List<int>();
            list.Add(10);
            list.Add(11);
            Booking booking = new Booking();
            booking.BookedProducts = list;
            booking.BookingId = 5;
            booking.Start = DateOnly.MinValue;
            booking.End = DateOnly.MaxValue;
            bookings.Add(booking);
            return bookings;
        }

        public static Booking? GetBookingById(int id)
        {
            return bookings.FirstOrDefault(booking => booking.BookingId == id);
        }

        
    }
}