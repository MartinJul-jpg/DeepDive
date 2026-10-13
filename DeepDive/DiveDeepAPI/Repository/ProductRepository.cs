using Microsoft.EntityFrameworkCore;
using Shared.Models;
using DiveDeepAPI.Data;
namespace DiveDeepAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DiveDeepContext _diveDeepContext;

        public ProductRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {

            List<Product> products = await _diveDeepContext.Products
                 .Include(p => p.Product_Bookings)
                 .ThenInclude(pb => pb.Booking).ToListAsync();
            return products;

        }

        public async Task<Product?> GetProductById(int id)
        {
            //Var for Null values 
            var product = await _diveDeepContext.Products
                .Include(p => p.Product_Bookings)
                .ThenInclude(pb => pb.Booking)
                .AsNoTracking()
                .SingleOrDefaultAsync(product => product.ProductId == id);
            return product;
        }

        public async Task<List<Package>> GetAllPackages()
        {
            List<Package> packages = await _diveDeepContext.Packages
                 .Include(p => p.Package_Products)
                 .ThenInclude(pp => pp.Product).ToListAsync();
            return packages;
        }

        public async Task<Package?> GetPackageById(int id)
        {
            //Var for Null values 
            var package = await _diveDeepContext.Packages
                 .Include(p => p.Package_Products)
                 .ThenInclude(pp => pp.Product)
                 .SingleOrDefaultAsync(package => package.PackageId == id);

            return package;
        }

        public async Task AddNewProduct(Product product)
        {
            await _diveDeepContext.Products.AddAsync(product);
            await _diveDeepContext.SaveChangesAsync();
        }


        public async Task DeleteProduct(int productid)
        {
            var product = _diveDeepContext.Products.Find(productid);
            if (product != null)
            {
                _diveDeepContext.Products.Remove(product);
                await _diveDeepContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProduct(Product updateProduct)
        {
            _diveDeepContext.Products.Update(updateProduct);

            await _diveDeepContext.SaveChangesAsync();
        }
    }
}