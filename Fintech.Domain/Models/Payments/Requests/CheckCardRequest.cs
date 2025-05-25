namespace Fintech.Domain.Models.Payments.Requests;

public class CheckCardRequest(string toPan, string fromPan)
{
    public CheckCardRequest() :this(string.Empty, string.Empty)
    {
        
    }
    public string ToPan { get; set; } = toPan;
    public string FromPan { get; set; } = fromPan;
}