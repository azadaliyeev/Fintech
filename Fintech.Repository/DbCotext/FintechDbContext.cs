using System.Reflection;
using Fintech.Domain.Entities;
using Fintech.Domain.Entities.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.DbCotext;

public class FintechDbContext(DbContextOptions<FintechDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<VerificationToken> VerificationTokens { get; set; }

    public DbSet<Currency> Currencies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CardEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VerificationTokenEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyEntityConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FintechDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}