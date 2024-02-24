
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(product=>product.ProductId);
        builder.Property(product=>product.ProductId)
            .ValueGeneratedOnAdd();

        builder.Property(product => product.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

        builder.Property(product => product.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

        builder.HasIndex(product => product.Slug, "IX_Category_Slug")
                .IsUnique();

        builder.HasOne(product => product.Brand)
                .WithMany(brand => brand.Products)
                .HasConstraintName("FK_Brand_Product")
                .OnDelete(DeleteBehavior.Cascade);        
    }
}