namespace Fintech.Domain.Builders;

public interface IUserBuilder
{
    void SetId(string id);
    void SetFirstName(string firstName);
    void SetLastName(string lastName);
    void SetEmail(string email);
    void SetPhoneNumber(string phoneNumber);
    void SetCountry(string country);
    void SetDateOfBirth(DateTime dateOfBirth);

    Dictionary<string,object> GetResult();
}