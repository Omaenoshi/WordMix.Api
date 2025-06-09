namespace WordMix.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Entities;

public interface IWordsRepository
{
    Task<Word[]> GetRandomWordsAsync(int limit, CancellationToken cancellationToken);
    
    Task<Word?> GetWordByIdAsync(long id, CancellationToken cancellationToken);
}