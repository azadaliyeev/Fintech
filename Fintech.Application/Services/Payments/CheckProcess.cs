using AutoMapper;
using Fintech.Domain.Models.Payments.Responses;
using Fintech.Domain.Services.Payments;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Application.Services.Payments;

public class CheckProcess(IUnitOfWork unitOfWork, IMapper mapper) : ICheckProcess
{
    public async Task<bool> HasUserVerificationAsync(string userId)
    {
        if (!unitOfWork.UserRepository.GetAll()
                .Any(x => x.Id == userId && x.IsVerified))
            return false;
        return true;
    }

    public async Task<ServiceResult<List<CheckCardResponse>>> CheckCardAsync(string toPan, string fromPan)
    {
        var result = unitOfWork.CardRepository.CheckCards(toPan, fromPan);
        var response = await result.ToListAsync();
        return ServiceResult<List<CheckCardResponse>>.Success(response);
    }

    public async Task<ServiceResult<TransactionStatusResponse>> TransactionStatusAsync(string transactionId)
    {
        var transaction = await unitOfWork.TransactionRepository.GetByTransactionId(transactionId);
        var transactionStatusResponse = mapper.Map<TransactionStatusResponse>(transaction);
        
        if (transactionStatusResponse is { TransactionId: "" })
            return ServiceResult<TransactionStatusResponse>.Fail("Transaction not found");
        
        return ServiceResult<TransactionStatusResponse>.Success(transactionStatusResponse);
    }
}