using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            Booking booking = BookingRepository.booking;
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookedProducts = BookingRepository.booking.BookedProducts;
                BookingRepository.booking = booking;
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
    }
}
