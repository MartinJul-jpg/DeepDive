using DeepDive.Data;
using DeepDive.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepDive.Repository
{
    public class ProductRepository
    {
        private BasketRepository _basketRepository;
        private readonly DeepDiveContext _deepDiveContext;

        public ProductRepository(BasketRepository basketRepository, DeepDiveContext deepDiveContext)
        {
            _basketRepository = basketRepository;
            _deepDiveContext = deepDiveContext;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = _deepDiveContext.Products
                .Include(p => p.Product_Bookings)
                .ThenInclude(pb => pb.Booking).ToList();
            return products;
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

        public void AddProductToBasket(int id)
        {
            if (_basketRepository.GetBasket() == null)
            {
                _basketRepository.ResetBasket();
            }

            _basketRepository.GetBasket().Product_Bookings.Add(new Product_Booking()
            {
                ProductId = id,
                Product = GetProductById(id),
                Booking = _basketRepository.GetBasket()
            });

            _basketRepository.GetBasket().CalculatePriceOfBooking();
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

        public void AddNewProduct(Product product)
        {
            _deepDiveContext.Products.Add(product);

            _deepDiveContext.SaveChanges();
        } 


        public void DeleteProduct(int productid)
        {
            var product = _deepDiveContext.Products.Find(productid);
            if (product != null)
            {
                _deepDiveContext.Products.Remove(product);
                _deepDiveContext.SaveChanges();
            }
        } 

        public void UpdateProduct(Product product)
        {


            _deepDiveContext.Products.Update(product);
            _deepDiveContext.SaveChanges();

        }
    }
}