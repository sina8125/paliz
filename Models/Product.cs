using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace BlazorApp2.Models
{
    public class Product : ICloneable
    {
        private readonly ILogger log;
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public string? SupplierName { get; set; }
        public double Quantity { get; set; }
        public int Price { get; set; }
        public string? Email { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        //public IBrowserFile ProductImage { get; set; }
        public string ProductImageName { get; set; }
        public string? ProductImagePhysicalName { get; set; }

        //public IBrowserFile? CatalogImage { get; set; } = null;
        public string? CatalogImageName { get; set; } = null;


        public object Clone()
        {
            //log.Information("Cloning product: {ProductName}", this.ProductName);
            return new Product
            {
                Id = this.Id,
                ProductName = this.ProductName,
                Category = this.Category,
                BrandName = this.BrandName,
                SupplierName = this.SupplierName,
                Quantity = this.Quantity,
                Price = this.Price,
                Email = this.Email,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                //ProductImage = this.ProductImage,
                ProductImageName = this.ProductImageName,
                ProductImagePhysicalName = this.ProductImagePhysicalName,
                //CatalogImage = this.CatalogImage,
                CatalogImageName = this.CatalogImageName
            };
        }

    }

}
