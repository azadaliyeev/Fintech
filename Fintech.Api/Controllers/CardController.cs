using Fintech.Domain.Models.Card.Block;
using Fintech.Domain.Models.Card.Create;
using Fintech.Domain.Models.Card.FilteredRequest;
using Fintech.Domain.Models.Card.Inactive;
using Fintech.Domain.Models.Card.Verification;
using Fintech.Domain.Services.Cards;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.Api.Controllers;

public class CardController(ICardService cardService) : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCardRequest request)
        => CreateActionResult(await cardService.CreateAsync(request));

    [HttpGet]
    public async Task<IActionResult> GetCardByPan(string pan) =>
        CreateActionResult(await cardService.GetCardByPanAsync(pan));

    [HttpGet("verification")]
    public async Task<IActionResult> VerificationCard([FromQuery] CardVerificationRequest request) =>
        CreateActionResult(await cardService.VerificationCardAsync(request));

    [HttpGet("filter")]
    public async Task<IActionResult> GetCardsByFilter([FromQuery] CardFilteredRequest request) =>
        CreateActionResult(await cardService.GetCardByFilterAsync(request));

    [HttpPut("inactive")]
    public async Task<IActionResult> InactiveCard(InactiveCardRequest request) =>
        CreateActionResult(await cardService.InactiveCardAsync(request));

    [HttpPut("block")]
    public async Task<IActionResult> BlockCard(BlockCardRequest request) =>
        CreateActionResult(await cardService.BlockCard(request));
}