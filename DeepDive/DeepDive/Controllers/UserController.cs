using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class UserController : Controller
    {
        private BookingRepository _bookingRepository;

        public UserController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfirmedBookings()
        {
            List<Booking> bookings = _bookingRepository.GetAllBookings();
            return View(bookings);
        }

    }
}
