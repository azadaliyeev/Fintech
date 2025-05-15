using System.Dynamic;
using System.Net;
using AutoMapper;
using Fintech.Application.Directors;
using Fintech.Application.ExceptionHandler;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Account.Block;
using Fintech.Domain.Models.Account.Create;
using Fintech.Domain.Models.Account.Dto;
using Fintech.Domain.Models.Account.FilteredRequest;
using Fintech.Domain.Models.Account.Inactive;
using Fintech.Domain.Services.Accounts;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using Fintech.Shared.ServiceResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Application.Services.AccountService;

public class AccountService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    AccountDirector accountDirector)
    : IAccountService
{
    public async Task<ServiceResult> BlockAccountAsync(BlockAccountRequest request)
    {
        var acc = await unitOfWork.AccountRepository.GetByIbanAsync(request.Iban);

        if (acc is null)
            return ServiceResult.Fail(ErrorMessages.NoAnyAccountWithThisIban.GetMessage(), HttpStatusCode.NotFound);

        if (acc.Status == Status.Blocked.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyBlocked.GetMessage());

        acc.Status = Status.Blocked.Get();
        unitOfWork.AccountRepository.UpdateAsync(acc);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }


    public async Task<ServiceResult> InactiveAccountAsync(InactiveAccountRequest request)
    {
        var acc = await unitOfWork.AccountRepository.Where(x => x.UserId == request.UserId && x.Iban == request.Iban)
            .FirstOrDefaultAsync();

        if (acc is null)
            return ServiceResult.Fail(ErrorMessages.UserNotExistsId.GetMessage(), HttpStatusCode.NotFound);

        if (acc.Status == Status.Inactive.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyInactive.GetMessage());

        acc.Status = Status.Inactive.Get();
        unitOfWork.AccountRepository.UpdateAsync(acc);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<Dictionary<string, object>>> GetAccountByFilter(AccountFilteredRequest request)
    {
        var acc = await unitOfWork.AccountRepository.Where(x => x.Iban == request.Iban).FirstOrDefaultAsync();

        if (acc is null)
            return ServiceResult<Dictionary<string, object>>.Fail(ErrorMessages.NotValidIban.GetMessage(),
                HttpStatusCode.NotFound);

        accountDirector.Build(request.Fields, acc);
        var result = accountDirector.GetResult();
        return ServiceResult<Dictionary<string, object>>.Success(result);
    }

    public async Task<ServiceResult<AccountWithCard>> GetWithCardByUserIdAsync(string userId)
    {
        var acc = await unitOfWork.AccountRepository.Where(x => x.UserId == userId)
            .Include(x => x.User.Cards).AsNoTracking().FirstOrDefaultAsync();

        if (acc is null)
            return ServiceResult<AccountWithCard>.Fail(ErrorMessages.UserNotExistsId.GetMessage(),
                HttpStatusCode.NotFound);

        var accWithCard = mapper.Map<AccountWithCard>(acc);

        return ServiceResult<AccountWithCard>.Success(accWithCard);
    }

    public async Task<ServiceResult<AccountDto>> CreateAsync(CreateAccountRequest request)
    {
        if (!await unitOfWork.UserRepository.ExistByIdAsync(request.UserId))
            return ServiceResult<AccountDto>.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        var acc = await FillAccount(request);
        await unitOfWork.AccountRepository.AddAsync(acc);
        await unitOfWork.CommitAsync();

        var accAsDto = mapper.Map<AccountDto>(acc);

        return ServiceResult<AccountDto>.SuccessAsCreated(accAsDto,
            $"/api/accounts/{acc.Id}");
    }

    public async Task CreateInAsync(string userId)
    {
        var account = new Account()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            Iban = GenerateIban(),
            CreateDate = DateTime.UtcNow
        };

        await unitOfWork.AccountRepository.AddAsync(account);
    }

    public string GenerateIban()
    {
        var bankAccountNumber = GenerateAccountNumber();
        var bankCode = "FNTC";
        var countryCode = "AZ";

        //var initialIban = countryCode + "00" + bankCode + bankAccountNumber;
        string countryCodeNumeric = "";

        foreach (var c in countryCode)
        {
            int number = c - 'A' + 10;
            countryCodeNumeric += number.ToString();
        }

        string bankCodeNumeric = "";

        foreach (var c in bankCode)
        {
            int number = c - 'A' + 10;
            bankCodeNumeric += number.ToString();
        }


        string rearrangedIban = bankCodeNumeric + bankAccountNumber + countryCodeNumeric + "00";

        long result = 0;
        foreach (char digit in rearrangedIban)
        {
            result = (result * 10 + (digit - '0')) % 97;
        }


        int checkDigit = (int)(98 - result);

        string checkDigitStr = checkDigit.ToString("D2");

        var finalIban = countryCode + checkDigitStr + bankCode + bankAccountNumber;

        return finalIban;
    }

    public string GenerateAccountNumber()
    {
        Random random = new Random();
        // ReSharper disable once NotAccessedVariable
        string accountNumber = "";

        for (int i = 0; i < 20; i++)
            accountNumber += random.Next(0, 10).ToString();

        return accountNumber;
    }

    public async Task<ServiceResult<AccountDto>> GetAccountByIbanAsync(string iban)
    {
        if (!IbanValidator.IsValid(iban))
            throw new NotValidIbanException(ErrorMessages.NotValidIban.GetMessage());

        var acc = await unitOfWork.AccountRepository.Where(x => x.Iban == iban).FirstOrDefaultAsync();

        if (acc is null)
            return ServiceResult<AccountDto>.Fail(ErrorMessages.NoAnyAccountWithThisIban.GetMessage(),
                HttpStatusCode.NotFound);

        var accAsDto = mapper.Map<AccountDto>(acc);

        return ServiceResult<AccountDto>.Success(accAsDto);
    }

    private async Task<Account> FillAccount(CreateAccountRequest request)
    {
        if (request.AccountType == AccountType.Master)
        {
            var acc = await unitOfWork.AccountRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.AccountType == AccountType.Master.Get());

            acc!.AccountType = AccountType.Default.Get();
            unitOfWork.AccountRepository.UpdateAsync(acc);
        }


        return new Account()
        {
            Id = Guid.NewGuid().ToString(),
            Iban = GenerateIban(),
            Balance = request.Balance,
            AccountType = request.AccountType.ToString(),
            Currency = request.Currencies.ToString(),
            UserId = request.UserId,
            CreateDate = DateTime.UtcNow
        };
    }

    public async Task<ServiceResult<List<AccountDto>>> GetAccountByPagination(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var accounts = await unitOfWork.AccountRepository.GetAll().Skip(skip)
            .Take(pageSize).ToListAsync();

        if (accounts.Count == 0)
        {
            return ServiceResult<List<AccountDto>>.Fail("No accounts found", HttpStatusCode.NotFound);
        }

        var accountsAsDto = mapper.Map<List<AccountDto>>(accounts);

        return ServiceResult<List<AccountDto>>.Success(accountsAsDto);
    }
}