using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class AdminController : Controller
    {
        private BasketRepository _basketRepository;
        private ProductRepository _productRepository;

        public AdminController(BasketRepository basketRepository, ProductRepository productrepo)
        {
            _basketRepository = basketRepository;
            _productRepository = productrepo;
        }
        public IActionResult Index()
        {
            List<Product> products = new List<Product>(_productRepository.GetAllProducts());
            return View(products); 

        } 

        public IActionResult DeleteBooking()
        {
            return View();
        } 

        public IActionResult ViewBooking()
        {
            var bookings = _basketRepository.GetAllBookings();
            return View(bookings);
        }

        public IActionResult UpdateBooking(int id)
        {
            var booking = _basketRepository.GetBookingById(id);
            ViewBag.Action = "UpdateBooking";
            return View(booking);
        }

        [HttpPost]
        public IActionResult UpdateBooking(Booking booking)
        {

            

            if (booking != null)
            {
                _basketRepository.UpdateBasketAdminPage(booking);
                return RedirectToAction("Index");
            }


            return View();
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

            if (product != null)
            {
                _productRepository.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            return View();
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
