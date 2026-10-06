using DeepDive.Models;

namespace DeepDive.ViewModels
{
    public class HomeViewModel
    {
        public ForecastViewModel ForecastViewModel { get; set; }
        public List<Package> Packages { get; set; }
    }
}
