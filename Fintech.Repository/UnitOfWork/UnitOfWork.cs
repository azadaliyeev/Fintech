using System.Data;
using Fintech.Domain.Repositories;
using Fintech.Domain.UnitOfWork;
using Fintech.Repository.DbCotext;
using Fintech.Repository.Repositories;
using Fintech.Shared.Helpers;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Fintech.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ConnectionStringOption _connectionStrings;
    private readonly FintechDbContext _dbContext;
    private IDbTransaction _transaction;
    private IDbContextTransaction? _efTransaction;
    private readonly Lazy<IDbConnection> _lazyConnection;
    public IDbConnection Connection => _lazyConnection.Value;
    public IAccountRepository? _accountRepository;
    public IUserRepository? _userRepository;
    public ICardRepository _cardRepository;
    public IInvoiceRepository _invoiceRepository;
    public ITransactionRepository _transactionRepository;
    public IVerificationTokenRepository _verificationTokenRepository;
    public ICurrencyRepository _currencyRepository;

    public UnitOfWork(FintechDbContext context, IOptions<ConnectionStringOption> connectionString)
    {
        _connectionStrings = connectionString.Value;
        _dbContext = context;
        _lazyConnection = new Lazy<IDbConnection>(() =>
        {
            var conn = new NpgsqlConnection(_connectionStrings.DefaultConnection);
            conn.Open();
            return conn;
        });
    }

    public IAccountRepository AccountRepository =>
        _accountRepository ??= new AccountRepository(_dbContext, _lazyConnection, _transaction);

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_dbContext, _lazyConnection, _transaction);

    public ICardRepository CardRepository =>
        _cardRepository ??= new CardRepository(_dbContext, _lazyConnection, _transaction);

    public IInvoiceRepository InvoiceRepository =>
        _invoiceRepository ??= new InvoiceRepository(_dbContext, _lazyConnection, _transaction);

    public ITransactionRepository TransactionRepository =>
        _transactionRepository ??= new TransactionRepository(_dbContext, _lazyConnection, _transaction);

    public IVerificationTokenRepository VerificationTokenRepository =>
        _verificationTokenRepository ??= new VerificationTokenRepository(_dbContext, _lazyConnection, _transaction);

    public ICurrencyRepository CurrencyRepository =>
        _currencyRepository ??= new CurrencyRepository(_dbContext, _lazyConnection, _transaction);


    public int Commit() =>
        _dbContext.SaveChanges();

    public Task<int> CommitAsync() =>
        _dbContext.SaveChangesAsync();

    public void BeginTransaction(bool isDapper = false)
    {
        if (isDapper)
            _transaction ??= Connection.BeginTransaction();
        else
            _efTransaction ??= _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction(bool isDapper = false)
    {
        try
        {
            if (isDapper)
                _transaction?.Commit();
            else
                _efTransaction?.Commit();
        }
        catch
        {
            RollbackTransaction(isDapper);
            throw;
        }
        finally
        {
            DisposeTransactions(isDapper);
        }
    }

    public void RollbackTransaction(bool isDapper = false)
    {
        if (isDapper)
            _transaction?.Rollback();
        else
            _efTransaction?.Rollback();
        DisposeTransactions(isDapper);
    }

    private void DisposeTransactions(bool isDapper = false)
    {
        if (isDapper)
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        else
        {
            _efTransaction?.Dispose();
            _efTransaction = null;
        }
    }

    public void Dispose()
    {
        DisposeTransactions(isDapper: true); // Dapper
        DisposeTransactions(isDapper: false); // EF Core

        if (_lazyConnection.IsValueCreated)
            _lazyConnection.Value.Dispose();

        _dbContext.Dispose();
    }
}