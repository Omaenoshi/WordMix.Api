namespace WordMix.Domain.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddOptions<SmtpSettings>().Bind(configuration.GetSection("SmtpSettings"))
                       .ValidateDataAnnotations()
                       .ValidateOnStart()
                       .Services
                       .AddHttpContextAccessor()
                       .Scan(x => x.FromCallingAssembly()
                                   .AddClasses(c => c.Where(i => i.Name.EndsWith("UseCase")))
                                   .AsSelf())
                       .Scan(x => x.FromCallingAssembly()
                                   .AddClasses(c => c.Where(i => i.Name.EndsWith("Service")))
                                   .AsImplementedInterfaces());
    }
}