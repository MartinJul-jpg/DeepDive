using DeepDive.Data;
using DeepDive.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DeepDive.Repository
{
    public class BookingRepository
    {
        private ProductRepository _productRepository;
        private readonly DeepDiveContext _deepDiveContext;

        public BookingRepository(DeepDiveContext deepDiveContext, ProductRepository productRepository)
        {
            _deepDiveContext = deepDiveContext;
            _productRepository = productRepository;
        }

        public void AddBooking(Booking basket)
        {
            Booking booking = new Booking();
            booking.BookingId = 0;
            booking.Start = basket.Start;
            booking.End = basket.End;
            _deepDiveContext.Bookings.Add(booking);
            _deepDiveContext.SaveChanges();

            foreach (Product_Booking pb in basket.Product_Bookings)
            {
                pb.Product = null;
                pb.Booking = null;
                pb.BookingId = booking.BookingId;
                _deepDiveContext.Product_Bookings.Add(pb);
            }
            _deepDiveContext.SaveChanges();
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = _deepDiveContext.Bookings
                .Include(b => b.Product_Bookings)
                .ThenInclude(pb => pb.Product).ToList();
            return bookings;
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

        public bool ValidateBookingOverlap(Booking basket)
        {
            //getting basketDates<DateOnly>
            List<DateOnly> basketDates = new List<DateOnly>();
            for (int days = 0; days < basket.DurationInDays + 1; days++)
            {
                basketDates.Add(basket.Start.AddDays(days));
            }

            //getting basketProductIds<int>
            List<int> basketProductIds = new List<int>();
            foreach (Product_Booking pb in basket.Product_Bookings)
            {
                if (!basketProductIds.Contains(pb.ProductId))
                {
                    basketProductIds.Add(pb.ProductId);
                }
            }

            //getting basketProductIdsTuple<(int, int)>
            List<(int, int)> basketProductIdsTuple = new List<(int, int)>();
            foreach (int productId in basketProductIds)
            {
                int basketAmount = basket.Product_Bookings.Where(pb => pb.ProductId == productId).ToList().Count();
                basketProductIdsTuple.Add((productId, basketAmount));
            }

            //getting bookings<Booking>
            List <Booking> bookings = _deepDiveContext.Bookings
                .Include(b => b.Product_Bookings)
                .ToList();

            //getting overlapsId<Booking>
            List<Booking> overlapsId = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                bool foundInBooking = false;
                foreach (Product_Booking pb in booking.Product_Bookings)
                {
                    if (basketProductIds.Contains(pb.ProductId))
                    {
                        foundInBooking = true;
                    }
                }
                if (foundInBooking)
                {
                    overlapsId.Add(booking);
                }
            }

            //getting overlapsIdTuple<(Booking, List<DateOnly>)>
            List<(Booking, List<DateOnly>)> overlapsIdTuple = new List<(Booking, List<DateOnly>)>();
            foreach (Booking booking in overlapsId)
            {
                int durationInDays = booking.End.DayNumber - booking.Start.DayNumber;

                List<DateOnly> bookingDates = new List<DateOnly>();
                for (int days = 0; days < durationInDays + 1; days++)
                {
                    bookingDates.Add(booking.Start.AddDays(days));
                }

                overlapsIdTuple.Add((booking, new List<DateOnly>(bookingDates)));
            }

            //getting overlaps<Booking>
            List<Booking> overlaps = new List<Booking>();
            foreach ((Booking, List<DateOnly>) bookingTuple in overlapsIdTuple)
            {
                bool foundInTuple = false;
                foreach (DateOnly bookingDate in bookingTuple.Item2)
                {
                    foreach (DateOnly basketDate in basketDates)
                    {
                        if (bookingDate == basketDate)
                        {
                            foundInTuple = true;
                        }
                    }
                }
                if (foundInTuple)
                {
                    overlaps.Add(bookingTuple.Item1);
                }
            }

            //getting overlaps_pbs<Product_Booking>
            List<Product_Booking> overlaps_pbs = new List<Product_Booking>();
            foreach (Booking booking in overlaps)
            {
                foreach (Product_Booking pb in booking.Product_Bookings)
                {
                    overlaps_pbs.Add(pb);
                }
            }

            //final stuff
            foreach ((int, int) basketProductIdTuple in basketProductIdsTuple)
            {
                int basketProductId = basketProductIdTuple.Item1;
                int basketProductIdAmount = basketProductIdTuple.Item2;

                int amount = _productRepository.GetProductById(basketProductId).Amount;
                foreach (var item in overlaps_pbs.Where(pb => pb.ProductId == basketProductId))
                {
                    //in here only pbs with the correct productId are present
                    amount -= 1;

                    if (amount < basketProductIdAmount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}