using Azure;
using Azure.Core;
using DeepDive.Models;
using System.Text;
using System.Text.Json;

namespace DeepDive.Services
{
    public class DiveDeepApiService : IDiveDeepApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiveDeepApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task AddBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Cart/Finalize/{userid}");
            using var response = await httpClient.SendAsync(request);
        }
        
        public async Task AddProductAsync(Product product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(product);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7000/DeepDiveApi/AddProduct/{product.GetType()}");
            request.Content = new StringContent(
               productjson, System.Text.Encoding.UTF8, "application/json"
                );

            using var response = await httpClient.SendAsync(request); 
                
        }

        public async Task AddProductToCartAsync(int productid, string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7000/DeepDiveApi/Cart/AddProduct/{userid}/{productid}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task ClearCartAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Cart/Clear/{userid}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task DeleteBookingAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7000/DeepDiveApi/Booking/Delete/{bookingid}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task DeleteProductAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7000/DeepDiveApi/Product/Delete/{productid}");
            using var response = await httpClient.SendAsync(request);
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Bookings");
            using var response = await httpClient.SendAsync(request);
            List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(response.Content.ReadAsStream());
            return bookings;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Products");
            using var response = await httpClient.SendAsync(request);
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(response.Content.ReadAsStream());
            return products;
        }

        public async Task<Booking> GetCartByUserAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Cart/GetCart/{userid}");
            using var response = await httpClient.SendAsync(request);
            Booking cart = JsonSerializer.Deserialize<Booking>(response.Content.ReadAsStream());
            return cart;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Booking/GetBooking/{bookingid}");
            using var response = await httpClient.SendAsync(request);
            Booking booking = JsonSerializer.Deserialize<Booking>(response.Content.ReadAsStream());
            return booking;
        }

        public async Task<Booking> GetNonFinalizedBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Booking/GetNonFinalizedBooking/{userid}");
            using var response = await httpClient.SendAsync(request);
            Booking booking = JsonSerializer.Deserialize<Booking>(response.Content.ReadAsStream());
            return booking;
        }

        public async Task<Product> GetProductByIdAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7000/DeepDiveApi/Product/{productid}");
            using var response = await httpClient.SendAsync(request);
            Product product = JsonSerializer.Deserialize<Product>(response.Content.ReadAsStream());
            return product;
        }


        public async Task ResetCartAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7000/DeepDiveApi/Cart/ResetCart/{userid}");
            using var response = await httpClient.SendAsync(request);
            
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(booking);
            using var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7000/DeepDiveApi/Booking/UpdateBooking");
            request.Content = new StringContent(
               productjson, System.Text.Encoding.UTF8, "application/json"
                );

            using var response = await httpClient.SendAsync(request);
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(product);
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Product/Update/{product.ProductId}");
            request.Content = new StringContent(
               productjson, System.Text.Encoding.UTF8, "application/json"
                );

            using var response = await httpClient.SendAsync(request);
        }

        public async Task<bool> ValidateBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/{userid}");
            bool response = JsonSerializer.Deserialize<bool>(request.Content.ReadAsStream());
            return response;
        }

        public async Task<HttpResponseMessage> HttpGetClient(HttpClient httpClient, string uri)
        {


            var response = await httpClient.GetAsync(uri);



            return response;
        }

        public async Task<HttpResponseMessage> HttpPostClient(HttpClient httpClient, string uri, HttpContent httpContent)
        {

            var response = await httpClient.PostAsync(uri, httpContent);



            return response;
        }

        public async Task<HttpResponseMessage> HttpClientValidate(string uri, string httpMethod, HttpContent httpContent)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient("DeepDiveApi");

            HttpResponseMessage response = null;
            switch (httpMethod)
            {

                case "Get":
                    response = await HttpGetClient(httpClient, uri);
                    break;
                case "Post":
                    response = await HttpPostClient(httpClient, uri, httpContent);
                    break;
                default:
                    return null;

            }
            if (response == null)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<List<Package>> GetAllPackagesAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package");
            using var response = await httpClient.SendAsync(request);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(response.Content.ReadAsStream());
            return packages;
        }

        public async Task<Package> GetPackageByIdAsync(int packageid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package/{packageid}");
            using var response = await httpClient.SendAsync(request);
            Package package = JsonSerializer.Deserialize<Package>(response.Content.ReadAsStream());
            return package;
        }

        public async Task UpdateCartDatesAsync(Booking cart)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(cart);
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/UpdateCart");
            request.Content = new StringContent(
               productjson, System.Text.Encoding.UTF8, "application/json"
                );

            using var response = await httpClient.SendAsync(request);
        }
    }
}
