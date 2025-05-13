using Byndyusoft.Logging.Builders;
using Byndyusoft.Logging.Configuration;
using Byndyusoft.MaskedSerialization.Serilog.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;
using Serilog.Configuration;
using WordMix.Api.Infrastructure.OpenTelemetry;
using WordMix.Api.Infrastructure.Serialization;
using WordMix.Api.Infrastructure.Swagger;
using WordMix.Api.Infrastructure.Versioning;
using WordMix.Api.Installers;

var serviceName = typeof(WordMix.Api.Program).Assembly.GetName().Name;
var serviceVersion = typeof(WordMix.Api.Program).Assembly.GetName().Version;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
                            configuration
                                .UseDefaultSettings(context.Configuration)
                                .UseOpenTelemetryTraces()
                                .WriteToOpenTelemetry(activityEventBuilder: StructuredActivityEventBuilder.Instance)
                                .WithMaskingPolicy()
                                .Enrich.WithPropertyDataAccessor()
                                .Enrich.WithStaticTelemetryItems()
                       );

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOpenTelemetry(
                          serviceName,
                          builder.Configuration.GetSection("OtlpExporterOptions").Bind,
                          builder => builder.AddNpgsql(),
                          builder => builder.AddTemplateMetrics()
                         );
services
    .AddMvcCore()
    .AddTracing();
services
    .AddRouting(options => options.LowercaseUrls = true)
    .AddJsonSerializerOptions();
services.AddHealthChecks();
services
    .ConfigureStaticTelemetryItemCollector()
    .WithBuildConfiguration()
    .WithAspNetCoreEnvironment()
    .WithServiceName(serviceName)
    .WithApplicationVersion(serviceVersion.ToString());
services
    .AddVersioning()
    .AddSwagger();
services
    .AddRelationalDb(NpgsqlFactory.Instance, builder.Configuration.GetConnectionString("Main"))
    .AddApplicationServices();

var app = builder.Build();
app
    .UseHealthChecks("/healthz")
    .UseOpenTelemetryPrometheusScrapingEndpoint()
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

// For tests accessibility
namespace WordMix.Api
{
    public class Program
    {
    }
}