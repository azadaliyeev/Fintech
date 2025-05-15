using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");
        builder.HasKey(x => x.Id).HasName("id");
        builder.Property(c => c.Id).HasColumnName("id").IsRequired().HasMaxLength(100);
        builder.Property(c => c.Pan).HasColumnName("pan").IsRequired().HasMaxLength(16);
        builder.HasIndex(c => c.Pan).IsUnique();
        builder.Property(c => c.Cvv).HasColumnName("cvv").IsRequired().HasMaxLength(3);
        builder.Property(c => c.Currency).HasColumnName("currency").IsRequired().HasMaxLength(5).HasDefaultValue("AZN");
        builder.Property(c => c.CardName).HasColumnName("card_name").IsRequired();
        builder.HasIndex(c => c.CardName).IsUnique();
        builder.Property(c => c.CardType).HasColumnName("card_type").IsRequired().HasMaxLength(20);
        builder.Property(c => c.CardBrand).HasColumnName("card_brand").IsRequired().HasMaxLength(50);
        builder.Property(c => c.CreateDate).HasColumnName("create_date").HasDefaultValueSql("now()");
        builder.Property(c => c.ExpireDate).HasColumnName("expire_date")
            .HasDefaultValueSql("now() + interval '3 years'");
        builder.Property(c => c.Balance).HasColumnName("balance").HasDefaultValue(0);
        builder.Property(c=>c.Status).HasColumnName("status").IsRequired().HasDefaultValue("active");
        builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();

        // Add any additional configuration here

        builder.HasOne(x => x.User)
            .WithMany(x => x.Cards)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}