
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(category=>category.ProductId);
        builder.Property(category=>category.ProductId)
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
        //TODO: ajustar many to many
        builder.HasMany(product => product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    post => post
                        .HasOne<CategoryModel>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostRole_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
    }
}