using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class UserController : Controller
    {
        private BasketRepository _basketRepository;

        public UserController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConfirmedBookings()
        {
            List<Booking> bookings = _basketRepository.GetAllBookings();
            return View(bookings);
        }

    }
}
