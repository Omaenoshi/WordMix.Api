namespace WordMix.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Domain.Entities;
using Domain.Repositories;

public class PlayersRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IPlayersRepository
{
    public Task InsertAsync(Player player, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<Player?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<Player?> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<Player[]> GetOrderByScore(int limit, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(Player player, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}