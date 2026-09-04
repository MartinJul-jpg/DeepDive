using DeepDive.Models;
using DeepDive.ViewModels;
using System.Runtime.CompilerServices;

namespace DeepDive.Repository
{
    public static class BookingRepository
    {
        private static Booking booking;

        public static Booking GetAllBookings()
        {
            return booking;
        }

        public static void AddProductToBooking(int id)
        {
            if (booking == null)
            {
                booking = new Booking { BookingId = id};
            }
            else
            {
                booking.BookedProducts.Add(id);

            }
        }
    }
}