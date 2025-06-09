namespace WordMix.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Domain.Entities;
using Domain.Repositories;

public class GamesRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IGamesRepository
{
    public Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<Game?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}