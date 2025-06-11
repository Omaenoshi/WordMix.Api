namespace WordMix.DataAccess;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders;
using Domain.Entities;
using Domain.Repositories;

public class WordsRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IWordsRepository
{
    public async Task<Word[]> GetRandomWordsAsync(int limit, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().WhereImgIsNull().OrderByRandom().LimitOffset(limit).Build();
        
        return (await DbSession.QueryAsync<Word>(queryObject, cancellationToken: cancellationToken)).ToArray();
    }
    
    public async Task<Word?> GetWordByIdAsync(long id, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ById(id).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<Word>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<Word> GetWordWithUrlRandomAsync(CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().WhereImgIsNotNull().OrderByRandom().LimitOffset(1).Build();
        
        return await DbSession.QuerySingleAsync<Word>(queryObject, cancellationToken: cancellationToken);
    }

    private class SelectQuery : SelectQueryBuilderBase<SelectQuery>
    {
        protected override void PrepareSelect()
        {
            SelectCollector.To<Word>().GetAllPublicValues();
        }

        protected override void PrepareFrom()
        {
            FromCollector.From<Word>(Tables.Words);
        }

        public SelectQuery ById(long id)
        {
            Conditionals.For<Word>().AddEquals(x => x.Id, id);
            return this;
        }
        
        public SelectQuery OrderByRandom()
        {
            OrderBy.AddExpression("RANDOM()");
            return this;
        }
        
        public SelectQuery WhereImgIsNotNull()
        {
            Conditionals.For<Word>().AddNotNull(x => x.Img);
            return this;
        }
        
        public SelectQuery WhereImgIsNull()
        {
            Conditionals.For<Word>().AddNull(x => x.Img);
            return this;
        }
    }
}