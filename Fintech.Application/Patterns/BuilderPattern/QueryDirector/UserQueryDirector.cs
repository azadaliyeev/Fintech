using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Patterns.BuilderPatterns.QueryBuilder;
namespace Fintech.Application.Patterns.BuilderPattern.QueryDirector;

public class UserQueryDirector(IUserQueryBuilder builder)
{
    private readonly IUserQueryBuilder _builder = builder;

    public IQueryable<User> MakeQuery(UserDto request, IQueryable<User> query)
    {
        _builder.SetQuery(query);
        return _builder.WithUserId(request.Id)
            .WithFirstName(request.FirstName)
            .WithLastName(request.LastName)
            .WithEmail(request.Email)
            .WithPhoneNumber(request.PhoneNumber)
            .WithCountry(request.Country)
            .WithDateOfBirth(request.DateOfBirth)
            .Build();
    }
}