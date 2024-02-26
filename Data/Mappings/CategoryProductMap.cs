using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryProductMap : IEntityTypeConfiguration<CategoryProductModel>
{
    public void Configure(EntityTypeBuilder<CategoryProductModel> builder)
    {
        builder.ToTable("CategoryProduct");
        builder.HasKey(cp => new { cp.ProductId, cp.CategoryId });
        
        builder.HasOne(cp => cp.Product)
            .WithMany(p => p.CategoryProduct)
            .HasForeignKey(cp => cp.ProductId);
        
        builder.HasOne(cp => cp.Category)
            .WithMany(c => c.CategoryProduct)
            .HasForeignKey(cp => cp.CategoryId);
    }
}