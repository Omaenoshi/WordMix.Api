namespace WordMix.Domain.Services;

using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Extensions.Options;
using Options;

public class EmailService(IOptions<SmtpSettings> smtpOptions) : IEmailService
{
    public async Task SendAsync(string email, string subject, string body, CancellationToken cancellationToken)
    {
        var smtpSettings = smtpOptions.Value;
        using var message = new MailMessage(smtpSettings.FromEmail, email, subject, body);
        message.IsBodyHtml = false; 

        using var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port);
        client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
        client.EnableSsl = smtpSettings.EnableSsl;
        
        await Task.Run(() => client.Send(message), cancellationToken);
    }
}