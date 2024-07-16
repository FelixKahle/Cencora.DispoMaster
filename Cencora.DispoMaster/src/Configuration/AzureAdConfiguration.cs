// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.DispoMaster.Configuration;

/// <summary>
/// Represents the Azure AD configuration.
/// </summary>
public class AzureAdConfiguration
{
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public required string Instance { get; set; }
    
    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    public required string Domain { get; set; }
    
    /// <summary>
    /// Gets or sets the tenant id.
    /// </summary>
    public required string TenantId { get; set; }
    
    /// <summary>
    /// Gets or sets the client id.
    /// </summary>
    public required string ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the client secret.
    /// </summary>
    public required string CallbackPath { get; set; }
    
    /// <summary>
    /// Gets or sets the scopes.
    /// </summary>
    public required string Scopes { get; set; }
}