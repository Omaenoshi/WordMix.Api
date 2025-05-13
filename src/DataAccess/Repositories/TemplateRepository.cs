namespace WordMix.DataAccess.Repositories;

using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;

public class TemplateRepository : DbSessionConsumer
{
    public TemplateRepository(IDbSessionAccessor sessionAccessor) : base(sessionAccessor)
    {
    }

    public async Task GetOneAsync(CancellationToken cancellationToken)
    {
        var queryObject = new QueryObject("SELECT 1");
        var result = await DbSession.QueryAsync<int>(queryObject, cancellationToken: cancellationToken);

        Debug.Assert(result.Single() == 1);
    }
}