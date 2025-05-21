using System.Globalization;
using Fintech.Domain.Entities;
using Fintech.Domain.Models.User;
using Fintech.Domain.Patterns.BuilderPatterns.QueryBuilder;

namespace Fintech.Application.Patterns.BuilderPattern.QueryBuilder;

public class UserQueryBuilder : IUserQueryBuilder
{
    private IQueryable<User> _query;

    public IUserQueryBuilder SetQuery(IQueryable<User> query)
    {
        _query = query;
        return this;
    }

    public IUserQueryBuilder WithUserId(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
            _query = _query.Where(user => user.Id == userId);
        return this;
    }

    public IUserQueryBuilder WithFirstName(string firstName)
    {
        if (!string.IsNullOrEmpty(firstName))
            _query = _query.Where(user => user.FirstName == firstName);
        return this;
    }

    public IUserQueryBuilder WithLastName(string lastName)
    {
        if (!string.IsNullOrEmpty(lastName))
            _query = _query.Where(user => user.LastName == lastName);
        return this;
    }

    public IUserQueryBuilder WithEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _query = _query.Where(user => user.Email == email);
        return this;
    }

    public IUserQueryBuilder WithPhoneNumber(string phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber))
            _query = _query.Where(user => user.PhoneNumber == phoneNumber);
        return this;
    }

    public IUserQueryBuilder WithCountry(string country)
    {
        if (!string.IsNullOrEmpty(country))
            _query = _query.Where(user => user.Country == country);
        return this;
    }

    public IUserQueryBuilder WithDateOfBirth(DateTime? dateOfBirth)
    {
        if (dateOfBirth.HasValue)
            _query = _query.Where(user => user.DateOfBirth == dateOfBirth);
        return this;
    }

    public IQueryable<User> Build()
    {
        return _query;
    }
}