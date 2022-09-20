using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Hosting;


/// <summary>
/// 
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet/maui/blob/main/src/Core/src/Hosting/MauiApp.cs
/// </remarks>
public sealed class GameApplication : IDisposable
{
    private readonly IHost _host;
    private readonly IServiceProvider _services;

    internal GameApplication(IHost host, IServiceProvider services)
    {
        _host = host;
        _services = services;
    }

    /// <summary>
    /// The application's configured services.
    /// </summary>
    public IServiceProvider Services => _services;

    /// <summary>
    /// The application's configured <see cref="IConfiguration"/>.
    /// </summary>
    public IConfiguration Configuration => _services.GetRequiredService<IConfiguration>();

    public static GameApplicationBuilder CreateBuilder(params string[] args) => new(new() { Args = args });

    public static GameApplicationBuilder CreateBuilder(GameApplicationOptions options) => new(options);

    public void Run()
    {
        _host.RunAsync().GetAwaiter().GetResult();
    }

    public async Task RunAsync()
    {
        await _host.RunAsync();
    }

    public void Dispose()
    {
        // Explicitly dispose the Configuration, since it is added as a singleton object that the ServiceProvider
        // won't dispose.
        (Configuration as IDisposable)?.Dispose();

        (_services as IDisposable)?.Dispose();

        _host?.Dispose();
    }
}

public static class GameApplicationExtensions
{
}