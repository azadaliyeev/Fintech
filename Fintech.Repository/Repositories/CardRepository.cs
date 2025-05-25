using System.Data;
using System.Linq.Expressions;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.Payments.Responses;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;
using Fintech.Shared.Enums;
using Fintech.Shared.Extension;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.Repositories;

public class CardRepository(FintechDbContext context, Lazy<IDbConnection> connection, IDbTransaction transaction)
    : BaseSqlRepository(context, connection, transaction), ICardRepository
{
    public async Task AddAsync(Card card) => await Context.Cards.AddAsync(card);


    public void Update(Card card) => Context.Update(card);

    public void DeleteAsync(Card card) => Context.Remove(card);

    public async Task<Card?> GetByIdAsync(string id) => await Context.Cards.FindAsync(id);

    public IQueryable<Card> GetAll() =>
        Context.Cards.AsQueryable();

    public IQueryable<Card> Where(Expression<Func<Card, bool>> predicate) => Context.Cards.Where(predicate);
    public async Task<Card?> GetByPanAsync(string pan) => await Context.Cards.FirstOrDefaultAsync(x => x.Pan == pan);

    public IQueryable<CheckCardResponse> CheckCards(string toPan, string fromPan)
    {
        try
        {
            var cards = Context.Set<CheckCardResponse>().FromSqlInterpolated($@"
                 SELECT p.pan,
                        CASE
                            WHEN c.pan IS NULL THEN 'Not Exist Pan'
                            WHEN c.status = {Status.Active.Get()} THEN c.status
                            ELSE c.status
                        END AS Message
                 FROM (SELECT {toPan} AS pan
                       UNION
                       SELECT {fromPan}) p
                 LEFT JOIN cards c ON p.pan = c.pan;
             ");
            return cards;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
  
    }

    public async Task BlockCardsByUserIdAsync(string userId, string status)
    {
        await Context.Cards.Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
    }
}