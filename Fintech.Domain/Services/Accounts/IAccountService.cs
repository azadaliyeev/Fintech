using Fintech.Domain.Entities;
using Fintech.Domain.Models.Account;
using Fintech.Domain.Models.Account.Block;
using Fintech.Domain.Models.Account.Create;
using Fintech.Domain.Models.Account.Dto;
using Fintech.Domain.Models.Account.FilteredRequest;
using Fintech.Domain.Models.Account.Inactive;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Accounts;

public interface IAccountService
{
    Task CreateInAsync(string userId);
    Task<ServiceResult<AccountDto>> CreateAsync(CreateAccountRequest request);
    string GenerateIban();
    string GenerateAccountNumber();
    Task<ServiceResult<AccountDto>> GetAccountByIbanAsync(string iban);
    Task<ServiceResult<List<AccountDto>>> GetAccountByPagination(int pageNumber, int pageSize);
    Task<ServiceResult<Dictionary<string, object>>> GetAccountByFilter(AccountFilteredRequest request);

    Task<ServiceResult> InactiveAccountAsync(InactiveAccountRequest request);
    Task<ServiceResult<AccountWithCard>> GetWithCardByUserIdAsync(string userId);

    Task<ServiceResult> BlockAccountAsync(BlockAccountRequest request);

    bool IsValidIban(string iban);
}