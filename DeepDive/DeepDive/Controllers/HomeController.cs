using DeepDive.Models;
using DeepDive.Repository;
using DeepDive.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepDive.Controllers
{
    public class HomeController : Controller
    {
        private ProductRepository _productRepository;
        private IWeatherHttpService _weatherHttpService;

        public HomeController(ProductRepository productRepository, IWeatherHttpService weatherHttpService)
        {
            _productRepository = productRepository;
            _weatherHttpService = weatherHttpService;
        }

        public async Task<IActionResult> Index()
        {
            List<Package> packages =  new List<Package>(_productRepository.GetAllPackages());

            var cityRespone = await _weatherHttpService.GetCityByNameAsync("Middelfart");

            var weatherRespone = await _weatherHttpService.GetMeanWeatherForecastAsync(cityRespone.Latitude, cityRespone.Longitude);

            var marineRespone = await _weatherHttpService.GetMarinaForecastAsync(cityRespone.Latitude, cityRespone.Longitude);

            return View(packages);
        }
    }
}
