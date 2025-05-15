using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Iban).IsRequired().HasMaxLength(29).HasColumnName("iban");
        builder.Property(a => a.Balance).IsRequired().HasColumnName("balance");
        builder.Property(a => a.AccountType).IsRequired().HasMaxLength(20).HasColumnName("account_type").HasDefaultValue("Master");
        builder.Property(a => a.CreateDate).IsRequired().HasColumnName("create_date").HasDefaultValueSql("now()");
        builder.Property(a => a.Currency).IsRequired().HasMaxLength(3).HasColumnName("currency").HasDefaultValue("AZN");
        builder.Property(a => a.UserId).IsRequired().HasMaxLength(100).HasColumnName("user_id");
        builder.Property(a => a.Status).HasColumnName("status").HasDefaultValue("active");

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}