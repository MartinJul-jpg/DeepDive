using DeepDive.Data;
using DeepDive.Models;

namespace DeepDive.Services
{
    public interface IDiveDeepApiService
    {
        Task<List<Booking>>GetAllBookingsAsync();

        Task<Booking> GetBookingByIdAsync(int bookingid);

        Task DeleteBookingAsync(int bookingid);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Booking booking);

        Task<List<Booking>> GetAllProductAsync();

        Task<Booking> GetProductByIdAsync(int productid);

        Task DeleteProductAsync(int productid);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task<Booking> GetBasketByUserAsync(int userid);

        Task<Booking> GetNonFinalizedBookingAsync(string userid);

        Task<bool> ValidateBookingAsync(Booking booking, string userid);

        Task AddProductToCartAsync(int productid, string userid);

        Task ClearCart(string userId);


        Task ResetCart(string userId);

        Task<HttpResponseMessage> HttpClientValidate(string uri, string httpMethod, HttpContent httpContent);

        Task<HttpResponseMessage> HttpPostClient(HttpClient httpClient, string uri, HttpContent httpContent);

        Task<HttpResponseMessage> HttpGetClient(HttpClient httpClient, string uri);



    }
}
