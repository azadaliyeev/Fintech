namespace Fintech.Domain.Entities;

public class VerificationToken
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
    public bool IsUsed { get; set; }

    public virtual User User { get; set; }
}

