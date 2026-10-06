using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeepDive.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public string _userId
        {
            get { return _userManager.GetUserId(this.User); }
        }

        private IDiveDeepApiService _diveDeepApiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IDiveDeepApiService diveDeepApiService, UserManager<ApplicationUser> userManager)
        {
            _diveDeepApiService = diveDeepApiService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Booking cart = await _diveDeepApiService.GetCartByUserAsync(_userId);

            if (await _diveDeepApiService.GetCartByUserAsync(_userId) != null && cart.Product_Bookings.Count > 0)
            {
                Booking foundCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

                foundCart.ExceedsStock = await _diveDeepApiService.ValidateBookingAsync(_userId);
                TryValidateModel(foundCart, nameof(Booking));

                Booking cartPrice = await _diveDeepApiService.GetCartByUserAsync(_userId);
                cartPrice.CalculatePriceOfBooking();

                return View(await _diveDeepApiService.GetCartByUserAsync(_userId));
            }

            return View();
        }

        public async Task<IActionResult> Index(int? bookingId, int? productId, int? quantity, Booking cart)
        {

            // Handle quantity update for specific product
            if (productId.HasValue && quantity.HasValue && bookingId.HasValue)
            {
                // Get info for new Product_Booking
                Product? product = await _diveDeepApiService.GetProductByIdAsync(productId.Value);
                Product_Booking newProductBooking = new Product_Booking
                {
                    ProductId = productId.Value,
                    Product = product,
                    BookingId = bookingId.Value,
                    Booking = cart,
                };

                Booking currentCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

                // Count how many of this product are in the cart
                List<Product_Booking> ofSameProduct = new List<Product_Booking>();
                foreach (Product_Booking productBooking in currentCart.Product_Bookings)
                {
                    if (productBooking.ProductId == newProductBooking.ProductId)
                    {
                        ofSameProduct.Add(productBooking);
                    }
                }

                if (ofSameProduct.Count > quantity)
                {
                    // Remove products from cart
                    int amountToRemove = ofSameProduct.Count - quantity.Value;
                    for (int i = 0; i < amountToRemove; i++)
                    {
                        currentCart.Product_Bookings.Remove(ofSameProduct[i]);
                    }
                }
                else if (ofSameProduct.Count < quantity)
                {
                    // Add products to cart
                    int amountToAdd = quantity.Value - ofSameProduct.Count;
                    for (int i = 0; i < amountToAdd; i++)
                    {
                        currentCart.Product_Bookings.Add(newProductBooking);
                    }
                }
            }

            await _diveDeepApiService.UpdateCartDatesAsync(cart, _userId);

            ModelState.Clear();

            Booking foundCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

            foundCart.ExceedsStock = await _diveDeepApiService.ValidateBookingAsync(_userId);
            TryValidateModel(foundCart, nameof(Booking));

            if (foundCart.Product_Bookings.Count <= 0)
            {
                return View();
            }

            return View(foundCart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Booking cart)
        {
            Booking foundCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

            foundCart.ExceedsStock = await _diveDeepApiService.ValidateBookingAsync(_userId);
            TryValidateModel(foundCart, nameof(Booking));

            if (ModelState.IsValid)
            {
                await _diveDeepApiService.AddBookingAsync(_userId);
                await _diveDeepApiService.ClearCartAsync(_userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }


    }
}
