using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Models;


namespace SimpleEcommerce.Data;
public class AppDbContext : DbContext
{
    public DbSet<BrandModel> Brands { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<SkuModel> Skus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=app.db;Cache=Shared");
}
