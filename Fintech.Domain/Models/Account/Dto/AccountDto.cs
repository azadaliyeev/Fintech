namespace Fintech.Domain.Models.Account.Dto;

public record AccountDto(string Id, string Iban, decimal Balance, string AccountType, string Currency, string Status);