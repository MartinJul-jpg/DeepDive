using DeepDive.Models;
using DeepDive.Repository;
using DeepDive.Services;
using DeepDive.ViewModels;
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

        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.ScrollToForecast = true;
            }

            if (searchTerm == null)
            {
                searchTerm = "Middelfart";
            }
            List<Package> packages =  new List<Package>(_productRepository.GetAllPackages());

            var cityRespone = await _weatherHttpService.GetCityByNameAsync(searchTerm);
            if (cityRespone == null)
            {
                cityRespone = await _weatherHttpService.GetCityByNameAsync("Middelfart");
            }
            var weatherRespone = await _weatherHttpService.GetMeanWeatherForecastAsync(cityRespone.Latitude, cityRespone.Longitude);

            var marineRespone = await _weatherHttpService.GetMarineForecastAsync(cityRespone.Latitude, cityRespone.Longitude);

            marineRespone = marineRespone.GetMeanMarineForecat();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Packages = packages,
                ForecastViewModel = new ForecastViewModel
                {
                    City = cityRespone,
                    WeatherForecast = weatherRespone,
                    MarineForecast = marineRespone
                }
            };

            return View(homeViewModel);
        }
    }
}
