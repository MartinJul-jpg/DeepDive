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
            List<Product> products = new List<Product>(_productRepository.GetAllProducts());
            return View(products); 



        } 


        public IActionResult DeleteBooking()
        {
            return View();
        } 

        public IActionResult UpdateBooking()
        {
            return View();
        } 

        public IActionResult EditProduct(int productid)
        {
            Product foundproduct = _productRepository.GetProductById(productid);
            ViewBag.Action = "EditProduct";
            return View(foundproduct);
        }

        

        public IActionResult CreateNewProduct()
        {
            
            ViewBag.Action = "CreateNewProduct";
            return View();
        } 

        [HttpPost]
        public IActionResult CreateNewProduct(Product product)
        {
            if (product != null)
            {
                _productRepository.AddNewProduct(product);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteProduct(int productid)
        {
            _productRepository.DeleteProduct(productid);
            return RedirectToAction("Index");
        } 

        
    }
}
