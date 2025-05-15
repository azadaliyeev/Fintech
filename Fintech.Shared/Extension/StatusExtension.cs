using Fintech.Shared.Enums;

namespace Fintech.Shared.Extension;

public static class StatusExtension
{
    public static string Get(this Status status)
    {
        return status switch
        {
            Status.Active => "active",
            Status.Inactive => "inactive",
            Status.Blocked => "blocked",
            Status.Deleted => "deleted",
            Status.Expired => "expired",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}