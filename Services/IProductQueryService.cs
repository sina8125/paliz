using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public interface IProductQueryService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int productId);
    }
}
