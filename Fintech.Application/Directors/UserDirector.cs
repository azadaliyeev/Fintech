using Fintech.Domain.Builders;
using Fintech.Domain.Entities;

namespace Fintech.Application.Directors;

public class UserDirector(IUserBuilder userBuilder)
{
    public void Build(List<string?>? filters, User user)
    {
        if (filters != null)
        {
            GetByFilter(filters, user);
            return;
        }

        userBuilder.SetId(user.Id);
        userBuilder.SetFirstName(user.FirstName!);
        userBuilder.SetLastName(user.LastName!);
        userBuilder.SetEmail(user.Email);
        userBuilder.SetPhoneNumber(user.PhoneNumber);
        userBuilder.SetCountry(user.Country!);
        userBuilder.SetDateOfBirth(user.DateOfBirth);
    }


    private void GetByFilter(List<string?> filters, User user)
    {
        foreach (var key in filters)
        {
            switch (key.ToLowerInvariant())
            {
                case "id":
                    userBuilder.SetId(user.Id);
                    break;
                case "firstname":
                    userBuilder.SetFirstName(user.FirstName!);
                    break;
                case "lastname":
                    userBuilder.SetLastName(user.LastName!);
                    break;
                case "email":
                    userBuilder.SetEmail(user.Email);
                    break;
                case "phonenumber":
                    userBuilder.SetPhoneNumber(user.PhoneNumber);
                    break;
                case "country":
                    userBuilder.SetCountry(user.Country!);
                    break;
                case "dateofbirth":
                    userBuilder.SetDateOfBirth(user.DateOfBirth);
                    break;
            }
        }
    }

    public Dictionary<string, object> GetResult() => userBuilder.GetResult();
}