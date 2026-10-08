using Shared.Models;

namespace DiveDeepAPI.Repository
{
    public interface ICartRepository
    {
        Task<Booking?> GetCart(string UserId);
        Task<Booking?> GetBookingById(int bookingId);

        Task<List<Booking>> GetConfirmedBookings(string UserId);

        Task<List<Booking>> GetAllConfirmedBookigns();

        Task AddNewCart(Booking booking);

        Task<bool> FinalizedCart(string UserId);

        Task<bool> ValidateBookingOverlap(string userId);

        Task<bool> DeleteBooking(int bookingId);

        Task UpdateCartBooking(Booking updateBooking);
    }
}
