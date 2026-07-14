using BookStore.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace BookStore.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _emailSettings = options.Value;
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_emailSettings.Email),
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        // Sending
        using var smtp = new SmtpClient();

        smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
