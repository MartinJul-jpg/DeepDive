using Shared.DTOs;
using Shared.Models;
using Shared.Services;
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

        //DONE
        public async Task FinalizeBookingAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/{userid}");
            using var response = await httpClient.SendAsync(request);
        }
        
        //DONE
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

        //DONE
        public async Task AddProductToCartAsync(int productid, string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Put, $"api/Cart/{productid}/{userid}");
            using var response = await httpClient.SendAsync(request);
        }

        //DONE
        public async Task DeleteBookingAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Cart/{bookingid}");
            using var response = await httpClient.SendAsync(request);
        }

        //DONE
        public async Task DeleteProductAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Product/{productid}");
            using var response = await httpClient.SendAsync(request);
        }

        //DONE
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Bookings");
            using var response = await httpClient.SendAsync(request);
            //List<BookingDTO> bookingDTOs = JsonSerializer.Deserialize<List<BookingDTO>>(response.Content.ReadAsStream());
            List<BookingDTO> bookingDTOs = await response.Content.ReadFromJsonAsync<List<BookingDTO>>();
            List <Booking> bookings = bookingDTOs.Select(DTO => _DTO_Service.DTO_ToBooking(DTO)).ToList();

            foreach (Booking booking in bookings)
            {
                await FillBookingWithProductsAsync(booking);
            }

            return bookings;
        }

        public async Task<List<Booking>> GetAllUserBookingsAsync(string userId)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Bookings/{userId}");
            using var response = await httpClient.SendAsync(request);
            //List<BookingDTO> bookingDTOs = JsonSerializer.Deserialize<List<BookingDTO>>(response.Content.ReadAsStream());
            List<BookingDTO> bookingDTOs = await response.Content.ReadFromJsonAsync<List<BookingDTO>>();
            List<Booking> bookings = bookingDTOs.Select(DTO => _DTO_Service.DTO_ToBooking(DTO)).ToList();

            foreach (Booking booking in bookings)
            {
                await FillBookingWithProductsAsync(booking);
            }

            return bookings;
        }

        //DONE
        public async Task<List<Product>> GetAllProductAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product");
            using var response = await httpClient.SendAsync(request);
            //List<ProductDTO> productDTOs = JsonSerializer.Deserialize<List<ProductDTO>>(response.Content.ReadAsStream());
            List<ProductDTO> productDTOs = await response.Content.ReadFromJsonAsync<List<ProductDTO>>();
            List <Product> products = productDTOs.Select(DTO => _DTO_Service.DTO_ToProduct(DTO)).ToList();
            return products;
        }

        //DONE
        public async Task<Booking> GetCartByUserAsync(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/{userid}");
            using var response = await httpClient.SendAsync(request);
            //BookingDTO DTO = JsonSerializer.Deserialize<BookingDTO>(response.Content.ReadAsStream());
            BookingDTO DTO = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Booking cart = _DTO_Service.DTO_ToBooking(DTO);

            cart = await FillBookingWithProductsAsync(cart);

            return cart;
        }

        //DONE
        public async Task<Booking> GetBookingByIdAsync(int bookingid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Booking/{bookingid}");
            using var response = await httpClient.SendAsync(request);
            //BookingDTO DTO = JsonSerializer.Deserialize<BookingDTO>(response.Content.ReadAsStream());
            BookingDTO DTO = await response.Content.ReadFromJsonAsync<BookingDTO>();
            Booking booking = _DTO_Service.DTO_ToBooking(DTO);

            booking = await FillBookingWithProductsAsync(booking);

            return booking;
        }

        //DONE
        public async Task<Product> GetProductByIdAsync(int productid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/{productid}");
            using var response = await httpClient.SendAsync(request);
            //ProductDTO DTO = JsonSerializer.Deserialize<ProductDTO>(response.Content.ReadAsStream());
            ProductDTO DTO = await response.Content.ReadFromJsonAsync<ProductDTO>();
            Product product = _DTO_Service.DTO_ToProduct(DTO);
            return product;
        }

        //DONE
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

        //DONE
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

        //DONE
        public async Task<bool> HasBookingOverlap(string userid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Cart/Validate/{userid}");
            using var response = await httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        //DONE
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package");
            using var response = await httpClient.SendAsync(request);
            //List<PackageDTO> packageDTOs = JsonSerializer.Deserialize<List<PackageDTO>>(response.Content.ReadAsStream());
            List<PackageDTO> packageDTOs = await response.Content.ReadFromJsonAsync<List<PackageDTO>>();
            List <Package> packages = packageDTOs.Select(DTO => _DTO_Service.DTO_ToPackage(DTO)).ToList();

            foreach (Package package in packages)
            {
                await FillPackageWithProductsAsync(package);
            }

            return packages;
        }

        //DONE
        public async Task<Package> GetPackageByIdAsync(int packageid)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/Product/Package/{packageid}");
            using var response = await httpClient.SendAsync(request);
            //PackageDTO DTO = JsonSerializer.Deserialize<PackageDTO>(response.Content.ReadAsStream());
            PackageDTO DTO = await response.Content.ReadFromJsonAsync<PackageDTO>();
            Package package = _DTO_Service.DTO_ToPackage(DTO);

            package = await FillPackageWithProductsAsync(package);

            return package;
        }

        //DONE
        public async Task UpdateCartAsync(Booking cart)
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

        public async Task CreateCartAsync(string userId)
        {
            using var httpClient = _httpClientFactory.CreateClient("DeepDiveApi");
            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/Cart/{userId}");
            using var response = await httpClient.SendAsync(request);
        }

        private async Task<Package> FillPackageWithProductsAsync(Package package)
        {
            foreach (Package_Product pp in package.Package_Products)
            {
                pp.Product = await GetProductByIdAsync(pp.ProductId);
            }

            return package;
        }

        private async Task<Booking> FillBookingWithProductsAsync(Booking booking)
        {
            foreach (Product_Booking pb in booking.Product_Bookings)
            {
                pb.Product = await GetProductByIdAsync(pb.ProductId);
            }

            return booking;
        }
    }
}
