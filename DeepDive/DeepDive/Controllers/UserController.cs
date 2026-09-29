using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class UserController : Controller
    {
        private CartRepository _cartRepository;

        public UserController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfirmedBookings()
        {
            List<Booking> bookings = _cartRepository.GetAllBookings();
            return View(bookings);
        }

    }
}
