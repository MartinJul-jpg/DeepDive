using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [ValidateAntiForgeryToken]
        public IActionResult Index(Booking basket, string submitAction)
        {

            // Always operate on the server-side basket for validation
            var serverBasket = BookingRepository.GetBasket();
            if (serverBasket == null || serverBasket.BookedProducts == null || !serverBasket.BookedProducts.Any())
            {
                ModelState.AddModelError("", "Your basket is empty.");
                return View(serverBasket);
            }

            // Validate dates/duration on the Basket page (move this logic here from Confirmation)
            // Ensure Start and End are present and End is after Start
            try
            {
                if (serverBasket.End.DayNumber - serverBasket.Start.DayNumber < 1)
                {
                    ModelState.AddModelError("", "Please select valid start and end dates. Duration must be at least 1 day.");
                    return View(serverBasket);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Please set valid start and end dates before proceeding to confirmation.");
                return View(serverBasket);
            }

            // Validate product availability here (move criteria from Confirmation to Basket page)
            bool allAvailable = serverBasket.BookedProducts.All(p => ProductRepository.ProductCheckAmount(p, 1));
            if (!allAvailable)
            {
                ModelState.AddModelError("", "One or more products are unavailable. Please remove unavailable items from your basket.");
                return View(serverBasket);
            }

            // If this post came from the Book button, go to confirmation. If it was
            // an update (e.g., a date change), stay on the basket page and show
            // updated totals/errors.
            if (!string.IsNullOrEmpty(submitAction) && submitAction.Equals("book", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Confirmation", "Basket");
            }

            // For update actions, just render the Basket view with updated model/state.
            return View(serverBasket);
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            // Allow users to navigate to the Confirmation page via GET.
            Booking basket = BookingRepository.GetBasket();
            if (basket == null)
            {
                return RedirectToAction("Index", "Basket");
            }
            // Clear any previous modelstate so validation messages from other pages
            // aren't shown on the confirmation page.
            ModelState.Clear();
            return View(basket);
        }

        [HttpPost]
        [ActionName("Confirmation")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmationPost()
        {
            // Finalize booking. Availability and duration checks have been moved to the Basket page
            // so we always use the server-side basket here to finalize.
            var serverBasket = BookingRepository.GetBasket();
            if (serverBasket == null)
            {
                return RedirectToAction("Index", "Basket");
            }

            BookingRepository.AddBooking(serverBasket);
            BookingRepository.ClearBasket();
            return RedirectToAction("Index", "Home");
        }
    }
}
