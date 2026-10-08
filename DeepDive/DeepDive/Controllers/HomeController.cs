using DeepDive.Models;
using DeepDive.Services;
using DeepDive.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace DeepDive.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;
        private IWeatherHttpService _weatherHttpService;

        public HomeController(IDiveDeepApiService diveDeepApiService, IWeatherHttpService weatherHttpService)
        {
            _diveDeepApiService = diveDeepApiService;
            _weatherHttpService = weatherHttpService;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            if (searchTerm == null)
            {
                searchTerm = "Middelfart";
            }
            List<Package> packages =  new List<Package>( await _diveDeepApiService.GetAllPackagesAsync());

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
