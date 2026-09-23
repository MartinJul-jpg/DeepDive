using DeepDive.Data;
using DeepDive.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DeepDive.Repository
{
    public class BasketRepository
    {
        private string _userId { get; set; }
        private Booking _basket
        {
            get 
            {
                Booking basket = NonFinalizedBasket();

                return basket;
            }
            set 
            {
                Booking basket = NonFinalizedBasket();

                basket = value; 
            }
        }

        private readonly ProductRepository _productRepository;
        private readonly DeepDiveContext _deepDiveContext;

        public BasketRepository(DeepDiveContext deepDiveContext, ProductRepository productRepository)
        {
            _productRepository = productRepository;
            _deepDiveContext = deepDiveContext;
        }

        public Booking GetBasket(string userId)
        {
            _userId = userId;

            return _basket;
        }

        public void UpdateBasketDates(Booking basket, string userId)
        {
            _userId = userId;

            _basket.Start = basket.Start;
            _basket.End = basket.End;
            _basket.ExceedsStock = basket.ExceedsStock;

            _deepDiveContext.SaveChanges();
        }

        public void UpdateBasketProducts(Booking basket, string userId)
        {
            _userId = userId;

        }

        public void UpdateBasketOverlap(bool exceedsStock, string userId)
        {
            _userId = userId;

            _basket.ExceedsStock = exceedsStock;

            _deepDiveContext.SaveChanges();
        } 

        public void UpdateBasketAdminPage(Booking booking)
        {
            _deepDiveContext.Bookings.Update(booking);
            _deepDiveContext.SaveChanges();
        }

        public void clearBasket(string userId)
        {
            _userId = userId;

            _deepDiveContext.Bookings.Remove(_basket);
            _deepDiveContext.SaveChanges();
        }
       

        public void ResetBasket(string userId)
        {
            _userId = userId;

            _deepDiveContext.Bookings.Add(new Booking() { Finalized = false, UserId = userId });

            _basket.Start = DateOnly.FromDateTime(DateTime.Today);
            _basket.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            _deepDiveContext.SaveChanges();
        }

        public void AddProductToBasket(int id, string userId)
        {
            _userId = userId;

            //this if needed anymore? 
            if (_basket == null)
            {
                ResetBasket(userId);
            }


            _basket.Product_Bookings.Add(new Product_Booking()
            {
                ProductId = id,
                Product = _productRepository.GetProductById(id),
                Booking = _basket
            });

            _basket.CalculatePriceOfBooking();

            _deepDiveContext.SaveChanges();
        }

        public void ValidateBookingOverlap(Booking basket, string userId)
        {
            _userId = userId;
            
            //getting basketDates<DateOnly>
            //is filled with the dates that exist within baskets timespan
            List<DateOnly> basketDates = new List<DateOnly>();
            for (int days = 0; days < basket.DurationInDays + 1; days++)
            {
                basketDates.Add(basket.Start.AddDays(days));
            }

            //getting basketProductIds<int>
            //is filled with the productId's that exist within the basket
            List<int> basketProductIds = new List<int>();
            foreach (Product_Booking pb in basket.Product_Bookings)
            {
                if (!basketProductIds.Contains(pb.ProductId))
                {
                    basketProductIds.Add(pb.ProductId);
                }
            }

            //getting basketProductIdsTuple<(int, int)>
            //is filled with tuple pairs that represent the productId's and the amounts that are within the basket
            List<(int, int)> basketProductIdsTuple = new List<(int, int)>();
            foreach (int productId in basketProductIds)
            {
                int basketAmount = basket.Product_Bookings.Where(pb => pb.ProductId == productId).ToList().Count();
                basketProductIdsTuple.Add((productId, basketAmount));
            }

            //getting Bookings<Booking>
            //gets all finalized Bookings that exist in the database
            List<Booking> bookings = _deepDiveContext.Bookings
                .Where(b => b.Finalized == true)
                .Include(b => b.Product_Bookings)
                .ToList();

            //getting overlapsId<Booking>
            //is filled with the Bookings that have any productId that exists in the basket
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
            //tuple pairs of those Bookings and their dates
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
                            UpdateBasketOverlap(true, _userId);
                            return;
                        }
                    }
                }
            }

            //in case no overlapping Bookings are detected, a check is required anyway,
            //checks that store capacity isnt exceeded by cart, in general
            foreach ((int, int) basketProductIdTuple in basketProductIdsTuple)
            {
                int basketProductId = basketProductIdTuple.Item1;
                int basketProductIdAmount = basketProductIdTuple.Item2;

                int amount = _productRepository.GetProductById(basketProductId).Amount;

                if (amount < basketProductIdAmount)
                {
                    UpdateBasketOverlap(true, _userId);
                    return;
                }
            }

            //cart items did not exceed remaining stock, at any point
            UpdateBasketOverlap(false, _userId);
            return;
        }

        private Booking NonFinalizedBasket()
        {
            Booking basket;

            if (_deepDiveContext.Bookings
                    .Where(b => b.UserId == _userId)
                    .Where(b => b.Finalized == false)
                    .Include(b => b.Product_Bookings)
                    .ThenInclude(b => b.Product).IsNullOrEmpty())
            {
                Booking booking = new Booking() { Finalized = false, UserId = _userId };

                booking.Start = DateOnly.FromDateTime(DateTime.Today);
                booking.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

                _deepDiveContext.Bookings.Add(booking);

                _deepDiveContext.SaveChanges();
            }

            basket = _deepDiveContext.Bookings
                    .Where(b => b.UserId == _userId)
                    .Where(b => b.Finalized == false)
                    .Include(b => b.Product_Bookings)
                    .ThenInclude(b => b.Product)
                    .ToList()
                    .First();

            return basket;
        }

        //Lived in BookingRepository before
        public void AddBooking(Booking basket)
        {
            Booking booking = _basket;
            booking.Start = basket.Start;
            booking.End = basket.End;
            booking.Finalized = true;
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
                .Where(b => b.Finalized == true)
                .Include(b => b.Product_Bookings)
                .ThenInclude(pb => pb.Product)
                .Include(u => u.User)
                .ToList();
            return bookings;
        }

        public Booking? GetBookingById(int id)
        {
            Booking booking = _deepDiveContext.Bookings
                .Include(b => b.Product_Bookings)
                .ThenInclude(pb => pb.Product)
                .Include(ui => ui.User)
                .Where(booking => booking.BookingId == id)
                .ToList()
                .First();

            return booking;
        } 

       
    }
}
