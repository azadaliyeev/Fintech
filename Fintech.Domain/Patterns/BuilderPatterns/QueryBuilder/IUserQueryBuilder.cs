using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Models.User.FilteredRequest;

namespace Fintech.Domain.Patterns.BuilderPatterns.QueryBuilder;

public interface IUserQueryBuilder
{
    IUserQueryBuilder SetQuery(IQueryable<User> query);
    IUserQueryBuilder WithUserId(string userId);
    IUserQueryBuilder WithFirstName(string firstName);
    IUserQueryBuilder WithLastName(string lastName);
    IUserQueryBuilder WithEmail(string email);
    IUserQueryBuilder WithPhoneNumber(string phoneNumber);
    IUserQueryBuilder WithCountry(string country);
    IUserQueryBuilder WithDateOfBirth(DateTime? dateOfBirth);
    IQueryable<User> Build();
}