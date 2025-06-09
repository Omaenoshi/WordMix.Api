namespace WordMix.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Domain.Entities;
using Domain.Repositories;

public class WordsRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IWordsRepository
{
    public Task<Word[]> GetRandomWordsAsync(int limit, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<Word?> GetWordByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}