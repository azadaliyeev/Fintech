using Fintech.Domain.Models.Payments.Requests;
using Fintech.Domain.Models.Payments.Responses;
using Fintech.Domain.Services.Payments;
using Fintech.Shared.ServiceResults;

namespace Fintech.Application.Services.Payments;

public class PaymentCardToCard: IPaymentCardToCard
{
    public Task<ServiceResult<PaymentCardToCardResponse>> CardToCardProcessAsync(PaymentCardToCardRequest request)
    {
            throw new NotImplementedException();
    }
}