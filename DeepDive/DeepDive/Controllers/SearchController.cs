using DeepDive.Models;
using DeepDive.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace DeepDive.Controllers
{
    public class SearchController : Controller
    {
        private readonly IDiveDeepApiService _diveDeepApiService;

        public SearchController(IDiveDeepApiService diveDeepApiService)
        {
            _diveDeepApiService = diveDeepApiService;
        }

        [HttpPost]
        public async Task<IActionResult> Show(string searchQuery)
        {
            List<Package> packages;
            List<Product> products;
            if (!searchQuery.IsNullOrEmpty())
            {
                string[] queryWords = searchQuery.ToLower().Split(" ");

                packages = await _diveDeepApiService.GetAllPackagesAsync();
                    //.Where(package => Regex.Match(package.GetSearchBlob().ToLower(), $"{searchQuery.Query.ToLower()}+").Length > 0)
                packages
                    .Where(package => HasQueryWords(package.GetSearchBlob(), queryWords))
                    .ToList();
                products = await _diveDeepApiService.GetAllProductAsync();
                    //.Where(product => Regex.Match(product.GetSearchBlob().ToLower(), $"{searchQuery.Query.ToLower()}+").Length > 0)
                products
                    .Where(product => HasQueryWords(product.GetSearchBlob(), queryWords))
                    .ToList();

                bool HasQueryWords(string blob, string[] queryWords)
                {
                    string properBlob = blob.ToLower();
                    return queryWords.All(queryWord => properBlob.Contains(queryWord));
                }
            }
            else
            {
                packages = new List<Package>();
                products = new List<Product>();
            }

            SearchQuery result = new SearchQuery { Packages = packages, Products = products };

            return View(result);
        }
    }
}
