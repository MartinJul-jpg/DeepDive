using Shared.Data;
using DeepDive.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace DeepDive.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private string _userId
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
            await _diveDeepApiService.CreateCartAsync(_userId);

            Booking foundCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

            if (foundCart != null && foundCart.Product_Bookings.Count > 0)
            {
                foundCart.ExceedsStock = await _diveDeepApiService.HasBookingOverlap(_userId);
                TryValidateModel(foundCart, nameof(Booking));

                foundCart.CalculatePriceOfBooking();

                return View(foundCart);
            }

            return View();
        }

        public async Task<IActionResult> Index(int? bookingId, int? productId, int? quantity, Booking cartFromForm)
        {
            Booking currentCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

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
                    Booking = cartFromForm,
                };

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

            currentCart.Start = cartFromForm.Start;
            currentCart.End = cartFromForm.End;

            await _diveDeepApiService.UpdateCartAsync(currentCart);

            ModelState.Clear();

            // might be a redundant call, use currentCart instead, it does make sure the database and inmemory is in sync though
            Booking foundCart = await _diveDeepApiService.GetCartByUserAsync(_userId);

            foundCart.ExceedsStock = await _diveDeepApiService.HasBookingOverlap(_userId);
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

            foundCart.ExceedsStock = await _diveDeepApiService.HasBookingOverlap(_userId);
            TryValidateModel(foundCart, nameof(Booking));

            if (ModelState.IsValid)
            {
                await _diveDeepApiService.FinalizeBookingAsync(_userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }


    }
}
