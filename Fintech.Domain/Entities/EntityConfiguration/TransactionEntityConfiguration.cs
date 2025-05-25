using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(t => t.Id).HasName("id");
        builder.Property(t=>t.TransactionId).HasColumnName("transaction_id").IsRequired().HasMaxLength(160);
        builder.Property(t => t.Id).HasColumnName("id").IsRequired().HasMaxLength(100);
        builder.Property(t => t.TransactionType).HasColumnName("transaction_type").IsRequired().HasMaxLength(8);
        builder.Property(t => t.TransactionStatus).HasColumnName("transaction_status").IsRequired().HasMaxLength(8);
        builder.Property(t => t.Currency).HasColumnName("currency").IsRequired().HasMaxLength(5);
        builder.Property(t => t.Amount).HasColumnName("amount").IsRequired();
        builder.Property(t => t.FromAccountId).HasColumnName("from_account_id").IsRequired().HasMaxLength(100);
        builder.Property(t => t.ToAccountId).HasColumnName("to_account_id").IsRequired().HasMaxLength(100);
        builder.Property(t => t.UserId).HasColumnName("user_id").IsRequired().HasMaxLength(100);
        // Add any additional configuration here
    }
}