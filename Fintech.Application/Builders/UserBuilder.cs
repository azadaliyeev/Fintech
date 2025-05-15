using Fintech.Domain.Builders;

namespace Fintech.Application.Builders;

public class UserBuilder : IUserBuilder
{
    private Dictionary<string, object> _result = new();

    public void SetId(string id) => _result["Id"] = id;

    public void SetFirstName(string firstName) => _result["FirstName"] = firstName;

    public void SetLastName(string lastName) => _result["LastName"] = lastName;

    public void SetEmail(string email) => _result["Email"] = email;

    public void SetPhoneNumber(string phoneNumber) => _result["PhoneNumber"] = phoneNumber;

    public void SetCountry(string country) => _result["Country"] = country;

    public void SetDateOfBirth(DateTime dateOfBirth) => _result["DateOfBirth"] = dateOfBirth;

    public Dictionary<string,object> GetResult()
    {
        return _result;
    }
}