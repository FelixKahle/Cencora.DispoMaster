// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.DispoMaster.Configuration;
using Microsoft.OpenApi.Models;

namespace Cencora.DispoMaster.Extensions;

/// <summary>
/// Provides extensions for the <see cref="IServiceCollection"/> class.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string SwaggerSecuritySchemeName = "oauth2";
    
    /// <summary>
    /// Configures Swagger for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <param name="swaggerSettings">The Swagger configuration.</param>
    /// <param name="azureAdSwaggerSettings">The Azure AD Swagger configuration.</param>
    public static void ConfigureSwagger(this IServiceCollection services, SwaggerConfiguration swaggerSettings, AzureAdSwaggerConfiguration azureAdSwaggerSettings)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerSettings.Name, new OpenApiInfo { Title = swaggerSettings.Title, Version = swaggerSettings.Version });
            options.AddSecurityDefinition(SwaggerSecuritySchemeName, new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Authorization Code Flow",
                Name = SwaggerSecuritySchemeName,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(azureAdSwaggerSettings.AuthorizationUrl),
                        TokenUrl = new Uri(azureAdSwaggerSettings.TokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { azureAdSwaggerSettings.Scope, "Access the API" }
                        }
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = SwaggerSecuritySchemeName }
                    },
                    new[] { azureAdSwaggerSettings.Scope }
                }
            });
        });
    }
}