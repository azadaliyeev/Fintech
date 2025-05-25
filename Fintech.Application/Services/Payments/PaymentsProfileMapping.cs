using AutoMapper;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Payments.Responses;

namespace Fintech.Application.Services.Payments;

public class PaymentsProfileMapping:Profile
{
    public PaymentsProfileMapping()
    {
        CreateMap<Transaction, TransactionStatusResponse>().ReverseMap();
    }
}