using Examples.ApplicationLifetimeEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using MonoGame.Extensions.Hosting;

var options = new GameApplicationOptions
{
    OnStarted = gameApp =>
    {
        var graphicsConfiguration = gameApp.Configuration.GetSection(GraphicsDeviceManagerOptions.GraphicsDeviceManager);

        if (graphicsConfiguration.Exists())
        {
            var graphicsOptions = new GraphicsDeviceManagerOptions();
            graphicsConfiguration.Bind(graphicsOptions);

            var graphics = gameApp.Services.GetRequiredService<GraphicsDeviceManager>();

            graphics.PreferredBackBufferWidth = graphicsOptions.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = graphicsOptions.PreferredBackBufferHeight;
            graphics.HardwareModeSwitch = graphicsOptions.HardwareModeSwitch;
            graphics.IsFullScreen = graphicsOptions.IsFullScreen;

            graphics.ApplyChanges();
        }
    },

    OnStopping = gameApp => { /* ... */ },

    OnStopped = gameApp => { /* ... */ },
};

var builder = GameApplication.CreateBuilder(options).UseGame<Game1>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true);

builder.Services.AddSingleton<IMyDependency, MyDependency>();

using var game = builder.Build();

await game.RunAsync();
