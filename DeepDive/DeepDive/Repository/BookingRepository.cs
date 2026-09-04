using DeepDive.Models;

namespace DeepDive.Repository
{
    public static class BookingRepository
    {
        private static List<Booking> bookings = new List<Booking>();

        public static List<Booking> GetAllBookings()
        {
            return bookings;
        }

        public static Booking? GetBookingById(int id)
        {
            return bookings.FirstOrDefault(booking => booking.BookingId == id);
        }
    }
}