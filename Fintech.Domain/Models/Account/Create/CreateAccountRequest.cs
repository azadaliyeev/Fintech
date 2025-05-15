using Fintech.Shared.Enums;

namespace Fintech.Domain.Models.Account.Create;

public record CreateAccountRequest(decimal Balance, AccountType AccountType, Shared.Enums.Currencies Currencies, string UserId);