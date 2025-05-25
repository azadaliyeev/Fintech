using Fintech.Domain.Repositories;

namespace Fintech.Domain.UnitOfWork;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public IUserRepository UserRepository { get; }

    public ICardRepository CardRepository { get; }
    

    public IInvoiceRepository InvoiceRepository { get; }

    public ITransactionRepository TransactionRepository { get; }

    public IVerificationTokenRepository VerificationTokenRepository { get; }

    public ICurrencyRepository CurrencyRepository { get; }
    int Commit();
    Task<int> CommitAsync();

    void BeginTransaction(bool isDapper = false);
    void RollbackTransaction(bool isDapper = false);
    void CommitTransaction(bool isDapper = false);
}