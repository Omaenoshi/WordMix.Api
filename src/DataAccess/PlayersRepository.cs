namespace WordMix.DataAccess;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders.Update;
using Domain.Entities;
using Domain.Repositories;

public class PlayersRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IPlayersRepository
{
    public async Task InsertAsync(Player player, CancellationToken cancellationToken)
    {
        var queryObject = InsertQueryBuilder.For(player, Tables.Players)
                                            .InsertAllPublicValues()
                                            .Build();
        
        player.Id = await DbSession.ExecuteScalarAsync<long>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<Player?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ById(id).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<Player>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<Player?> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ByUserId(userId).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<Player>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<Player[]> GetOrderByScore(int limit, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().OrderByExperience().LimitOffset(limit).Build();
        
        return (await DbSession.QueryAsync<Player>(queryObject, cancellationToken: cancellationToken)).ToArray();
    }

    public async Task UpdateAsync(Player player, CancellationToken cancellationToken)
    {
        var queryObject = UpdateQueryBuilder<Player>.For(Tables.Players)
                                                    .Set(x => x.Experience, player.Experience)
                                                    .Where($"id={player.Id}")
                                                    .Build();
        
        await DbSession.ExecuteAsync(queryObject, cancellationToken: cancellationToken);
    }
    
    private class SelectQuery : SelectQueryBuilderBase<SelectQuery>
    {
        protected override void PrepareSelect()
        {
            SelectCollector.To<Player>().GetAllPublicValues();
        }

        protected override void PrepareFrom()
        {
            FromCollector.From<Player>(Tables.Players);
        }

        public SelectQuery ById(long id)
        {
            Conditionals.For<Player>().AddEquals(x => x.Id, id);
            return this;
        }
        
        public SelectQuery ByUserId(long userId)
        {
            Conditionals.For<Player>().AddEquals(x => x.UserId, userId);
            return this;
        }
        
        public SelectQuery OrderByExperience()
        {
            OrderBy.Add("experience", isDescending: true);
            return this;
        }
    }
}