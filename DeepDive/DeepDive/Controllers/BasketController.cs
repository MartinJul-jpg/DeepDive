using DeepDive.Data;
using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeepDive.Controllers
{
    [Authorize(Roles = "User")]
    public class BasketController : Controller
    {
        public string _userId
        {
            get { return _userManager.GetUserId(this.User); }
        }

        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasketController(BasketRepository basketRepository, ProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (_basketRepository.GetBasket(_userId) != null && _basketRepository.GetBasket(_userId).Product_Bookings.Count > 0)
            {
                _basketRepository.ValidateBookingOverlap(_basketRepository.GetBasket(_userId), _userId);
                TryValidateModel(_basketRepository.GetBasket(_userId), nameof(Booking));

                _basketRepository.GetBasket(_userId).CalculatePriceOfBooking();

                return View(_basketRepository.GetBasket(_userId));
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

                Booking currentCart = _basketRepository.GetBasket(_userId);

                // Count how many of this product are in the basket
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
                    // Remove products from basket
                    int amountToRemove = ofSameProduct.Count - quantity.Value;
                    for (int i = 0; i < amountToRemove; i++)
                    {
                        currentCart.Product_Bookings.Remove(ofSameProduct[i]);
                    }
                }
                else if (ofSameProduct.Count < quantity)
                {
                    // Add products to basket
                    int amountToAdd = quantity.Value - ofSameProduct.Count;
                    for (int i = 0; i < amountToAdd; i++)
                    {
                        currentCart.Product_Bookings.Add(newProductBooking);
                    }
                }

                _basketRepository.UpdateBasketProducts(currentCart, _userId);
            }

            _basketRepository.UpdateBasketDates(cart, _userId);

            ModelState.Clear();
            _basketRepository.ValidateBookingOverlap(_basketRepository.GetBasket(_userId), _userId);
            TryValidateModel(_basketRepository.GetBasket(_userId), nameof(Booking));

            // If the basket is empty, return empty view
            if (_basketRepository.GetBasket(_userId).Product_Bookings.Count <= 0)
            {
                return View();
            }

            return View(_basketRepository.GetBasket(_userId));
        }

        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            _basketRepository.ValidateBookingOverlap(_basketRepository.GetBasket(_userId), _userId);
            TryValidateModel(_basketRepository.GetBasket(_userId), nameof(Booking));

            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation", "Basket");
            }

            return RedirectToAction("Index", "Basket");
        }

        public IActionResult Confirmation()
        {
            return View(new Confirmation());
        }

        [HttpPost]
        public IActionResult Confirmation(Confirmation confirmation)
        {
            if (ModelState.IsValid)
            {
                _basketRepository.UpdateBasketConfirmation(confirmation, _userId);
                _basketRepository.AddBooking(_basketRepository.GetBasket(_userId));
                _basketRepository.clearBasket(_userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Confirmation", "Basket");
        }
    }
}
