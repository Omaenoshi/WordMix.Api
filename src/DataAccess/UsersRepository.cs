namespace WordMix.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Domain.Entities;
using Domain.Repositories;

public class UsersRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IUsersRepository
{
    public Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<User?> GetByVerificationTokenAsync(string token, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}