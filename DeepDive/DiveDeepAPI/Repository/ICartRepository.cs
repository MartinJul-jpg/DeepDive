using DiveDeepAPI.Models;

namespace DiveDeepAPI.Repository
{
    public interface ICartRepository
    {
        Task<Booking?> GetCart(string UserId);

        Task<List<Booking>> GetConfirmedBookigns(string UserId);

        Task<List<Booking>> GetAllConfirmedBookigns();

        Task AddNewCart(Booking booking);

        Task<bool> FinalizedCart(string UserId);

        Task<bool> ValidateBookingOverlap(Booking cart, string userId);

        Task<bool> DeleteBooking(int bookingId);

        Task UpdateCartBooking(Booking oldBooking, Booking updateBooking);
    }
}
