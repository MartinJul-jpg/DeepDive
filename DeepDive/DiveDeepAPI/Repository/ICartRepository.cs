using DiveDeepAPI.Models;

namespace DiveDeepAPI.Repository
{
    public interface ICartRepository
    {
        Task<Booking?> GetCart(string UserId);

        Task<List<Booking>> GetConfirmedBookigns(string UserId);

        Task<List<Booking>> GetAllConfirmedBookigns();

        Task AddNewCart(Booking booking);

        Task FinalizedBooking(Booking booking);

        Task<bool> ValidateBookingOverlap(Booking cart, string userId);
    }
}
