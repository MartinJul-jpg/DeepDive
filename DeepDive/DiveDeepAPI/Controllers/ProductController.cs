using Microsoft.AspNetCore.Mvc;
using DiveDeepAPI.Repository;
using DiveDeepAPI.Models;

namespace DiveDeepAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _productRepository.GetAllProducts();
            // select to stop it from grabing the product again 
            var productDTOs = products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Price = p.Price,
                Brand = p.Brand,
                Amount = p.Amount,
                ImgPath = p.ImgPath,

                UniqueInfo = p.GetUniqueInfo(),
                ProductType = p.GetType().Name,

                PackageIds = p.Package_Products
            .Select(pp => pp.PackageId)
            .ToList(),

                BookingIds = p.Product_Bookings
                .Select(pb => pb.BookingId)
                .ToList()
            });
            return Ok(productDTOs);
        }

    }
}
