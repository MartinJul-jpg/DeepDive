using DeepDive.Models;
using DeepDive.Repository;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int id)
        {
            var bookingvm = new BookingViewModel();
            Product product = Repository.ProductRepository.GetProductById(id);
            bookingvm.Product = product;

            return View(bookingvm);
        }

        public IActionResult AddBooking(int id)
        {
            
            BookingRepository.AddProductToBooking(id);
            return RedirectToAction("Index", "Basket");
            

            
            
            
        }
    }
}
