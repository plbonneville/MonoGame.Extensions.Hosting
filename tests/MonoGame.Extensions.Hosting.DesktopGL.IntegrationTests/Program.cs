using MonoGame.Extensions.Hosting;
using MonoGame.Extensions.Hosting.DesktopGL.IntegrationTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

//// Option #1 ===============================================================
//var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();
//builder.Services.AddSingleton<IDemo, Demo>();
//using var game = builder.Build();
//await game.RunAsync();



//// Option #2 ===============================================================
//////ServiceProvider serviceProvider = default;

//////var configuration = new ConfigurationBuilder()
//////    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//////    .AddJsonFile("appsettings.Development.json", optional: true)
//////    .Build();

//var options = new GameApplicationOptions()
//{
//    Args = args,
//    //OnStarted = () =>
//    //{
//    //    var graphicsConfiguration = configuration.GetSection(GraphicsDeviceManagerOptions.GraphicsDeviceManager);

//    //    if (graphicsConfiguration.Exists())
//    //    {
//    //        var graphics = serviceProvider.GetRequiredService<GraphicsDeviceManager>();

//    //        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
//    //        var graphicsOptions = new GraphicsDeviceManagerOptions();
//    //        graphicsConfiguration.Bind(graphicsOptions);//.ValidateDataAnnotations();

//    //        graphics.PreferredBackBufferWidth = graphicsOptions.PreferredBackBufferWidth;
//    //        graphics.PreferredBackBufferHeight = graphicsOptions.PreferredBackBufferHeight;

//    //        // https://learn-monogame.github.io/tutorial/game-settings/
//    //        graphics.HardwareModeSwitch = graphicsOptions.HardwareModeSwitch;
//    //        graphics.IsFullScreen = graphicsOptions.IsFullScreen;

//    //        graphics.ApplyChanges();
//    //    }
//    //},
//    OnStopping = () => System.Diagnostics.Debug.WriteLine("Stoping"),
//    OnStopped = () => System.Diagnostics.Debug.WriteLine("Stopped"),
//};
//var builder = GameApplication.CreateBuilder(options).UseGame<Game1>();

//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile("appsettings.Development.json", optional: true);

//builder.Services.AddSingleton<IDemo, Demo>();

//using var game = builder.Build();

//////serviceProvider = builder.Services.BuildServiceProvider();

//await game.RunAsync();



// Option #3 ===============================================================
var options = new GameApplicationOptions
{
    Args = args,

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

            // https://learn-monogame.github.io/tutorial/game-settings/
            graphics.HardwareModeSwitch = graphicsOptions.HardwareModeSwitch;
            graphics.IsFullScreen = graphicsOptions.IsFullScreen;

            graphics.ApplyChanges();
        }
    }
};

var builder = GameApplication.CreateBuilder(options).UseGame<Game1>();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true);

builder.Services.AddSingleton<IDemo, Demo>();

using var game = builder.Build();

await game.RunAsync();