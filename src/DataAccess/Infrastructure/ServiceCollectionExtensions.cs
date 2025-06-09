namespace WordMix.DataAccess.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services.Scan(x => x.FromCallingAssembly()
                                   .AddClasses(c => c.Where(i => i.Name.EndsWith("Repository")))
                                   .AsImplementedInterfaces());
    }
}