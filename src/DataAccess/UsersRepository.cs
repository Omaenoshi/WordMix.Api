namespace WordMix.DataAccess;

using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders;
using Byndyusoft.Data.Relational.QueryBuilder.QueryObjectBuilders.Update;
using Domain.Entities;
using Domain.Repositories;

public class UsersRepository(IDbSessionAccessor dbSessionAccessor) : DbSessionConsumer(dbSessionAccessor), IUsersRepository
{
    public async Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        var queryObject = InsertQueryBuilder.For(user, Tables.Users)
                                            .InsertAllPublicValues()
                                            .Build();
        
        user.Id = await DbSession.ExecuteScalarAsync<long>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ByEmail(email).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<User>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByVerificationTokenAsync(string token, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ByVerificationToken(token).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<User>(queryObject, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var queryObject = UpdateQueryBuilder<User>.For(Tables.Users)
                                                    .Set(x => x.IsVerified, user.IsVerified)
                                                    .Build();
        
        await DbSession.ExecuteAsync(queryObject, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var queryObject = new SelectQuery().ById(id).Build();
        
        return await DbSession.QuerySingleOrDefaultAsync<User>(queryObject, cancellationToken: cancellationToken);
    }
    
    private class SelectQuery : SelectQueryBuilderBase<SelectQuery>
    {
        protected override void PrepareSelect()
        {
            SelectCollector.To<User>().GetAllPublicValues();
        }

        protected override void PrepareFrom()
        {
            FromCollector.From<User>(Tables.Users);
        }

        public SelectQuery ById(long id)
        {
            Conditionals.For<User>().AddEquals(x => x.Id, id);
            return this;
        }
        
        public SelectQuery ByEmail(string email)
        {
            Conditionals.For<User>().AddEquals(x => x.Email, email);
            return this;
        }
        
        public SelectQuery ByVerificationToken(string token)
        {
            Conditionals.For<User>().AddEquals(x => x.VerificationToken, token);
            return this;
        }
    }
}