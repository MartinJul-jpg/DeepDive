using DeepDive.Models;
using DeepDive.ViewModels;
using System.Runtime.CompilerServices;

namespace DeepDive.Repository
{
    public static class BookingRepository
    {
        private static Booking _basket;
        private static List<Booking> _bookings = new List<Booking>();

        public static Booking GetBasket()
        {
            return _basket;
        }

        public static void UpdateBasket(Booking basket)
        {
            _basket = basket;
        }

        public static void AddProductToBasket(int id)
        {
            if (_basket == null)
            {
                _basket = new Booking();
            }

            _basket.BookedProducts.Add(id);
            _basket.Start = DateOnly.FromDateTime(DateTime.Today);
            _basket.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        }

        public static void ClearBasket()
        {
            _basket = null;
        }

        public static void AddBooking(Booking basket)
        {
            Booking booking = new Booking();

            booking.BookingId = _bookings.Any() ? _bookings.Max(x => x.BookingId) + 1 : 1;
            booking.BookedProducts = basket.BookedProducts;
            booking.Start = basket.Start;
            booking.End = basket.End;

            _bookings.Add(booking);
        }
    }
}