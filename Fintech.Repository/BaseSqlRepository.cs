using System.Data;
using Dapper;
using Fintech.Repository.DbCotext;

namespace Fintech.Repository;

public class BaseSqlRepository(
    FintechDbContext context,
    Lazy<IDbConnection> connection,
    IDbTransaction? transaction = null)
{
    
    protected readonly FintechDbContext Context = context;

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var con = connection.Value;
        var queryResult = await con.QueryAsync<T>(sql, parameters, transaction);
        con.Close();
        return queryResult;
    }
    
    protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var con = connection.Value;
        var queryResult = await connection.Value.ExecuteAsync(sql, parameters, transaction);
        con.Close();
        return queryResult;
    }
}