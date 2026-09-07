using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            Booking basket = BookingRepository.GetBasket();

            return View(basket);
        }

        [HttpPost]
        public IActionResult Index(Booking booking)
        {
            return View(booking);
        }

        [HttpPost]
        public IActionResult Add(Booking booking)
        {
            if (ModelState.IsValid)
            {
                BookingRepository.AddBooking(booking);
                BookingRepository.ClearBasket();
                return RedirectToAction("Index", "Home");
            }

            return View(booking);
        }
    }
}
