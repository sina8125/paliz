using BlazorApp2.Models;
using BlazorApp2.Repository;

namespace BlazorApp2.Services
{
    public class ProductQueryService(IProductRepository _productRepository) : IProductQueryService
    {
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }
        public async Task<Product> GetProduct(int productId)
        {
            return await _productRepository.GetProduct(productId);
        }

        public async Task<Product> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProduct(productId);
        }
    }
}
