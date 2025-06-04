namespace WordMix.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Entities;

public interface IUserRepository
{
    Task InsertAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}