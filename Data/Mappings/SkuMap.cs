
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class SkuMap : IEntityTypeConfiguration<SkuModel>
{
    public void Configure(EntityTypeBuilder<SkuModel> builder)
    {
        builder.ToTable("Sku");
        builder.HasKey(sku=>sku.SkuId);
        builder.Property(sku=>sku.SkuId)
            .ValueGeneratedOnAdd();

        builder.Property(sku => sku.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

        builder.Property(sku => sku.ImageUrl)
                .IsRequired()
                .HasColumnName("ImageUrl")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

        builder.Property(sku => sku.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("DECIMAL")
                .HasMaxLength(20);

        builder.Property(sku => sku.Stock)
                .IsRequired()
                .HasColumnName("Stock")
                .HasColumnType("INTEGER")
                .HasMaxLength(20);

        builder.HasIndex(sku => sku.ImageUrl, "IX_Sku_Image_Url")
                .IsUnique();

        builder.HasOne(sku => sku.Product)
                .WithMany(product => product.Skus)
                .HasConstraintName("FK_Product_Sku")
                .OnDelete(DeleteBehavior.Cascade);        
    }
}