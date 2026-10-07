using DiveDeepAPI.Models;

namespace DiveDeepAPI.Repository
{
    public interface IProductRepository
    {

        Task<List<Product>> GetAllProducts();

        Task<Product?> GetProductById(int id);

        Task<List<Package>> GetAllPackages();

        Task<Package?> GetPackageById(int id);

        Task AddNewProduct(Product product);

        Task DeleteProduct(int id);

        Task UpdateProduct(Product UpdateProduct);
    }
}
