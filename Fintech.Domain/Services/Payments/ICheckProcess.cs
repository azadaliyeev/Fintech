using Fintech.Domain.Models.Payments.Responses;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Payments;

public interface ICheckProcess
{
    Task<bool> HasUserVerificationAsync(string userId);
    Task<ServiceResult<List<CheckCardResponse>>> CheckCardAsync(string toPan, string fromPan);
    Task<ServiceResult<TransactionStatusResponse>> TransactionStatusAsync(string transactionId);

}