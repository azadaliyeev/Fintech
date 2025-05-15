namespace Fintech.Domain.Models.Account.FilteredRequest;

public record AccountFilteredRequest(string Iban, List<string>? Fields);