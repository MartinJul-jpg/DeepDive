using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeepDive.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private BasketRepository _basketRepository;
        private BookingRepository _bookingRepository;
        private ProductRepository _productRepository;

        public BasketController(BasketRepository basketRepository, BookingRepository bookingRepository, ProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _bookingRepository = bookingRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (_basketRepository.GetBasket() != null && _basketRepository.GetBasket().Product_Bookings.Count >= 0)
            {
                _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket());
                TryValidateModel(_basketRepository.GetBasket(), nameof(Booking));

                return View(_basketRepository.GetBasket());
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

                Booking currentCart = _basketRepository.GetBasket();

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
            }
            _basketRepository.UpdateBasketDates(cart);

            ModelState.Clear();
            _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket());
            TryValidateModel(_basketRepository.GetBasket(), nameof(Booking));

            // If the basket is empty, return empty view
            if (_basketRepository.GetBasket().Product_Bookings.Count <= 0)
            {
                return View();
            }

            return View(_basketRepository.GetBasket());
        }

        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket());
            TryValidateModel(_basketRepository.GetBasket(), nameof(Booking));

            if (ModelState.IsValid)
            {
                _bookingRepository.AddBooking(_basketRepository.GetBasket());
                _basketRepository.ResetBasket();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
