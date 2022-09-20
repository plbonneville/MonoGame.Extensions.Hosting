using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

    internal GameApplication(IHost host, IServiceProvider services)
    {
        _host = host;
        Services = services;
    }

    /// <summary>
    /// The application's configured services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// The application's configured <see cref="IConfiguration"/>.
    /// </summary>
    public IConfiguration Configuration => Services.GetRequiredService<IConfiguration>();

    public static GameApplicationBuilder CreateBuilder(params string[] args) => new(new GameApplicationOptions { Args = args });

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

        (Services as IDisposable)?.Dispose();

        _host.Dispose();
    }
}
