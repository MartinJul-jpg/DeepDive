using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;

namespace DeepDive.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        public string _userId
        {
            get { return _userManager.GetUserId(this.User); }
        }

        private readonly IDiveDeepApiService _diveDeepApiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IDiveDeepApiService diveDeepApiService, UserManager<ApplicationUser> userManager)
        {
            _diveDeepApiService = diveDeepApiService;
            _userManager = userManager;
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
