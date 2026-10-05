using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class UserController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;

        public UserController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmedBookings()
        {
            List<Booking> bookings = await _diveDeepApiService.GetAllBookingsAsync();
            return View(bookings);
        }

    }
}
