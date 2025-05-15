namespace Fintech.Domain.Models.User.FilteredRequest;

public record UserFilteredRequest(string UserId, List<string?>? Fields);