using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            productId => productId.Value,
            value => new ProductId(value));

        builder.Property(x => x.Sku).HasConversion(
            sku => sku.Value,
            value => Sku.Create(value)!);

        builder.OwnsOne(x => x.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency).HasMaxLength(3);
        });

    }
}
