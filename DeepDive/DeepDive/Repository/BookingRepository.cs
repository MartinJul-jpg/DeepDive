using DeepDive.Data;
using DeepDive.Models;
using DeepDive.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DeepDive.Repository
{
    public class BookingRepository
    {
        private Booking _basket;

        private ProductRepository _productRepository;
        private DeepDiveContext _deepDiveContext;

        public BookingRepository(DeepDiveContext deepDiveContext, ProductRepository productRepository)
        {
            _deepDiveContext = deepDiveContext;
            _productRepository = productRepository;

        }

        public Booking GetBasket()
        {
            return _basket;
        }

        public void UpdateBasket(Booking basket)
        {
            
            _basket = basket;
        }

        public void AddProductToBasket(int id)
        {
            if (_basket == null)
            {
                _basket = new Booking();
            }

            _basket.Product_Bookings.Add(new Product_Booking()
            {
                Product = _productRepository.GetProductById(id),
                Booking = _basket
            });
            _basket.Start = DateOnly.FromDateTime(DateTime.Today);
            _basket.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        }

        public void ClearBasket()
        {
            _basket = null;
        }

        public void AddBooking(Booking basket)
        {
            Booking booking = new Booking();

            booking.Product_Bookings = basket.Product_Bookings;
            booking.Start = basket.Start;
            booking.End = basket.End;

            _deepDiveContext.Bookings.Add(booking);
            _deepDiveContext.SaveChanges();
        } 

        public Booking? GetBookingById(int id)
        {
            Booking booking = _deepDiveContext.Bookings
                .Include(b => b.Product_Bookings)
                .ThenInclude(pb => pb.Product)
                .Where(booking => booking.BookingId == id)
                .ToList()
                .First();


            return booking;
        }

        public bool ValidateBasketOverlap(Booking basket)
        {
            List<DateOnly> dates = new List<DateOnly>();
            for (int days = 0; days < basket.DurationInDays; days++)
            {
                dates.Add(basket.Start.AddDays(days));
            }

            List<Booking> allBookings = new List<Booking>(_deepDiveContext.Bookings.Include(b => b.Product_Bookings).ThenInclude(pb => pb.Product));

            List<Booking> overlappingBookings = new List<Booking>();
            foreach (DateOnly date in dates)
            {
                foreach (Booking booking in allBookings)
                {
                    List<DateOnly> bookingDates = new List<DateOnly>();
                    for (int days = 0; days < booking.DurationInDays; days++)
                    {
                        bookingDates.Add(booking.Start.AddDays(days));
                    }

                    foreach (DateOnly bookingDate in bookingDates)
                    {
                        if (bookingDate == date && overlappingBookings.Contains(booking))
                        {
                            overlappingBookings.Add(booking);
                        }
                    }
                }
            }

            List<Product_Booking> overlappingProduct_bookings = new List<Product_Booking>();
            foreach (Booking booking in overlappingBookings)
            {
                foreach (Product_Booking product_booking in booking.Product_Bookings)
                {
                    overlappingProduct_bookings.Add(product_booking);
                }
            }

            List<int> overlappingProductIds = new List<int>();
            foreach (Product_Booking overlappingProduct_booking in overlappingProduct_bookings)
            {
                if (overlappingProductIds.Contains(overlappingProduct_booking.ProductId))
                {
                    overlappingProductIds.Add(overlappingProduct_booking.ProductId);
                }
            }

            foreach (int overlappingProductId in overlappingProductIds)
            {
                int basketAmount = 0;
                foreach (Product_Booking product_booking in basket.Product_Bookings)
                {
                    if (product_booking.ProductId == overlappingProductId)
                    {
                        basketAmount += 1;
                    }
                }

                int amount = _productRepository.GetProductById(overlappingProductId).Amount;
                foreach (Product_Booking overlappingProduct_booking in overlappingProduct_bookings)
                {
                    if (overlappingProduct_booking.ProductId == overlappingProductId)
                    {
                        amount -= 1;
                    }

                    if (amount < basketAmount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}