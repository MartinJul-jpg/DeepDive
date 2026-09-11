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
        public IActionResult Index(int? productId, int quantity, Booking booking)
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Add(Booking basket)
        {
            if (ModelState.IsValid)
            {
                //Checks if product amount is smaller or equal to actuall product amount saved inside storage
                if (basket.BookedProducts.All(product => ProductRepository.ProductCheckAmount(product, 1)))
                {
                    BookingRepository.AddBooking(basket);
                    //Change this to be more dynamic instead of static
                    //basket.BookedProducts.ToList().ForEach(product => ProductRepository.RemoveAmount(product, 1));
                    BookingRepository.ClearBasket();
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Basket");
        }
    }
}
