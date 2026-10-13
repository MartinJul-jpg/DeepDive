using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Shared.Models;
using Shared.Services;

namespace DiveDeepAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IDTO_Service _DTO_Service;

        public ProductController(IProductRepository productRepository, IDTO_Service DTO_Service)
        {
            _productRepository = productRepository;
            _DTO_Service = DTO_Service;
        }

        // GET localhost:8080/api/Product
        // json med listen af alle produkter
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            // select to stop it from grabbing the product again 
            var productDTOs = products.Select(p => _DTO_Service.ProductToDTO(p));
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

            var productDTO = _DTO_Service.ProductToDTO(product);
            return Ok(productDTO);
        }

        // GET Product/Package
        // json med listen af alle pakker
        [HttpGet("Package")]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _productRepository.GetAllPackages();
            var packageDTOs = packages.Select(p => _DTO_Service.PackageToDTO(p));
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
            var packageDTO = _DTO_Service.PackageToDTO(package);
            return Ok(packageDTO);
        }

        // POST localhost:8080/api/Product/Add/[type]
        // returnerer OK eller bad request hvis sendte produkt er null
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO DTO)
        {
            Product product = _DTO_Service.DTO_ToProduct(DTO);

            if (product != null)
            {
                await _productRepository.AddNewProduct(product);
                return Ok();
            }

            return BadRequest();
        }

        // DELETE localhost:8080/api/Product/[id]
        // returnerer OK, bad request hvis id er under 1, not found hvis produktet ikke findes. 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            var product = await _productRepository.GetProductById(id);

            if (product == null) return NotFound();

            await _productRepository.DeleteProduct(id);
            return Ok();
        }

        // PUT localhost:8080/api/Product/[id]
        // returnerer OK, badrequest hvis id er under 1, notfound hvis id'et ikke findes. 
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO DTO)
        {
            if (DTO.ProductId <= 0) return BadRequest();
            var productReplace = await _productRepository.GetProductById(DTO.ProductId);
            if (productReplace == null) return NotFound();
            else
            {
                Product product = _DTO_Service.DTO_ToProduct(DTO);
                
                if (product == null) return BadRequest();

                try
                {
                    await _productRepository.UpdateProduct(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Conflict();
                }
            }

            return Ok();
        }

    }
}
