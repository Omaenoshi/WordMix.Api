﻿namespace WordMix.Api.Infrastructure.OpenTelemetry;

using System;
using Byndyusoft.Execution.Metrics;
using global::OpenTelemetry.Exporter;
using global::OpenTelemetry.Metrics;
using global::OpenTelemetry.Resources;
using global::OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        string? serviceName,
        Action<OtlpExporterOptions> configureOtlp,
        Action<TracerProviderBuilder>? configureBuilder = null,
        Action<MeterProviderBuilder>? configureMeter = null)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(builder =>
                             {
                                 builder
                                     .AddAspNetCoreInstrumentation(o => o.AddDefaultIgnorePatterns())
                                     .AddHttpClientInstrumentation()
                                     .AddOtlpExporter(configureOtlp);
                                 configureBuilder?.Invoke(builder);
                             }
                        )
            .WithMetrics(builder =>
                             {
                                 builder
                                     .AddPrometheusExporter()
                                     .AddRuntimeInstrumentation()
                                     .AddHttpRequestExecutionDurationInstrumentation(o => o.AddDefaultIgnorePatterns())
                                     .AddHttpClientInstrumentation();
                                 configureMeter?.Invoke(builder);
                             }
                        );
        return services;
    }
}