using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class BasketController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Booking basket = BookingRepository.GetBasket();

            return View(basket);
        }

        [HttpPost]
        public IActionResult Index(int? bookingId, int? productId, int? quantity, Booking basket)
        {
            Booking booking = BookingRepository.GetBasket();

            // Handle quantity update for specific product
            if (productId.HasValue && quantity.HasValue && bookingId.HasValue)
            {
                // Count how many of this product are in the basket
                List<int> ofSameProduct = new List<int>();
                foreach (int id in booking.BookedProducts)
                {
                    if (id == productId)
                    {
                        ofSameProduct.Add(id);
                    }
                }

                if (ofSameProduct.Count > quantity)
                {
                    // Remove products from basket
                    int amountToRemove = ofSameProduct.Count - quantity.Value;
                    for (int i = 0; i < amountToRemove; i++)
                    {
                        booking.BookedProducts.Remove(productId.Value);
                    }
                }
                else if (ofSameProduct.Count < quantity)
                {
                    // Add products to basket
                    int amountToAdd = quantity.Value - ofSameProduct.Count;
                    for (int i = 0; i < amountToAdd; i++)
                    {
                        booking.BookedProducts.Add(productId.Value);
                    }
                }
            }
            else
            {
                // Handle date changes
                booking.Start = basket.Start;
                booking.End = basket.End;
            }

            // Recalculate DurationInDays and TotalPrice
            booking.CalculatePriceOfBooking();
            BookingRepository.UpdateBasket(booking);
            return View(booking);
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
