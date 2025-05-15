using System.Net;
using System.Net.Mail;
using Fintech.Domain.Entities;
using Fintech.Domain.Services.Email;


namespace Fintech.Application.Services.EmailService;

public class EmailService : IEmailService
{
    public async Task SendVerificationEmailAsync(string email, string fullName, string token)
    {
        var fromAdress = new MailAddress("azad-2000@mail.ru", "Fintech");
        var toAdress = new MailAddress(email, fullName);
        var fromPassword = "tppKQkgWj3DuZ5PmgdQn";
        string subject = "Verification Email";
        string link = $"http://localhost:5222/api/User/verify/{token}";
        string body = $"Hello {fullName}, Please click on the link to verify your email: <a href=\"{link}\">{link}</a>";


        var smptp = new SmtpClient
        {
            Host = "smtp.mail.ru",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAdress.Address, fromPassword)
        };


        using (var message = new MailMessage(fromAdress, toAdress))
        {
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            try
            {
                await smptp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Error sending email", ex);
            }
        }
    }

    public async Task SendResetPasswordEmailAsync(string email, string fullName, string token)
    {
        var fromAdress = new MailAddress("azad-2000@mail.ru", "Fintech");
        var toAdress = new MailAddress(email, fullName);
        var fromPassword = "tppKQkgWj3DuZ5PmgdQn";
        string subject = "Verification Email";
        string link = $"https://localhost:7158/swagger/index.html";
        string body =
            $"Hello {fullName}, Please click on the link to reset your password: <a href=\"{link}\">{link}</a> , Token: {token}";


        var smptp = new SmtpClient
        {
            Host = "smtp.mail.ru",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAdress.Address, fromPassword)
        };


        using (var message = new MailMessage(fromAdress, toAdress))
        {
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            try
            {
                await smptp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Error sending email", ex);
            }
        }
    }
}