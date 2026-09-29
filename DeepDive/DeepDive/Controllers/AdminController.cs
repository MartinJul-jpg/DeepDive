using DeepDive.Data;
using DeepDive.Filters;
using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Controllers
{
    [AdminAuthorize("Admin")]
    public class AdminController : Controller
    {
        private CartRepository _cartRepository;
        private ProductRepository _productRepository;

        private readonly UserManager<ApplicationUser> _userManager;



        public AdminController(CartRepository cartRepository, ProductRepository productrepo, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _productRepository = productrepo;
        }

        public IActionResult Index()
        {
            List<Product> products = new List<Product>(_productRepository.GetAllProducts());
            return View(products); 

        }
  
        public IActionResult DeleteBooking(int id)
        {
            _basketRepository.DeleteBookingAdmin(id);
            return RedirectToAction("Index");
        } 

        public IActionResult ViewBooking()
        {
            var bookings = _cartRepository.GetAllBookings();
            return View(bookings);
        }

        public IActionResult UpdateBooking(int id)
        {
            var booking = _cartRepository.GetBookingById(id);
            ViewBag.Action = "UpdateBooking";
            return View(booking);
        }

        [HttpPost]
        public IActionResult UpdateBooking(Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cartRepository.UpdateCartFromAdminPage(booking);
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

        public IActionResult EditProduct(int id)
        {
            Product foundproduct = _productRepository.GetProductById(id);
            ViewBag.Action = "EditProduct";
            return View(foundproduct);
        }

        [HttpPost]
        public IActionResult EditProduct(BCD? bcd, DivingSuit? divingSuit, Fin? fin, RegulatorSet? regulatorSet, Snorkel? snorkel, Tank? tank, string productTypeSelector)
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
                    _productRepository.UpdateProduct(product);
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
        public IActionResult CreateNewProduct(BCD? bcd, DivingSuit? divingSuit, Fin? fin, RegulatorSet? regulatorSet, Snorkel? snorkel, Tank? tank, string productTypeSelector)
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
                _productRepository.AddNewProduct(product);
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        } 

        
    }
}
