using Azure;
using Azure.Core;
using DeepDive.Models;
using DeepDive.ReceivedDTO;
using System.Text;
using System.Text.Json;

namespace DeepDive.Services
{
    public class DiveDeepApiService : IDiveDeepApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDTO_Service _DTO_Service;

        public DiveDeepApiService(IHttpClientFactory httpClientFactory, IDTO_Service DTO_Service)
        {
            _httpClientFactory = httpClientFactory;
            _DTO_Service = DTO_Service;
        }

        //done
        public async Task FinalizeBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/{userid}");
            using var response = await httpClient.SendAsync(request);
        }
        
        //done
        public async Task AddProductAsync(Product product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(_DTO_Service.ProductToDTO(product));
            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/Product");
            request.Content = new StringContent(
                productjson, 
                System.Text.Encoding.UTF8, 
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        //mangler en metode i api til at tilføje enkelte produkter til usercart
        public async Task AddProductToCartAsync(int productid, string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/AddProduct/{userid}/{productid}");
            using var response = await httpClient.SendAsync(request);
        }

        //done
        public async Task DeleteBookingAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Cart/{bookingid}");
            using var response = await httpClient.SendAsync(request);
        }

        //done
        public async Task DeleteProductAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Product/{productid}");
            using var response = await httpClient.SendAsync(request);
        }

        //done
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Bookings");
            using var response = await httpClient.SendAsync(request);
            List<BookingDTO> bookingDTOs = await response.Content.ReadFromJsonAsync<List<BookingDTO>>();
            List<Booking> bookings = bookingDTOs.Select(DTO => _DTO_Service.DTO_ToBooking(DTO)).ToList();
            return bookings;
        }

        //done
        public async Task<List<Product>> GetAllProductAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product");
            using var response = await httpClient.SendAsync(request);
            List<ProductDTO> productDTOs = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
            List<Product> products = productDTOs.Select(DTO => _DTO_Service.DTO_ToProduct(DTO)).ToList();
            return products;
        }

        //done
        public async Task<Booking> GetCartByUserAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/{userid}");
            using var response = await httpClient.SendAsync(request);
            BookingDTO DTO = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Booking cart = _DTO_Service.DTO_ToBooking(DTO);
            return cart;
        }

        //mangler en metode i api til at få en booking ud fra et bookingid
        public async Task<Booking> GetBookingByIdAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Booking/GetBooking/{bookingid}");
            using var response = await httpClient.SendAsync(request);
            BookingDTO DTO = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Booking booking = _DTO_Service.DTO_ToBooking(DTO);
            return booking;
        }

        //done
        public async Task<Product> GetProductByIdAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/{productid}");
            using var response = await httpClient.SendAsync(request);
            ProductDTO DTO = await response.Content.ReadFromJsonAsync<ProductDTO>();
            Product product = _DTO_Service.DTO_ToProduct(DTO);
            return product;
        }

        //done
        public async Task UpdateBookingAsync(Booking booking)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(_DTO_Service.BookingToDTO(booking));
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/UpdateBooking");
            request.Content = new StringContent(
                productjson, 
                System.Text.Encoding.UTF8, 
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        //done
        public async Task UpdateProductAsync(Product product)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(_DTO_Service.ProductToDTO(product));
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Product");
            request.Content = new StringContent(
                productjson, 
                System.Text.Encoding.UTF8, 
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }

        //done
        public async Task<bool> ValidateBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Validate/{userid}");
            using var response = await httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        //done done
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package");
            using var response = await httpClient.SendAsync(request);
            List<PackageDTO> packageDTOs = await response.Content.ReadFromJsonAsync<List<PackageDTO>>(); 
            List<Package> packages = packageDTOs.Select(DTO => _DTO_Service.DTO_ToPackage(DTO)).ToList();

          
            return packages;
        }

        //done done
        public async Task<Package> GetPackageByIdAsync(int packageid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package/{packageid}");
            using var response = await httpClient.SendAsync(request);
            PackageDTO DTO = await response.Content.ReadFromJsonAsync<PackageDTO>(); ;
            Package package = _DTO_Service.DTO_ToPackage(DTO);
            return package;
        }

        //done
        public async Task UpdateCartDatesAsync(Booking cart)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            var productjson = JsonSerializer.Serialize(_DTO_Service.BookingToDTO(cart));
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/UpdateCart");
            request.Content = new StringContent(
                productjson, 
                System.Text.Encoding.UTF8, 
                "application/json");

            using var response = await httpClient.SendAsync(request);
        }
    }
}
