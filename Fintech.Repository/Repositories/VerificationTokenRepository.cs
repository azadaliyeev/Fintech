using System.Data;
using Fintech.Domain.Entities;
using Fintech.Domain.Repositories;
using Fintech.Repository.DbCotext;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Repository.Repositories;

public class VerificationTokenRepository(
    FintechDbContext context,
    Lazy<IDbConnection> connection,
    IDbTransaction transaction) : BaseSqlRepository(context, connection, transaction), IVerificationTokenRepository
{
    public async Task AddAsync(VerificationToken verificationToken) =>
        await Context.VerificationTokens.AddAsync(verificationToken);

    public void Update(VerificationToken verificationToken) => Context.VerificationTokens.Update(verificationToken);

    public void Delete(VerificationToken verificationToken) => Context.VerificationTokens.Remove(verificationToken);

    public async Task<VerificationToken?> GetByUserId(string userId) =>
        await Context.VerificationTokens.FindAsync(userId);

    public async Task<VerificationToken?> GetByTokenAsync(string token) =>
        await Context.VerificationTokens.FirstOrDefaultAsync(x => x.Token == token);

    public async Task DeleteTokensAysnc(List<string> usersId)
    {
        await Context.VerificationTokens.Where(x => usersId.Contains(x.UserId))
            .ExecuteDeleteAsync();
    }
}