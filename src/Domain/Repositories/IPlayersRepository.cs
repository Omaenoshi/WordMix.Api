namespace WordMix.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Entities;

public interface IPlayersRepository
{
    Task InsertAsync(Player player, CancellationToken cancellationToken);
    
    Task<Player?> GetByIdAsync(long id, CancellationToken cancellationToken);
    
    Task<Player?> GetByUserIdAsync(long userId, CancellationToken cancellationToken);
    
    Task<Player[]> GetOrderByScore(int limit, CancellationToken cancellationToken);
    
    Task UpdateAsync(Player player, CancellationToken cancellationToken);
}