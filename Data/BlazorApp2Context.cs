using BlazorApp2.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Data
{
    public class BlazorApp2Context(DbContextOptions<BlazorApp2Context> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
