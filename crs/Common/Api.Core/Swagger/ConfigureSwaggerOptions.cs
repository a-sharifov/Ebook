﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Core.Swagger;

/// <summary>
/// Configure Swagger for each API version discovered.
/// </summary>
/// <param name="provider">Provider of API versions.</param>
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;
    private readonly string? _title = configuration["Swagger:Title"];

    /// <summary>
    /// Configure each API discovered for Swagger Documentation.
    /// </summary>
    /// <param name="options"> Swagger options.</param>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
    }

    /// <summary>
    /// Configure Swagger Options. Inherited from the Interface.
    /// </summary>
    /// <param name="name"> Name of the options.</param>
    /// <param name="options"> Swagger options.</param>
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    /// <summary>
    /// Create information about the version of the API.
    /// </summary>
    /// <param name="desc"> Description of the API.</param>
    /// <returns>Information about the API.</returns>
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
    {
        var info = new OpenApiInfo()
        {
            Title = _title,
            Version = desc.ApiVersion.ToString()
        };

        if (desc.IsDeprecated)
        {
            info.Description +=
                "This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        return info;
    }
}
