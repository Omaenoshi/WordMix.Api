namespace WordMix.Api.Infrastructure.Swagger;

using System;
using System.IO;
using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var apiVersionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerDoc(apiVersionDescription.GroupName,
                               new OpenApiInfo
                                   {
                                       Version = apiVersionDescription.ApiVersion.ToString(),
                                       Description = apiVersionDescription.IsDeprecated ? "DEPRECATED" : "",
                                       Title = Assembly.GetExecutingAssembly().GetName().Name
                                   });


        var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");

        foreach (var xmlFile in xmlFiles)
            options.IncludeXmlComments(xmlFile);
        
        AddAuthorization(options);
    }
    
    private static void AddAuthorization(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer",
                                      new OpenApiSecurityScheme
                                          {
                                              In = ParameterLocation.Header,
                                              Description = "Enter a valid token",
                                              Name = "Authorization",
                                              Type = SecuritySchemeType.Http,
                                              BearerFormat = "JWT",
                                              Scheme = "Bearer"
                                          });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                                           {
                                               {
                                                   new OpenApiSecurityScheme
                                                       {
                                                           Reference = new OpenApiReference
                                                                           {
                                                                               Type = ReferenceType.SecurityScheme,
                                                                               Id = "Bearer"
                                                                           }
                                                       },
                                                   Array.Empty<string>()
                                               }
                                           });
    }
}