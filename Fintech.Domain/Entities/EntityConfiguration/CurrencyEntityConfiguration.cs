using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currencies");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.CurrencyCode).HasColumnName("currency_code").IsRequired().HasMaxLength(5);
        builder.Property(c => c.ExchangeRate).HasColumnName("exchange_rate").IsRequired();
        builder.Property(c => c.LastUpdated).HasColumnName("last_updated").HasDefaultValueSql("now()")
            .ValueGeneratedOnAddOrUpdate();
        builder.Property(c => c.BuyRate).HasColumnName("buy_rate").IsRequired();
        builder.Property(c => c.SellRate).HasColumnName("sell_rate").IsRequired();
    }
}