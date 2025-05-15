using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintech.Domain.Entities.EntityConfiguration;

public class VerificationTokenEntityConfiguration : IEntityTypeConfiguration<VerificationToken>
{
    public void Configure(EntityTypeBuilder<VerificationToken> builder)
    {
        builder.ToTable("verification_token");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.ExpireDate).HasColumnName("expire_date")
            .HasDefaultValueSql("now() + interval '15 minutes'");
        builder.Property(x => x.IsUsed).HasColumnName("is_used").HasDefaultValue(false);
        builder.Property(x => x.Token).HasColumnName("token").IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(x => x.VerificationToken)
            .HasForeignKey<VerificationToken>(x => x.UserId);
    }
}