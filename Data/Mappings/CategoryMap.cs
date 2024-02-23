using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<CategoryModel>
{
    public void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(category=>category.CategoryId);
        builder.Property(category=>category.CategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
    }
}