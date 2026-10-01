using Azure;
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
        public async Task AddBookingAsync(Booking booking)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new Booking
                {
                   Finalized = true
                }),
                Encoding.UTF8);
            var responsemessage = await HttpClientValidate("/Booking/Add", "Post", jsonContent);
        }

        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task AddProductToCartAsync(int productid, string userid)
        {
            throw new NotImplementedException();
        }

        public Task ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookingAsync(int bookingid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int productid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetAllProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBasketByUserAsync(int userid)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingByIdAsync(int bookingid)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetNonFinalizedBookingAsync(string userid)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetProductByIdAsync(int productid)
        {
            throw new NotImplementedException();
        }


        public Task ResetCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateBookingAsync(Booking booking, string userid)
        {
            throw new NotImplementedException();
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


    }
}
