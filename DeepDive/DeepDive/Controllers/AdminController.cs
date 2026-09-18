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

        public IActionResult EditProduct(int id)
        {
            Product foundproduct = _productRepository.GetProductById(id);
            ViewBag.Action = "EditProduct";
            return View(foundproduct);
        }

        

        public IActionResult CreateNewProduct()
        {
            
            ViewBag.Action = "CreateNewProduct";
            return View();
        } 

        [HttpPost]
        public IActionResult CreateNewProduct(BCD? bcd, DivingSuit? divingSuit, Fin? fin, RegulatorSet? regulatorSet, Snorkel? snorkel, Tank? tank, string Brand)
        {
            Product? product = Brand switch
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
