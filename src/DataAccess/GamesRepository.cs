namespace WordMix.DataAccess;

using System;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders;
using Domain.Entities;
using Domain.Repositories;

public class GamesRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IGamesRepository
{
    public async Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
        var queryObject = InsertQueryBuilder.For(game, Tables.Games)
                                            .InsertAllPublicValues()
                                            .Build();
        
        game.Id = await DbSession.ExecuteScalarAsync<long>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<Game?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ById(id).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<Game>(queryObject, cancellationToken: cancellationToken);
    }
    
    private class SelectQuery : SelectQueryBuilderBase<SelectQuery>
    {
        protected override void PrepareSelect()
        {
            SelectCollector.To<Game>().GetAllPublicValues();
        }

        protected override void PrepareFrom()
        {
            FromCollector.From<Game>(Tables.Games);
        }

        public SelectQuery ById(long id)
        {
            Conditionals.For<Game>().AddEquals(x => x.Id, id);
            return this;
        }
    }
}