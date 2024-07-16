// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.DispoMaster;

/// <summary>
/// The entry point of the application.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Provides a mechanism for configuring and building an instance of <see cref="IHost"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="IHostBuilder"/> is used to configure and build an instance of <see cref="IHost"/>.
    /// It provides a fluent API for configuring various aspects of the host, such as logging, services, and application configuration.
    /// </remarks>
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        return hostBuilder;
    }
}