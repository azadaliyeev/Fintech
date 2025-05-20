using Fintech.Shared.Enums;

namespace Fintech.Domain.Models.Account.Create;

public record CreateAccountRequest(AccountType AccountType, Currencies Currencies, string UserId);