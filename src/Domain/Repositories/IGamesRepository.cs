namespace WordMix.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Entities;

public interface IGamesRepository
{
    Task InsertAsync(Game game, CancellationToken cancellationToken);
    
    Task<Game?> GetByIdAsync(long id, CancellationToken cancellationToken);
}