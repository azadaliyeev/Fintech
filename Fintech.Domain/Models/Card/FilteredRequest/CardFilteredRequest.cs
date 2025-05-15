namespace Fintech.Domain.Models.Card.FilteredRequest;

public record CardFilteredRequest(string Pan, List<string>? Filters);