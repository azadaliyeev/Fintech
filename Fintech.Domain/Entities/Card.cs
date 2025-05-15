namespace Fintech.Domain.Entities;

public class Card
{
    public string Id { get; set; } = null!;
    public string Pan { get; set; } = null!;
    public string Cvv { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public string CardName { get; set; } = null!;
    public string CardType { get; set; } = null!;
    public string CardBrand { get; set; } = null!;
    
    public string Status { get; set; } = null!;
    public decimal Balance { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public string UserId { get; set; } = null!;
    
    
    public User User { get; set; } = null!;
}