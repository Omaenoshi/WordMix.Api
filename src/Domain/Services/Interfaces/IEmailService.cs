namespace WordMix.Domain.Services.Interfaces;

using System.Threading;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string body, CancellationToken cancellationToken);
}