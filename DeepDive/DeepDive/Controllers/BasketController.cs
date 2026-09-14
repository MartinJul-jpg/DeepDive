using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        private BasketRepository _basketRepository;
        private BookingRepository _bookingRepository;
        private ProductRepository _productRepository;

        public BasketController(BasketRepository basketRepository,BookingRepository bookingRepository, ProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _bookingRepository = bookingRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket());
            TryValidateModel(_basketRepository.GetBasket(), nameof(Booking));

            return View(_basketRepository.GetBasket());
        }
            

        public IActionResult Index(int? bookingId, int? productId, int? quantity, Booking cart)
        {

            // Handle quantity update for specific product
            if (productId.HasValue && quantity.HasValue && bookingId.HasValue)
            {
                // Count how many of this product are in the basket
                //List<int> ofSameProduct = new List<int>();
                //foreach (int id in cart.Product_Bookings)
                //{
                //    if (id == productId)
                //    {
                //        ofSameProduct.Add(id);
                //    }
                //}

                //if (ofSameProduct.Count > quantity)
                //{
                //    // Remove products from basket
                //    int amountToRemove = ofSameProduct.Count - quantity.Value;
                //    for (int i = 0; i < amountToRemove; i++)
                //    {
                //        cart.Product_Bookings.Remove(productId);
                //    }
                //}
                //else if (ofSameProduct.Count < quantity)
                //{
                //    // Add products to basket
                //    int amountToAdd = quantity.Value - ofSameProduct.Count;
                //    for (int i = 0; i < amountToAdd; i++)
                //    {
                //        cart.Product_Bookings.Add(productId);
                //    }
                //}
            }
            else
            {
                // Handle date changes
                //cart.Start = basket.Start;
                //cart.End = basket.End;

                _basketRepository.UpdateBasketDates(cart);

                ModelState.Clear();
                _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket());
                TryValidateModel(_basketRepository.GetBasket(), nameof(Booking));
            }

            // Recalculate DurationInDays and TotalPrice
            cart.CalculatePriceOfBooking();
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
