using Fintech.Domain.Models.Account.Block;
using Fintech.Domain.Models.Account.Create;
using Fintech.Domain.Models.Account.FilteredRequest;
using Fintech.Domain.Models.Account.Inactive;
using Fintech.Domain.Services.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Api.Controllers;

public class AccountController(IAccountService accountService) : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request) =>
        CreateActionResult(await accountService.CreateAsync(request));

    [HttpGet("{iban}")]
    public async Task<IActionResult> GetByIban(string iban) =>
        CreateActionResult(await accountService.GetAccountByIbanAsync(iban));

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetAccountsByPagination(int pageNumber, int pageSize) =>
        CreateActionResult(await accountService.GetAccountByPagination(pageNumber, pageSize));

    [HttpGet("withcard/{userId}")]
    public async Task<IActionResult> GetAccountsWithCardsByUserId(string userId) =>
        CreateActionResult(await accountService.GetWithCardByUserIdAsync(userId));

    [HttpGet("filtered")]
    public async Task<IActionResult> GetAccountsByFilter([FromQuery] AccountFilteredRequest request) =>
        CreateActionResult(await accountService.GetAccountByFilter(request));

    [HttpPut("inactive")]
    public async Task<IActionResult> InactiveAccount(InactiveAccountRequest request) =>
        CreateActionResult(await accountService.InactiveAccountAsync(request));


    [HttpPut("block")]
    public async Task<IActionResult> BlockAccount(BlockAccountRequest request) =>
        CreateActionResult(await accountService.BlockAccountAsync(request));
    
    
}