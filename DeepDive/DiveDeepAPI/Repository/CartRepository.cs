using DiveDeepAPI.Data;
using DiveDeepAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DiveDeepAPI.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly DiveDeepContext _diveDeepContext;
        private readonly IProductRepository _productRepository;

        public CartRepository(DiveDeepContext diveDeepContext, IProductRepository productRepository)
        {
            _diveDeepContext = diveDeepContext;
            _productRepository = productRepository;
        }
        public async Task<Booking?> GetCart(string UserId)
        {
            var cart = await _diveDeepContext.Bookings
                .Include(p => p.Product_Bookings)
                .ThenInclude(p => p.Product).
                SingleOrDefaultAsync(u => u.UserId == UserId && u.Finalized == false);
            return cart;
        }

        public async Task<List<Booking>> GetConfirmedBookings(string UserId)
        {
            var bookings = await _diveDeepContext.Bookings.Include(p => p.Product_Bookings)
                .ThenInclude(p => p.Product).Where(u => u.UserId == UserId && u.Finalized == true).
                ToListAsync();
            return bookings;
        }

        public async Task<List<Booking>> GetAllConfirmedBookigns()
        {
            var bookings = await _diveDeepContext.Bookings.Include(p => p.Product_Bookings)
                .ThenInclude(p => p.Product).Where(u => u.Finalized == true).
                ToListAsync();
            return bookings;
        }

        public async Task AddNewCart(Booking booking)
        {
            await _diveDeepContext.Bookings.AddAsync(booking);
            await _diveDeepContext.SaveChangesAsync();
        }

        public async Task<bool> FinalizedCart(string UserId)
        {
            var booking = await _diveDeepContext.Bookings
               .Include(p => p.Product_Bookings)
               .ThenInclude(p => p.Product)
               .SingleOrDefaultAsync(u => u.UserId == UserId && u.Finalized == false);

            if (booking.ExceedsStock == true) return false;

            booking.Finalized = true;
            _diveDeepContext.Update(booking);
            await _diveDeepContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ValidateBookingOverlap(string userId)
        {
            Booking cart = await GetCart(userId);

            //getting cartDates<DateOnly>
            //is filled with the dates that exist within carts timespan
            List<DateOnly> cartDates = new List<DateOnly>();
            for (int days = 0; days < cart.DurationInDays + 1; days++)
            {
                cartDates.Add(cart.Start.AddDays(days));
            }

            //getting cartProductIds<int>
            //is filled with the productId's that exist within the cart
            List<int> cartProductIds = new List<int>();
            foreach (Product_Booking pb in cart.Product_Bookings)
            {
                if (!cartProductIds.Contains(pb.ProductId))
                {
                    cartProductIds.Add(pb.ProductId);
                }
            }

            //getting cartProductIdsTuple<(int, int)>
            //is filled with tuple pairs that represent the productId's and the amounts that are within the cart
            List<(int, int)> cartProductIdsTuple = new List<(int, int)>();
            foreach (int productId in cartProductIds)
            {
                int cartAmount = cart.Product_Bookings.Where(pb => pb.ProductId == productId).ToList().Count();
                cartProductIdsTuple.Add((productId, cartAmount));
            }

            //getting Bookings<Booking>
            //gets all finalized Bookings that exist in the database
            List<Booking> bookings = _diveDeepContext.Bookings
                .Where(b => b.Finalized == true)
                .Include(b => b.Product_Bookings)
                .ToList();

            //getting overlapsId<Booking>
            //is filled with the Bookings that have any productId that exists in the cart
            List<Booking> overlapsId = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                bool foundInBooking = false;
                foreach (Product_Booking pb in booking.Product_Bookings)
                {
                    if (cartProductIds.Contains(pb.ProductId))
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

            //getting overlapscartDate_List<List<Booking>>
            List<List<Booking>> overlapscartDate_List = new List<List<Booking>>();
            foreach (DateOnly cartDate in cartDates)
            {
                List<Booking> overlapscartDate = new List<Booking>();
                foreach ((Booking, List<DateOnly>) bookingTuple in overlapsIdTuples)
                {
                    bool foundInBookingTuple = false;
                    foreach (DateOnly bookingDate in bookingTuple.Item2)
                    {
                        if (bookingDate == cartDate)
                        {
                            foundInBookingTuple = true;
                        }
                    }
                    if (foundInBookingTuple)
                    {
                        overlapscartDate.Add(bookingTuple.Item1);
                    }
                }

                overlapscartDate_List.Add(overlapscartDate);
            }

            //getting overlapscartDate_pbs_List<List<Product_Booking>>
            List<List<Product_Booking>> overlapscartDate_pbs_List = new List<List<Product_Booking>>();
            foreach (List<Booking> booking_List in overlapscartDate_List)
            {
                List<Product_Booking> overlapscartDate_pbs = new List<Product_Booking>();
                foreach (Booking booking in booking_List)
                {
                    foreach (Product_Booking pb in booking.Product_Bookings)
                    {
                        overlapscartDate_pbs.Add(pb);
                    }
                }
                overlapscartDate_pbs_List.Add(overlapscartDate_pbs);
            }

            //check that store capacity isnt exceeded by cart + booking, per date
            foreach (List<Product_Booking> pbs in overlapscartDate_pbs_List)
            {
                foreach ((int, int) cartProductIdTuple in cartProductIdsTuple)
                {
                    int cartProductId = cartProductIdTuple.Item1;
                    int cartProductIdAmount = cartProductIdTuple.Item2;

                    int amount = (await _productRepository.GetProductById(cartProductId)).Amount;
                    foreach (var item in pbs.Where(pb => pb.ProductId == cartProductId))
                    {
                        //in here only pbs with the correct productId are present
                        amount -= 1;

                        if (amount < cartProductIdAmount)
                        {
                            return true;
                        }
                    }
                }
            }
            //in case no overlapping Bookings are detected, a check is required anyway,
            //checks that store capacity isnt exceeded by cart, in general
            foreach ((int, int) cartProductIdTuple in cartProductIdsTuple)
            {
                int cartProductId = cartProductIdTuple.Item1;
                int cartProductIdAmount = cartProductIdTuple.Item2;

                int amount = (await _productRepository.GetProductById(cartProductId)).Amount;

                if (amount < cartProductIdAmount)
                {
                    return true;
                }
            }
            //cart items did not exceed remaining stock, at any point
            return false;
        }

        public async Task<bool> DeleteBooking(int bookingId)
        {
            var booking = _diveDeepContext.Bookings.Find(bookingId);
            if (booking != null && booking.Finalized == true)
            {
                _diveDeepContext.Bookings.Remove(booking);
                await _diveDeepContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task UpdateCartBooking(Booking oldBooking,Booking updateBooking)
        {
            _diveDeepContext.Entry(oldBooking).CurrentValues.SetValues(updateBooking);

            await _diveDeepContext.SaveChangesAsync();
        }

    }
}
