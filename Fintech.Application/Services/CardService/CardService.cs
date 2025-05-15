using System.Net;
using AutoMapper;
using Fintech.Application.Directors;
using Fintech.Application.ExceptionHandler;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Card;
using Fintech.Domain.Models.Card.Block;
using Fintech.Domain.Models.Card.Create;
using Fintech.Domain.Models.Card.FilteredRequest;
using Fintech.Domain.Models.Card.Inactive;
using Fintech.Domain.Models.Card.Verification;
using Fintech.Domain.Services.Cards;
using Fintech.Domain.Services.Users;
using Fintech.Domain.UnitOfWork;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Fintech.Shared.Helpers;
using Fintech.Shared.ServiceResults;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Application.Services.CardService;

public class CardService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IUserService userService,
    CardDirector cardDirector)
    : ICardService
{
    public async Task<ServiceResult> BlockCard(BlockCardRequest request)
    {
        var card = await unitOfWork.CardRepository.GetByPanAsync(request.Pan);

        if (card is null)
            return ServiceResult.Fail(ErrorMessages.CardNotExists.GetMessage(),
                HttpStatusCode.NotFound);

        if (card.Status == Status.Blocked.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyBlocked.GetMessage());

        card.Status = Status.Blocked.Get();
        unitOfWork.CardRepository.Update(card);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> InactiveCardAsync(InactiveCardRequest request)
    {
        var card = await unitOfWork.CardRepository.Where(x => x.UserId == request.UserId && x.Pan == request.Pan)
            .FirstOrDefaultAsync();

        if (card is null)
            return ServiceResult.Fail(ErrorMessages.NoAnyCardWithThisPan.GetMessage());

        if (card.Status == Status.Inactive.Get())
            return ServiceResult.Fail(ErrorMessages.AlreadyInactive.GetMessage());

        card.Status = Status.Inactive.Get();
        unitOfWork.CardRepository.Update(card);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<Dictionary<string, object>>> GetCardByFilterAsync(CardFilteredRequest request)
    {
        var card = await unitOfWork.CardRepository.Where(x => x.Pan == request.Pan).FirstOrDefaultAsync();

        if (card is null)
            return ServiceResult<Dictionary<string, object>>.Fail(ErrorMessages.NotValidPan.GetMessage());

        cardDirector.Build(request.Filters, card);

        var result = cardDirector.GetResult();

        return ServiceResult<Dictionary<string, object>>.Success(result);
    }

    public async Task<ServiceResult> VerificationCardAsync(CardVerificationRequest request)
    {
        var result = await unitOfWork.CardRepository.Where(x =>
                x.Pan == request.Pan && x.Cvv == request.Cvv && x.ExpireDate == request.ExpireDate)
            .AsNoTracking().AnyAsync();

        if (!result)
            return ServiceResult.Fail(ErrorMessages.CardNotExists.GetMessage(),
                HttpStatusCode.NotFound);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<CardDto>> CreateAsync(CreateCardRequest request)
    {
        var userExists = await userService.IsUserExistsAsync(request.UserId);

        if (!userExists)
            return ServiceResult<CardDto>.Fail(ErrorMessages.UserNotExistsId.GetMessage());

        var card = FillCard(request);

        await unitOfWork.CardRepository.AddAsync(card);
        await unitOfWork.CommitAsync();

        var cardDto = mapper.Map<CardDto>(card);

        return ServiceResult<CardDto>.SuccessAsCreated(cardDto, $"/api/card/{card.Id}");
    }

    public Card FillCard(CreateCardRequest request)
    {
        var card = mapper.Map<Card>(request);
        card.Id = Guid.NewGuid().ToString();
        card.Pan = PanGeneratorByBrand.GeneratePan(request.CardBrand.ToString());
        card.Cvv = GenerateCvv.Generate();
        return card;
    }

    public async Task<ServiceResult<CardDto>> GetCardByPanAsync(string pan)
    {
        if (!PanValidator.ValidatePan(pan))
            throw new NotValidPanException(ErrorMessages.NotValidPanFormat.GetMessage());

        var card = await unitOfWork.CardRepository.Where(x => x.Pan == pan).FirstOrDefaultAsync();

        if (card is null)
            return ServiceResult<CardDto>.Fail(ErrorMessages.NoAnyAccountWithThisIban.GetMessage(),
                HttpStatusCode.Unauthorized);

        var cardDto = mapper.Map<CardDto>(card);

        return ServiceResult<CardDto>.Success(cardDto);
    }
}