using DeepDive.Data;
using DeepDive.Models;

namespace DeepDive.Services
{
    public interface IDiveDeepApiService
    {
        Task<List<Booking>>GetAllBookingsAsync();

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

        Task<Booking> GetCartByUserAsync(string userid);

        Task UpdateCartDatesAsync(Booking cart);

        Task<bool> ValidateBookingAsync(string userid);

        Task AddProductToCartAsync(int productid, string userid);
    }
}
