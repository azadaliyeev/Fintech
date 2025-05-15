using Fintech.Shared.Enums;

namespace Fintech.Shared.Extension;

public static class ErrorMessageExtensions
{
    public static string GetMessage(this ErrorMessages error)
    {
        return error switch
        {
            ErrorMessages.PhoneNumberExists => "Phone number already exists.",
            ErrorMessages.EmailExists => "Email already exists.",
            ErrorMessages.InvalidPassword => "Invalid password.",
            ErrorMessages.EmailNotExists => "No any user with this email.",
            ErrorMessages.UserNotExistsId => "User not exists with this id.",
            ErrorMessages.NoAnyAccountWithThisIban => "No Any account with this iban.",
            ErrorMessages.NotValidIban => "Not valid iban format.",
            ErrorMessages.NotValidPanFormat => "Not valid pan format.",
            ErrorMessages.NoAnyCardWithThisPan => "No any card with this pan.",
            ErrorMessages.UserAlreadyVerified => "User already verified.",
            ErrorMessages.UserNotVerified => "User email not verified.",
            ErrorMessages.NotValidPan => "Not valid pan.",
            ErrorMessages.AlreadyInactive => "Already inactive.",
            ErrorMessages.Null => "Can not be empty",
            ErrorMessages.CardNotExists => "Card not exists.",
            ErrorMessages.AlreadyBlocked => "Already blocked.",
            ErrorMessages.AlreadyDeleted => "Already deleted.",
            _ => "Unknown error."
        };
    }
}