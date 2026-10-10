using Shared.Models;

namespace DiveDeepAPI.Repository
{
    public interface ICartRepository
    {
        Task<Booking?> GetCart(string UserId);

        Task<Booking?> GetBookingById(int bookingId);

        Task<List<Booking>> GetConfirmedBookings(string UserId);

        Task<List<Booking>> GetAllConfirmedBookigns();

        Task AddNewCartAsync(Booking booking);

        Task<bool> FinalizedCart(string UserId);

        Task<bool> HasBookingOverlap(string userId);

        Task<bool> DeleteBooking(int bookingId);

        Task UpdateCartBooking(Booking updateBooking, Booking oldBooking);

        Task UpdateBooking(Booking updateBooking);
    }
}
