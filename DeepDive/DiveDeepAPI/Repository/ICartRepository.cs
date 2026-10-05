using DiveDeepAPI.Models;

namespace DiveDeepAPI.Repository
{
    public interface ICartRepository
    {
        Task<Booking?> GetCart(string UserId);
    }
}
