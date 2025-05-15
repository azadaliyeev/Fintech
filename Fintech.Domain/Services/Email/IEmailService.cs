namespace Fintech.Domain.Services.Email;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string email, string fullName, string token);

    Task SendResetPasswordEmailAsync(string email, string fullName, string token);
}