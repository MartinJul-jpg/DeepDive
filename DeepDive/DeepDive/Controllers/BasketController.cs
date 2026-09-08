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
        public IActionResult Index(Booking basket)
        {
            BookingRepository.UpdateBasket(basket);
            return View(basket);
        }

        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            if (ModelState.IsValid)
            {
                BookingRepository.AddBooking(basket);
                BookingRepository.ClearBasket();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Basket");
        }
    }
}
