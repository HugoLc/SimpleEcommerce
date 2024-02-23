using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Data.Mappings;

public class BrandMap : IEntityTypeConfiguration<BrandModel>
{
    public void Configure(EntityTypeBuilder<BrandModel> builder)
    {
        builder.ToTable("Brand");
        builder.HasKey(category=>category.BrandId);
        builder.Property(category=>category.BrandId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
    }
}