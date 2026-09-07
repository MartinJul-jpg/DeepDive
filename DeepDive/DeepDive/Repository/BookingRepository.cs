using DeepDive.Models;
using DeepDive.ViewModels;
using System.Runtime.CompilerServices;

namespace DeepDive.Repository
{
    public static class BookingRepository
    {
        public static Booking booking;

        public static void AddProductToBooking(int id)
        {
            if (booking == null)
            {
                booking = new Booking { BookingId = id };
            }
            booking.BookedProducts.Add(id);
        }


    }
}