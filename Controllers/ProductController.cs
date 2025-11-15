using BlazorApp2.Models;
using BlazorApp2.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(Serilog.ILogger _logger, IProductQueryService _productQuery) : Controller
    {
        //private static List<Product> products = new();
        //private readonly Serilog.ILogger _logger;

        //public ProductController(Serilog.ILogger logger)
        //{
        //    _logger = logger;
        //}


        [HttpGet("list")]
        public async Task<IActionResult> ProductsList()
        {
            _logger.Information("get all products");
            return Ok(await _productQuery.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            _logger.Information($"get product with id {id}");
            return Ok(await _productQuery.GetProduct(id));
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            _logger.Information($"create product {product.ProductName}");
            //product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            return Ok(await _productQuery.AddProduct(product));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _productQuery.DeleteProduct(id));
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(Product product)
        {
            return Ok(await _productQuery.UpdateProduct(product));
        }
    }
}
