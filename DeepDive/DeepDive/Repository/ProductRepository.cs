using DeepDive.Data;
using DeepDive.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Repository
{
    public class ProductRepository
    {
        private DeepDiveContext _deepDiveContext;

        public ProductRepository(DeepDiveContext deepDiveContext)
        {
            _deepDiveContext = deepDiveContext;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = _deepDiveContext.Products
                .Include(p => p.Product_Bookings)
                .ThenInclude(pb => pb.Booking).ToList();
            return products;
        }

        public Product? RemoveAmount (int id, int amount)
        {
            Product Tmproduct = GetProductById(id)!;
            Tmproduct.Amount -= amount;
            return Tmproduct;
        }

        public Product? GetProductById(int id)
        {
            Product product = _deepDiveContext.Products
                .Include(p => p.Product_Bookings)
                .ThenInclude(pb => pb.Booking)
                .Where(product => product.ProductId == id)
                .ToList()
                .First();


            return product;
        }

        public List<Package> GetAllPackages()
        {
            List<Package> packages = _deepDiveContext.Packages
                .Include(p => p.Package_Products)
                .ThenInclude(pp => pp.Product).ToList();

            return packages;
        }

        public Package? GetPackageById(int id)
        {
            Package package = _deepDiveContext.Packages
                 .Include(p => p.Package_Products)
                 .ThenInclude(pp => pp.Product)
                 .Where(package => package.PackageId == id)
                 .ToList()
                 .First();


            return package;
        }
    }
}