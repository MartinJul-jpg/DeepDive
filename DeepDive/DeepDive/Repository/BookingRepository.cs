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
            List<(Booking, List<DateOnly>)> overlapsIdTuples = new List<(Booking, List<DateOnly>)>();
            foreach (Booking booking in overlapsId)
            {
                int durationInDays = booking.End.DayNumber - booking.Start.DayNumber;

                List<DateOnly> bookingDates = new List<DateOnly>();
                for (int days = 0; days < durationInDays + 1; days++)
                {
                    bookingDates.Add(booking.Start.AddDays(days));
                }

                overlapsIdTuples.Add((booking, new List<DateOnly>(bookingDates)));
            }

            //getting overlapsBasketDate_List<List<Booking>>
            List<List<Booking>> overlapsBasketDate_List = new List<List<Booking>>();
            foreach (DateOnly basketDate in basketDates)
            {
                List<Booking> overlapsBasketDate = new List<Booking>();
                foreach ((Booking, List<DateOnly>) bookingTuple in overlapsIdTuples)
                {
                    bool foundInBookingTuple = false;
                    foreach (DateOnly bookingDate in bookingTuple.Item2)
                    {
                        if (bookingDate == basketDate)
                        {
                            foundInBookingTuple = true;
                        }
                    }
                    if (foundInBookingTuple)
                    {
                        overlapsBasketDate.Add(bookingTuple.Item1);
                    }
                }

                overlapsBasketDate_List.Add(overlapsBasketDate);
            }

            //getting overlapsBasketDate_pbs_List<List<Product_Booking>>
            List<List<Product_Booking>> overlapsBasketDate_pbs_List = new List<List<Product_Booking>>();
            foreach (List<Booking> booking_List in overlapsBasketDate_List)
            {
                List<Product_Booking> overlapsBasketDate_pbs = new List<Product_Booking>();
                foreach (Booking booking in booking_List)
                {
                    foreach (Product_Booking pb in booking.Product_Bookings)
                    {
                        overlapsBasketDate_pbs.Add(pb);
                    }
                }
                overlapsBasketDate_pbs_List.Add(overlapsBasketDate_pbs);
            }

            //check that store capacity isnt exceeded by cart + booking, per date
            foreach (List<Product_Booking> pbs in overlapsBasketDate_pbs_List)
            {
                foreach ((int, int) basketProductIdTuple in basketProductIdsTuple)
                {
                    int basketProductId = basketProductIdTuple.Item1;
                    int basketProductIdAmount = basketProductIdTuple.Item2;

                    int amount = _productRepository.GetProductById(basketProductId).Amount;
                    foreach (var item in pbs.Where(pb => pb.ProductId == basketProductId))
                    {
                        //in here only pbs with the correct productId are present
                        amount -= 1;

                        if (amount < basketProductIdAmount)
                        {
                            return false;
                        }
                    }
                }
            }

            //in case no overlapping bookings are detected, a check is required anyway,
            //checks that store capacity isnt exceeded by cart, in general
            foreach ((int, int) basketProductIdTuple in basketProductIdsTuple)
            {
                int basketProductId = basketProductIdTuple.Item1;
                int basketProductIdAmount = basketProductIdTuple.Item2;

                int amount = _productRepository.GetProductById(basketProductId).Amount;

                if (amount < basketProductIdAmount)
                {
                    return false;
                }
            }

            //cart items did not exceed remaining stock, at any point
            return true;
        }
    }
}