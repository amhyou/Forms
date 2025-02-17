using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using forms.Data;


public class EmailSettings
{
    public string? SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }
    public string? SmtpPassword { get; set; }
    public string? SenderEmail { get; set; }
    public string? SenderName { get; set; }
}

public class EmailService : IEmailSender<ApplicationUser>
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var subject = "Confirm your email";
        var body = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm</a>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var subject = "Reset your password";
        var body = $"Click the following link to reset your password: <a href='{resetCode}'>Reset Password</a>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        var subject = "Reset your password";
        var body = $"Click the following link to reset your password: <a href='{resetLink}'>Reset Password</a>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        emailMessage.To.Add(new MailboxAddress(email, email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            Console.WriteLine($"Attempting to send email to {email} ...");
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
            await client.SendAsync(emailMessage);
            Console.WriteLine($"Email sent successfully to {email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred.");
            Console.WriteLine($"Error details: {ex.Message}");
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
