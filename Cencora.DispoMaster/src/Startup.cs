// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;
using Cencora.DispoMaster.Configuration;
using Cencora.DispoMaster.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Cencora.DispoMaster;

/// <summary>
/// Represents the startup class for the application.
/// </summary>
public class Startup
{
    private const string SwaggerSettingsSection = "Swagger";
    private const string AzureAdSettingsSection = "AzureAd";
    private const string AzureAdSwaggerSettingsSection = "AzureAdSwagger";
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <exception cref="ArgumentNullException">KeyVault:KeyVaultUri</exception>
    /// <exception cref="ArgumentNullException">AzureMaps:MapsClientId</exception>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <exception cref="ArgumentNullException">SwaggerSettingsSection is null.</exception>
    /// <exception cref="ArgumentNullException">AzureAdSwaggerSettingsSection is null.</exception>
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AzureAdConfiguration>(Configuration.GetSection(AzureAdSettingsSection));
        services.Configure<SwaggerConfiguration>(Configuration.GetSection(SwaggerSettingsSection));
        services.Configure<AzureAdSwaggerConfiguration>(Configuration.GetSection(AzureAdSwaggerSettingsSection));

        var swaggerConfiguration = Configuration.GetSection(SwaggerSettingsSection).Get<SwaggerConfiguration>() ?? throw new InvalidOperationException();
        var azureAdSwaggerConfiguration = Configuration.GetSection(AzureAdSwaggerSettingsSection).Get<AzureAdSwaggerConfiguration>() ?? throw new InvalidOperationException();
        
        // Add the controllers.
        services.AddControllers()
            // Configure the JSON options.
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
            });
        // Configures ApiExplorer using metadata.
        services.AddEndpointsApiExplorer();
        
        // Configure Swagger.
        services.ConfigureSwagger(swaggerConfiguration, azureAdSwaggerConfiguration);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection(AzureAdSettingsSection));
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The web host environment.</param>
    /// <remarks>
    /// This method is called by the runtime to configure the HTTP request pipeline.
    /// It adds middleware components to the pipeline to handle various aspects of the request processing.
    /// </remarks>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Only use Swagger and the developer exception page in development.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(Configuration["AzureAdSwagger:ClientId"]);
                options.OAuthUsePkce();
                options.OAuthScopeSeparator(" ");
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}