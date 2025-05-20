using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.FirstName).HasMaxLength(50).HasColumnName("first_name");
        builder.Property(u => u.LastName).HasMaxLength(50).HasColumnName("last_name");
        builder.Property(u => u.Email).HasMaxLength(100).HasColumnName("email");
        builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15).HasColumnName("phone_number");
        builder.Property(u => u.Country).HasMaxLength(50).HasColumnName("country");
        builder.Property(u => u.DateOfBirth).HasColumnName("date_of_birth");
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100).HasColumnName("password");
        builder.Property(u => u.IsVerified).HasColumnName("verified");
        builder.Property(u => u.Status).HasColumnName("status").IsRequired().HasMaxLength(50).HasDefaultValue("active");
    }
}