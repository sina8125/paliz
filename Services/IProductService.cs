using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public interface IProductService
    {

        public List<Product>? GetProducts();
        public Product? GetProduct(int id);
        public Product? CreateProduct(Product product);
        public bool? DeleteProduct(Product product);
        public bool? EditProduct(Product product);

    }
}
