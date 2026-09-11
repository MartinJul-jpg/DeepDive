using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfirmedBookings()
        {
            List<Booking> bookings = BookingRepository.GetBookings();
            return View(bookings);
        }

    }
}
