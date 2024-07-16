// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.DispoMaster.Configuration;

/// <summary>
/// Configuration for Swagger
/// </summary>
public class SwaggerConfiguration
{
    /// <summary>
    /// Name of Swagger.
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Title of Swagger.
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Version.
    /// </summary>
    public required string Version { get; init; }
}