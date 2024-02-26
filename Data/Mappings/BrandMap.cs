using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class BrandMap : IEntityTypeConfiguration<BrandModel>
{
    public void Configure(EntityTypeBuilder<BrandModel> builder)
    {
        builder.ToTable("Brand");
        builder.HasKey(brand=>brand.BrandId);
        builder.Property(brand=>brand.BrandId)
            .ValueGeneratedOnAdd();

        builder.Property(brand => brand.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
    }
}