using DeepDive.Data;
using DeepDive.Filters;
using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Controllers
{
    //[AdminAuthorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService; 

        private readonly UserManager<ApplicationUser> _userManager;



        public AdminController(IDiveDeepApiService diveDeepApiService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _diveDeepApiService = diveDeepApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _diveDeepApiService.GetAllProductAsync();
            return View(products); 

        }
  
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _diveDeepApiService.DeleteBookingAsync(id);
            return RedirectToAction("Index");
        } 

        public async Task<IActionResult> ViewBooking()
        {
            var bookings = await _diveDeepApiService.GetAllBookingsAsync();
            return View(bookings);
        }

        public async Task<IActionResult> UpdateBooking(int id)
        {
            var booking = await _diveDeepApiService.GetBookingByIdAsync(id);
            ViewBag.Action = "UpdateBooking";
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _diveDeepApiService.UpdateBookingAsync(booking);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("Booking.RowVersion", "Bookingen er ændret siden du efterspurgte resourcen");
            }

            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            Product foundproduct = await _diveDeepApiService.GetProductByIdAsync(id);
            ViewBag.Action = "EditProduct";
            return View(foundproduct);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(BCD? bcd, DivingSuit? divingSuit, Fin? fin, RegulatorSet? regulatorSet, Snorkel? snorkel, Tank? tank, string productTypeSelector)
        {
            Product? product = productTypeSelector switch
            {
                "BCD" => bcd,
                "DivingSuit" => divingSuit,
                "Fin" => fin,
                "RegulatorSet" => regulatorSet,
                "Snorkel" => snorkel,
                "Tank" => tank,
                _ => null
            };

            try
            {
                if (ModelState.IsValid)
                {
                    await _diveDeepApiService.UpdateProductAsync(product);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("Booking.RowVersion", "Produktet er ændret siden du efterspurgte resourcen");
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateNewProduct()
        {
            
            ViewBag.Action = "CreateNewProduct";
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(BCD? bcd, DivingSuit? divingSuit, Fin? fin, RegulatorSet? regulatorSet, Snorkel? snorkel, Tank? tank, string productTypeSelector)
        {
            Product? product = productTypeSelector switch
            {
                "BCD" => bcd,
                "DivingSuit" => divingSuit,
                "Fin" => fin,
                "RegulatorSet" => regulatorSet,
                "Snorkel" => snorkel,
                "Tank" => tank,
                _ => null

            };

            if (product != null)
            {
                await _diveDeepApiService.AddProductAsync(product);
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _diveDeepApiService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        } 

        
    }
}
