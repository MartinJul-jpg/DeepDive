//using DiveDeepAPI.Data;
//using DiveDeepAPI.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//
//namespace DiveDeepAPI.Repository
//{
//    public class CartRepository
//    {
//        private string _userId { get; set; }
//        private Booking _cart
//        {
//            get 
//            {
//                Booking cart = NonFinalizedcart();
//
//                return cart;
//            }
//            set 
//            {
//                Booking cart = NonFinalizedcart();
//
//                cart = value; 
//            }
//        }
//
//        private readonly ProductRepository _productRepository;
//        private readonly DiveDeepContext _diveDeepContext;
//
//        public CartRepository(DiveDeepContext deepDiveContext, ProductRepository productRepository)
//        {
//            _productRepository = productRepository;
//            _diveDeepContext = deepDiveContext;
//        }
//        public Booking GetCart(string userId)
//        {
//            _userId = userId;
//
//            return _cart;
//        }
//
//        public void UpdateCartDates(Booking cart, string userId)
//        {
//            _userId = userId;
//
//            _cart.Start = cart.Start;
//            _cart.End = cart.End;
//            _cart.ExceedsStock = cart.ExceedsStock;
//
//            _diveDeepContext.SaveChanges();
//        }
//
//        public void UpdateCartProducts(Booking cart, string userId)
//        {
//            _userId = userId;
//
//        }
//
//        public void UpdateCartOverlap(bool exceedsStock, string userId)
//        {
//            _userId = userId;
//
//            _cart.ExceedsStock = exceedsStock;
//
//            _diveDeepContext.SaveChanges();
//        } 
//
//        public void UpdateCartFromAdminPage(Booking booking)
//        {
//            _diveDeepContext.Bookings.Update(booking);
//            _diveDeepContext.SaveChanges();
//        }
//
//        public void ClearCart(string userId)
//        {
//            _userId = userId;
//
//            _diveDeepContext.Bookings.Remove(_cart);
//            _diveDeepContext.SaveChanges();
//        }
//       
//
//        public void ResetCart(string userId)
//        {
//            _userId = userId;
//
//            _diveDeepContext.Bookings.Add(new Booking() { Finalized = false, UserId = userId });
//
//            _cart.Start = DateOnly.FromDateTime(DateTime.Today);
//            _cart.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
//
//            _diveDeepContext.SaveChanges();
//        }
//
//        public void AddProductToCart(int id, string userId)
//        {
//            _userId = userId;
//
//            //this if needed anymore? 
//            if (_cart == null)
//            {
//                ResetCart(userId);
//            }
//
//
//            _cart.Product_Bookings.Add(new Product_Booking()
//            {
//                ProductId = id,
//                Product =  _productRepository.GetProductById(id),
//                Booking = _cart
//            });
//
//            _cart.CalculatePriceOfBooking();
//
//            _diveDeepContext.SaveChanges();
//        }
//
//        public void ValidateBookingOverlap(Booking cart, string userId)
//        {
//            _userId = userId;
//            
//            //getting cartDates<DateOnly>
//            //is filled with the dates that exist within carts timespan
//            List<DateOnly> cartDates = new List<DateOnly>();
//            for (int days = 0; days < cart.DurationInDays + 1; days++)
//            {
//                cartDates.Add(cart.Start.AddDays(days));
//            }
//
//            //getting cartProductIds<int>
//            //is filled with the productId's that exist within the cart
//            List<int> cartProductIds = new List<int>();
//            foreach (Product_Booking pb in cart.Product_Bookings)
//            {
//                if (!cartProductIds.Contains(pb.ProductId))
//                {
//                    cartProductIds.Add(pb.ProductId);
//                }
//            }
//
//            //getting cartProductIdsTuple<(int, int)>
//            //is filled with tuple pairs that represent the productId's and the amounts that are within the cart
//            List<(int, int)> cartProductIdsTuple = new List<(int, int)>();
//            foreach (int productId in cartProductIds)
//            {
//                int cartAmount = cart.Product_Bookings.Where(pb => pb.ProductId == productId).ToList().Count();
//                cartProductIdsTuple.Add((productId, cartAmount));
//            }
//
//            //getting Bookings<Booking>
//            //gets all finalized Bookings that exist in the database
//            List<Booking> bookings = _diveDeepContext.Bookings
//                .Where(b => b.Finalized == true)
//                .Include(b => b.Product_Bookings)
//                .ToList();
//
//            //getting overlapsId<Booking>
//            //is filled with the Bookings that have any productId that exists in the cart
//            List<Booking> overlapsId = new List<Booking>();
//            foreach (Booking booking in bookings)
//            {
//                bool foundInBooking = false;
//                foreach (Product_Booking pb in booking.Product_Bookings)
//                {
//                    if (cartProductIds.Contains(pb.ProductId))
//                    {
//                        foundInBooking = true;
//                    }
//                }
//                if (foundInBooking)
//                {
//                    overlapsId.Add(booking);
//                }
//            }
//
//            //getting overlapsIdTuple<(Booking, List<DateOnly>)>
//            //tuple pairs of those Bookings and their dates
//            List<(Booking, List<DateOnly>)> overlapsIdTuples = new List<(Booking, List<DateOnly>)>();
//            foreach (Booking booking in overlapsId)
//            {
//                int durationInDays = booking.End.DayNumber - booking.Start.DayNumber;
//
//                List<DateOnly> bookingDates = new List<DateOnly>();
//                for (int days = 0; days < durationInDays + 1; days++)
//                {
//                    bookingDates.Add(booking.Start.AddDays(days));
//                }
//
//                overlapsIdTuples.Add((booking, new List<DateOnly>(bookingDates)));
//            }
//
//            //getting overlapscartDate_List<List<Booking>>
//            List<List<Booking>> overlapscartDate_List = new List<List<Booking>>();
//            foreach (DateOnly cartDate in cartDates)
//            {
//                List<Booking> overlapscartDate = new List<Booking>();
//                foreach ((Booking, List<DateOnly>) bookingTuple in overlapsIdTuples)
//                {
//                    bool foundInBookingTuple = false;
//                    foreach (DateOnly bookingDate in bookingTuple.Item2)
//                    {
//                        if (bookingDate == cartDate)
//                        {
//                            foundInBookingTuple = true;
//                        }
//                    }
//                    if (foundInBookingTuple)
//                    {
//                        overlapscartDate.Add(bookingTuple.Item1);
//                    }
//                }
//
//                overlapscartDate_List.Add(overlapscartDate);
//            }
//
//            //getting overlapscartDate_pbs_List<List<Product_Booking>>
//            List<List<Product_Booking>> overlapscartDate_pbs_List = new List<List<Product_Booking>>();
//            foreach (List<Booking> booking_List in overlapscartDate_List)
//            {
//                List<Product_Booking> overlapscartDate_pbs = new List<Product_Booking>();
//                foreach (Booking booking in booking_List)
//                {
//                    foreach (Product_Booking pb in booking.Product_Bookings)
//                    {
//                        overlapscartDate_pbs.Add(pb);
//                    }
//                }
//                overlapscartDate_pbs_List.Add(overlapscartDate_pbs);
//            }
//
//            //check that store capacity isnt exceeded by cart + booking, per date
//            foreach (List<Product_Booking> pbs in overlapscartDate_pbs_List)
//            {
//                foreach ((int, int) cartProductIdTuple in cartProductIdsTuple)
//                {
//                    int cartProductId = cartProductIdTuple.Item1;
//                    int cartProductIdAmount = cartProductIdTuple.Item2;
//
//                    int amount = _productRepository.GetProductById(cartProductId).Amount;
//                    foreach (var item in pbs.Where(pb => pb.ProductId == cartProductId))
//                    {
//                        //in here only pbs with the correct productId are present
//                        amount -= 1;
//
//                        if (amount < cartProductIdAmount)
//                        {
//                            UpdateCartOverlap(true, _userId);
//                            return;
//                        }
//                    }
//                }
//            }
//
//            //in case no overlapping Bookings are detected, a check is required anyway,
//            //checks that store capacity isnt exceeded by cart, in general
//            foreach ((int, int) cartProductIdTuple in cartProductIdsTuple)
//            {
//                int cartProductId = cartProductIdTuple.Item1;
//                int cartProductIdAmount = cartProductIdTuple.Item2;
//
//                int amount = _productRepository.GetProductById(cartProductId).Amount;
//
//                if (amount < cartProductIdAmount)
//                {
//                    UpdateCartOverlap(true, _userId);
//                    return;
//                }
//            }
//
//            //cart items did not exceed remaining stock, at any point
//            UpdateCartOverlap(false, _userId);
//            return;
//        }
//
//        private Booking NonFinalizedcart()
//        {
//            Booking cart;
//
//            if (_diveDeepContext.Bookings
//                    .Where(b => b.UserId == _userId)
//                    .Where(b => b.Finalized == false)
//                    .Include(b => b.Product_Bookings)
//                    .ThenInclude(b => b.Product).IsNullOrEmpty())
//            {
//                Booking booking = new Booking() { Finalized = false, UserId = _userId };
//
//                booking.Start = DateOnly.FromDateTime(DateTime.Today);
//                booking.End = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
//
//                _diveDeepContext.Bookings.Add(booking);
//
//                _diveDeepContext.SaveChanges();
//            }
//
//            cart = _diveDeepContext.Bookings
//                    .Where(b => b.UserId == _userId)
//                    .Where(b => b.Finalized == false)
//                    .Include(b => b.Product_Bookings)
//                    .ThenInclude(b => b.Product)
//                    .ToList()
//                    .First();
//
//            return cart;
//        }
//
//        //Lived in BookingRepository before
//        public void AddBooking(Booking cart)
//        {
//            Booking booking = _cart;
//            booking.Start = cart.Start;
//            booking.End = cart.End;
//            booking.Finalized = true;
//            _diveDeepContext.SaveChanges();
//
//            foreach (Product_Booking pb in cart.Product_Bookings)
//            {
//                pb.Product = null;
//                pb.Booking = null;
//                pb.BookingId = booking.BookingId;
//                _diveDeepContext.Product_Bookings.Add(pb);
//            }
//            _diveDeepContext.SaveChanges();
//        }
//
//        public List<Booking> GetAllBookings()
//        {
//            List<Booking> bookings = _diveDeepContext.Bookings
//                .Where(b => b.Finalized == true)
//                .Include(b => b.Product_Bookings)
//                .ThenInclude(pb => pb.Product)
//                .Include(u => u.User)
//                .ToList();
//            return bookings;
//        }
//
//        public Booking? GetBookingById(int id)
//        {
//            Booking booking = _diveDeepContext.Bookings
//                .Include(b => b.Product_Bookings)
//                .ThenInclude(pb => pb.Product)
//                .Include(ui => ui.User)
//                .Where(booking => booking.BookingId == id)
//                .ToList()
//                .First();
//
//            return booking;
//        }  
//
//        public void DeleteBookingAdmin(int id)
//        {
//            var foundbooking = _diveDeepContext.Bookings.Find(id);
//            if (foundbooking != null)
//            {
//                _diveDeepContext.Bookings.Remove(foundbooking);
//                _diveDeepContext.SaveChanges();
//            }
//        }
//
//       
//    }
//}
//