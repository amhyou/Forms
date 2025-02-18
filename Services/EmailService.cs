using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using forms.Data;
using forms.Models;

namespace forms.Services;

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

    public async Task SendFormCopyAsync(forms.Models.Form newForm, Template templateForm, string email)
    {
        string tableRows = string.Join("", newForm.Responses.Select(response =>
        $"<tr><td>{templateForm.Questions.FirstOrDefault(q => q.Id == response.QuestionId)?.Text}</td><td>{response.Answer}</td></tr>"
    ));

        string emailTemplate = @"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }
            .container { background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1); max-width: 600px; margin: auto; }
            h2 { color: #333; }
            .form-info { font-size: 16px; margin-bottom: 20px; color: #666; }
            .table-container { width: 100%; overflow-x: auto; }
            table { width: 100%; border-collapse: collapse; margin-top: 10px; }
            th, td { border: 1px solid #ddd; padding: 10px; text-align: left; }
            th { background-color: #007BFF; color: white; }
            .footer { margin-top: 20px; font-size: 14px; color: #888; text-align: center; }
        </style>
    </head>
    <body>

        <div class='container'>
            <h2>" + templateForm.Title + @"</h2>
            <p class='form-info'>" + templateForm.Description + @"</p>

            <div class='table-container'>
                <table>
                    <thead>
                        <tr>
                            <th>Question</th>
                            <th>Answer</th>
                        </tr>
                    </thead>
                    <tbody>
                        " + tableRows + @"
                    </tbody>
                </table>
            </div>

            <p class='footer'>Thank you for filling out this form!</p>
        </div>

    </body>
    </html>";

        var subject = "Your copy of the form";

        await SendEmailAsync(email, subject, emailTemplate);
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
