namespace Fintech.Shared.Helpers;

// public class ConnectionStrings
// {
//    
//     public string DefaultConnection { get; set; } 
//
//     public ConnectionStrings()
//     {
//         
//     }
// }
public class ConnectionStringOption
{
    public const string Key = "ConnectionStrings";
    public string DefaultConnection { get; set; } = default!;
}