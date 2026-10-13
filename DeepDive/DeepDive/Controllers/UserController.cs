using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;
using System.Security.Claims;

namespace DeepDive.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private string _userId
        {
            get { return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException(); }
        }

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
            List<Booking> bookings = await _diveDeepApiService.GetAllUserBookingsAsync(_userId);
            return View(bookings);
        }

    }
}
