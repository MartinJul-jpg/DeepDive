using DeepDive.Models;
using DeepDive.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class AdminController : Controller
    {
        private BookingRepository _bookingRepository;
        private ProductRepository _productRepository;

        public AdminController(BookingRepository bookingrepo, ProductRepository productrepo)
        {
            _bookingRepository = bookingrepo;
            _productRepository = productrepo;
        }
        public IActionResult Index()
        {
            return View(); 



        } 


        public IActionResult DeleteBooking()
        {
            return View();
        } 

        public IActionResult UpdateBooking()
        {
            return View();
        } 

        public IActionResult EditProduct(Product? product)
        {

            ViewBag.Action = "Edit";
            return View(product);
        }

        

        public IActionResult CreateNewProduct()
        {
            
            ViewBag.Action = "Add";
            return View();
        } 

        public IActionResult DeleteProduct()
        {
            return View();
        } 

        
    }
}
