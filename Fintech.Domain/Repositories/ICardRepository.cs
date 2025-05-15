using System.Linq.Expressions;
using Fintech.Domain.Entities;

namespace Fintech.Domain.Repositories;

public interface ICardRepository
{
    Task AddAsync(Card card);
    void Update(Card card);
    void DeleteAsync(Card card);
    Task<Card?> GetByIdAsync(string id);

    IQueryable<Card> Where(Expression<Func<Card, bool>> predicate);

    Task<Card?> GetByPanAsync(string pan);

    Task BlockCardsByUserIdAsync(string userId,string status);
}