using System.Linq.Expressions;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Payments.Responses;

namespace Fintech.Domain.Repositories;

public interface ICardRepository
{
    Task AddAsync(Card card);
    void Update(Card card);
    void DeleteAsync(Card card);
    Task<Card?> GetByIdAsync(string id);
    IQueryable<Card> GetAll();
    IQueryable<Card> Where(Expression<Func<Card, bool>> predicate);

    Task<Card?> GetByPanAsync(string pan);

    IQueryable<CheckCardResponse> CheckCards(string toPan, string fromPan);

    Task BlockCardsByUserIdAsync(string userId, string status);
}