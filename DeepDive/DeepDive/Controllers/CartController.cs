using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Repository;
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

        private CartRepository _cartRepository;
        private ProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(CartRepository cartRepository, ProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (_cartRepository.GetCart(_userId) != null && _cartRepository.GetCart(_userId).Product_Bookings.Count > 0)
            {
                _cartRepository.ValidateBookingOverlap(_cartRepository.GetCart(_userId), _userId);
                TryValidateModel(_cartRepository.GetCart(_userId), nameof(Booking));

                _cartRepository.GetCart(_userId).CalculatePriceOfBooking();

                return View(_cartRepository.GetCart(_userId));
            }

            return View();
        }

        public IActionResult Index(int? bookingId, int? productId, int? quantity, Booking cart)
        {

            // Handle quantity update for specific product
            if (productId.HasValue && quantity.HasValue && bookingId.HasValue)
            {
                // Get info for new Product_Booking
                Product? product = _productRepository.GetProductById(productId.Value);
                Product_Booking newProductBooking = new Product_Booking
                {
                    ProductId = productId.Value,
                    Product = product,
                    BookingId = bookingId.Value,
                    Booking = cart,
                };

                Booking currentCart = _cartRepository.GetCart(_userId);

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

                _cartRepository.UpdateCartProducts(currentCart, _userId);
            }

            _cartRepository.UpdateCartDates(cart, _userId);

            ModelState.Clear();
            _cartRepository.ValidateBookingOverlap(_cartRepository.GetCart(_userId), _userId);
            TryValidateModel(_cartRepository.GetCart(_userId), nameof(Booking));

            // If the cart is empty, return empty view
            if (_cartRepository.GetCart(_userId).Product_Bookings.Count <= 0)
            {
                return View();
            }

            return View(_cartRepository.GetCart(_userId));
        }

        [HttpPost]
        public IActionResult Add(Booking cart)
        {
            _cartRepository.ValidateBookingOverlap(_cartRepository.GetCart(_userId), _userId);
            TryValidateModel(_cartRepository.GetCart(_userId), nameof(Booking));

            if (ModelState.IsValid)
            {
                _cartRepository.AddBooking(_cartRepository.GetCart(_userId));
                _cartRepository.ClearCart(_userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
