using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        private BookingRepository _bookingRepository;
        private ProductRepository _productRepository;

        public BasketController(BookingRepository bookingRepository, ProductRepository productRepository)
        {
            _bookingRepository = bookingRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            Booking basket = _bookingRepository.GetBasket();

            return View(basket);
        }

        [HttpPost]
        public IActionResult Index(Booking basket)
        {
            _bookingRepository.UpdateBasket(basket);
            return View(basket);
        }

        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            if (ModelState.IsValid)
            {
                _bookingRepository.AddBooking(basket);
                //Change this to be more dynamic instead of static
                basket.BookedProducts.ToList().ForEach(product => _productRepository.RemoveAmount(product, 1));

                _bookingRepository.ClearBasket();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
