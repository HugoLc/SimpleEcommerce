using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryProductMap : IEntityTypeConfiguration<CategoryProductModel>
{
    public void Configure(EntityTypeBuilder<CategoryProductModel> builder)
    {
        builder.ToTable("CategoryProduct");
        builder.HasKey(cp => new { cp.ProductId, cp.CategoryId });
        
        builder.HasOne(cp => cp.Product)
            .WithMany(p => p.Categories)
            .HasForeignKey(cp => cp.ProductId);
        
        builder.HasOne(cp => cp.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(cp => cp.CategoryId);
        
        // Aqui você pode adicionar mais configurações específicas para CategoryProduct, se necessário
    }
}