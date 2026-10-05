using DiveDeepAPI.Data;
using DiveDeepAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace DiveDeepAPI.Repository
{
    public class CartRepository : ICartRepository 
    {

        private readonly DiveDeepContext _diveDeepContext;

        public CartRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }
        
        public async Task<Booking?> GetCart(string UserId)
        {
            var cart = await _diveDeepContext.Bookings
                .Include(p => p.Product_Bookings)
                .ThenInclude(p => p.Product).
                SingleOrDefaultAsync(u => u.UserId == UserId && u.Finalized == false);
            return cart;
        }
    }
}
