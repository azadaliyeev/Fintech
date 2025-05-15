using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.HasKey(i => i.Id).HasName("id");
        builder.Property(i => i.Id).HasColumnName("id").IsRequired().HasMaxLength(100);
        builder.Property(i => i.CustomerName).HasColumnName("customer_name").IsRequired().HasMaxLength(50);
        builder.Property(i => i.Currency).HasColumnName("currency").HasMaxLength(5);
        builder.Property(i => i.IssueDate).HasColumnName("issue_date");
        builder.Property(i => i.DueDate).HasColumnName("due_date").IsRequired();
        builder.Property(i => i.ItemDescription).HasColumnName("item_description");
        builder.Property(i => i.SubTotal).HasColumnName("subtotal").IsRequired();
        builder.Property(i => i.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(i => i.Status).HasColumnName("status").IsRequired();

        // Add any additional configuration here
    }
}