
using Shared.Models;

namespace DeepDive.Services
{
    public interface IDiveDeepApiService
    {
        Task<List<Booking>>GetAllBookingsAsync();

        Task<List<Booking>> GetAllUserBookingsAsync(string userId);

        Task<Booking> GetBookingByIdAsync(int bookingid);

        Task DeleteBookingAsync(int bookingid);

        Task FinalizeBookingAsync(string userid);

        Task UpdateBookingAsync(Booking booking);

        Task<List<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int productid);

        Task DeleteProductAsync(int productid);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task<List<Package>> GetAllPackagesAsync();

        Task<Package> GetPackageByIdAsync(int packageid);

        Task UpdateCartAsync(Booking cart);

        Task CreateCartAsync(string userId);

        Task<Booking> GetCartByUserAsync(string userid);

        Task<bool> HasBookingOverlap(string userid);

        Task AddProductToCartAsync(int productid, string userid);
    }
}
