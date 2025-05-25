using Fintech.Domain.Models.Payments.Requests;
using Fintech.Domain.Models.Payments.Responses;
using Fintech.Shared.ServiceResults;

namespace Fintech.Domain.Services.Payments;

public interface IPaymentCardToCard
{
    Task<ServiceResult<PaymentCardToCardResponse>> CardToCardProcessAsync(PaymentCardToCardRequest request);
}