namespace WordMix.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Entities;

public interface IPlayerRepository
{
    Task InsertAsync(Player player, CancellationToken cancellationToken);
}