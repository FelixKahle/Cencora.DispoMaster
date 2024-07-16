// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.DispoMaster.Configuration;

/// <summary>
/// Represents the configuration for the Azure AD Swagger.
/// </summary>
public class AzureAdSwaggerConfiguration
{
    /// <summary>
    /// Gets or sets the authorization URL.
    /// </summary>
    public required string AuthorizationUrl { get; init; }
    
    /// <summary>
    /// Gets or sets the token URL.
    /// </summary>
    public required string TokenUrl { get; init; }
    
    /// <summary>
    /// Gets or sets the scope.
    /// </summary>
    public required string Scope { get; init; }
    
    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public required string ClientId { get; init;}
}