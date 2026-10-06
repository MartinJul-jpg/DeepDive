using DiveDeepAPI.DTO;
using DiveDeepAPI.Models;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


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

        // GET localhost:8080/api/Product
        // json med listen af alle produkter
        [HttpGet]
        public async Task<IActionResult> GetProducts()
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
                RowVersion = p.RowVersion,

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

        // GET localhost:8080/api/Product/[id]
        // json af et product
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0) return BadRequest();

            var product = await _productRepository.GetProductById(id);

            if (product == null) return NotFound();

            var productDTO = new ProductDTO
            {
                ProductId = product.ProductId,
                Price = product.Price,
                Brand = product.Brand,
                Amount = product.Amount,
                ImgPath = product.ImgPath,
                RowVersion = product.RowVersion,

                UniqueInfo = product.GetUniqueInfo(),
                ProductType = product.GetType().Name,

                PackageIds = product.Package_Products?.Select(pp => pp.PackageId).ToList() ?? new List<int>(),
                BookingIds = product.Product_Bookings?.Select(pb => pb.BookingId).ToList() ?? new List<int>()
            };
            return Ok(productDTO);
        }

        // GET Product/Package
        // json med listen af alle pakker
        [HttpGet("Package")]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _productRepository.GetAllPackages();
            var packageDTOs = packages.Select(p => new PackageDTO
            {
                PackageId = p.PackageId,
                PackageIds = p.Package_Products
            .Select(pp => pp.ProductId)
            .ToList(),
            });
            return Ok(packageDTOs);
        }

        // GET localhost:8080/api/Product/Package/[id]
        // json af pakke
        [HttpGet("Package/{id}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            if (id <= 0) return BadRequest();
            var package = await _productRepository.GetPackageById(id);
            if (package == null) return NotFound();
            var packageDTOs = new PackageDTO
            {
                PackageId = package.PackageId,
                PackageIds = package.Package_Products?.Select(pp => pp.PackageId).ToList() ?? new List<int>()
            };
            return Ok(packageDTOs);
        }

        // POST localhost:8080/api/Product/Add/[type]
        // returnerer OK eller bad request hvis sendte produkt er null
        [HttpPost("Add/{type}")]
        public async Task<IActionResult> AddProduct([FromBody] JsonElement body, [FromRoute] string type)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product? product = type switch
            {
                "BCD" => JsonSerializer.Deserialize<BCD>(body.GetRawText(), options),
                "DivingSuit" => JsonSerializer.Deserialize<DivingSuit>(body.GetRawText(), options),
                "Fin" => JsonSerializer.Deserialize<Fin>(body.GetRawText(), options),
                "RegulatorSet" => JsonSerializer.Deserialize<RegulatorSet>(body.GetRawText(), options),
                "Snorkel" => JsonSerializer.Deserialize<Snorkel>(body.GetRawText(), options),
                "Tank" => JsonSerializer.Deserialize<Tank>(body.GetRawText(), options),
                _ => null
            };
            if (product != null)
            {
                await _productRepository.AddNewProduct(product);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE localhost:8080/api/Product/[id]
        // returnerer OK, bad request hvis id er under 1, not found hvis produktet ikke findes. 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepository.GetProductById(id);

            if (id <= 0) return BadRequest();
            else if (product == null) return NotFound();

            await _productRepository.DeleteProduct(id);
            return Ok();
        }

        // PUT localhost:8080/api/Product/[id]
        // returnerer OK, badrequest hvis id er under 1, notfound hvis id'et ikke findes. 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] JsonElement body, int id)
        {
            if (id <= 0) return BadRequest();
            var productReplace = await _productRepository.GetProductById(id);
            if (productReplace == null) return NotFound();
            else
            {
                string type = productReplace.GetType().Name;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Product? product = type switch
                {
                    "BCD" => JsonSerializer.Deserialize<BCD>(body.GetRawText(), options),
                    "DivingSuit" => JsonSerializer.Deserialize<DivingSuit>(body.GetRawText(), options),
                    "Fin" => JsonSerializer.Deserialize<Fin>(body.GetRawText(), options),
                    "RegulatorSet" => JsonSerializer.Deserialize<RegulatorSet>(body.GetRawText(), options),
                    "Snorkel" => JsonSerializer.Deserialize<Snorkel>(body.GetRawText(), options),
                    "Tank" => JsonSerializer.Deserialize<Tank>(body.GetRawText(), options),
                    _ => null
                };
                
                if (product == null) return BadRequest();

                product.ProductId = id;

                await _productRepository.UpdateProduct(productReplace, product);
            }

            return Ok();
        }

    }
}
