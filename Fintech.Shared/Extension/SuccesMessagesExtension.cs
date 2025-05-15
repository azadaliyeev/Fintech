using Fintech.Shared.Enums;

namespace Fintech.Shared.Extension;

public static class SuccesMessagesExtension
{
    public static string Get(this SuccesMessages succesMessages)
    {
        return succesMessages switch
        {
            SuccesMessages.UpdatedSuccessfully => "Updated successfully",
            SuccesMessages.ResetPasswordEmailSent => "Reset password email sent successfully",
            SuccesMessages.UserVerifiedSuccess => "User verified successfully",
            SuccesMessages.PasswordUpdatedSuccess => "Password updated successfully",
            _ => throw new ArgumentOutOfRangeException(nameof(succesMessages), succesMessages, null)
        };
    }
}