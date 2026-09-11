using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            Booking basket = _basketRepository.GetBasket();

            return View(basket);
        }

        [HttpPost]
        public IActionResult Index(Booking basket)
        {
            _basketRepository.UpdateBasketDates(basket);
            return View(_basketRepository.GetBasket());
        }

        //does not inform the user that their booking overlaps and thus cannot be made yet.
        //For now it just redirects the user back to the basket without carrying out AddBooking. 
        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            if (ModelState.IsValid && _bookingRepository.ValidateBookingOverlap(_basketRepository.GetBasket()))
            {
                _bookingRepository.AddBooking(_basketRepository.GetBasket());
                _basketRepository.ResetBasket();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
