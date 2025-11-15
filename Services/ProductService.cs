using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private string ApiUrl => CreateApiUrl("product");

        public List<Product>? GetProducts()
        {
            return GetJson<List<Product>>($"{ApiUrl}/list/");
        }

        public Product? GetProduct(int id)
        {
            return GetJson<Product>($"{ApiUrl}/{id}");
        }

        public Product? CreateProduct(Product product)
        {
            return PostJson<Product>($"{ApiUrl}/create", product);
        }

        public bool? DeleteProduct(Product product)
        {
            return DeleteJson<Product>($"{ApiUrl}/delete/{product.Id}", null);
        }

        public bool? EditProduct(Product product)
        {
            return PutJson<Product>($"{ApiUrl}/edit", product);
        }


    }
}
