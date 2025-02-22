using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using System.Net.Mail;

namespace BlogApi.Services;

public class EmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "DevFabio",
        string fromEmail = "fabio@test.com")
    {
        try
        {
            SendUsingAnyOtherEmailService(
                toName,
                toEmail,
                subject,
                body,
                fromName,
                fromEmail);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private void SendUsingAnyOtherEmailService(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName,
        string fromEmail)
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port)
        {
            Credentials = new NetworkCredential(Configuration.Smtp.Username, Configuration.Smtp.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mail.To.Add(new MailAddress(toEmail, toName));

        smtpClient.Send(mail);
    }

    private async Task SendUsingSendGrid(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName,
        string fromEmail)
    {
        var client = new SendGridClient(Configuration.Smtp.Password);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(toEmail, toName);
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        await client.SendEmailAsync(msg);
    }
}
