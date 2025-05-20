using System.ComponentModel.DataAnnotations;

namespace Fintech.Domain.Entities;

public class VerificationToken(string id, string userId, string token, bool isUsed)
{
    public VerificationToken() : this(string.Empty, string.Empty, string.Empty, false)
    {
    }

    [StringLength(100)] public string Id { get; set; } = id;
    [StringLength(100)] public string UserId { get; set; } = userId;
    [StringLength(100)] public string Token { get; set; } = token;
    public DateTime? ExpireDate { get; set; }
    public bool IsUsed { get; set; } = isUsed;

    public virtual User User { get; set; } = null!;
}