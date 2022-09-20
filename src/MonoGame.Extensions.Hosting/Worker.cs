using Microsoft.Extensions.Hosting;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace MonoGame.Extensions.Hosting;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Should I rename this to GameShell?
/// https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/shell/
/// </remarks>
public class Worker : IHostedService
{
    private readonly Game _game;
    private readonly GameApplication _gameApplication;
    private readonly GameApplicationOptions _options;
    private readonly IHostApplicationLifetime _appLifetime;

    public Worker(GameApplicationOptions options, GameApplication gameApplication, Game game, IHostApplicationLifetime appLifetime)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _gameApplication = gameApplication ?? throw new ArgumentNullException(nameof(gameApplication));
        _game = game ?? throw new ArgumentNullException(nameof(game));
        _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));

        var graphicsDeviceManagerProperty = game
            .GetType()
            .GetProperty("graphicsDeviceManager", BindingFlags.Instance | BindingFlags.NonPublic);

        var graphicsDeviceManagerValue = graphicsDeviceManagerProperty!.GetValue(game) as GraphicsDeviceManager;

        // If the GraphicsDeviceManager was not instantiated in the Game contructor, create it now.
        Graphics = graphicsDeviceManagerValue ?? new GraphicsDeviceManager(game);

        ContentManager = game.Content;
    }

    internal static GraphicsDeviceManager? Graphics { get; private set; }

    internal static ContentManager? ContentManager { get; private set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        _game.Exiting += OnGameExiting;

        return Task.CompletedTask;
    }

    private void OnGameExiting(object? sender, EventArgs e)
    {
        StopAsync(new CancellationToken());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _appLifetime.StopApplication();

        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _options.OnStarted?.Invoke(_gameApplication);

        _game.Run();
    }

    private void OnStopping()
    {
        _options.OnStopping?.Invoke(_gameApplication);
    }

    private void OnStopped()
    {
        _options.OnStopped?.Invoke(_gameApplication);
    }
}
