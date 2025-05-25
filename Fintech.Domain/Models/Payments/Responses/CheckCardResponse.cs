using Microsoft.EntityFrameworkCore;

namespace Fintech.Domain.Models.Payments.Responses;

[Keyless]
public class CheckCardResponse(string pan, string message)
{
    public CheckCardResponse() : this(string.Empty, string.Empty)
    {
    }
    public string Pan { get; set; } = pan;
    public string Message { get; set; } = message;
}