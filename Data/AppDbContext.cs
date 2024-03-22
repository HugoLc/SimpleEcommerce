using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Data.Mappings;
using SimpleEcommerce.Models;


namespace SimpleEcommerce.Data;
public class AppDbContext : DbContext
{
    public DbSet<BrandModel> Brands { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<CategoryProductModel> CategoryProduct { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<SkuModel> Skus { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BrandMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new CategoryProductMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new SkuMap());
    }
}
