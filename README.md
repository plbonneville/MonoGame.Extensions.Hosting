# MonoGame.Extensions.Hosting

Hosting and startup infrastructures for MonoGame applications.

Adds support for dependency injection, configuration, logging, and more to MonoGame applications.

## Installing MonoGame.Extensions.Hosting

You should install [MonoGame.Extensions.Hosting](https://www.nuget.org/packages/MonoGame.Extensions.Hosting) with NuGet:

```powershell
Install-Package MonoGame.Extensions.Hosting
```

Or via the .NET command-line interface (CLI):

```powershell
dotnet add package MonoGame.Extensions.Hosting
```

Either commands, from Package Manager Console or .NET command-line interface (CLI), will download and install MonoGame.Extensions.Hosting and all required dependencies.

## Usage

Replace the content of your `Program.cs` file with the following code:

```csharp
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();
using var game = builder.Build();
await game.RunAsync();
```

### Injecting Services

Configures the services by registering them on the builder.

Update `Program.cs` with the following code:

```csharp
using Microsoft.Extensions.DependencyInjection;
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();

// Register services
builder.Services.AddSingleton<IDemo, Demo>();
// builder.Services.AddScoped<IDemo, Demo>();
// builder.Services.AddTransient<IDemo, Demo>();

using var game = builder.Build();
await game.RunAsync();
```

Inside your `Game1.cs` class, you can inject the service as a constructor parameter:

```csharp
public class Game1 : Game
{
    private readonly IDemo _demo;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1(IDemo demo)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _demo = demo;
    }
}
```

### Executing code on host application lifetime events

To execute code when the host application starts, stops, or is performing a graceful shutdown:

1. Initializes a new instance of `GameApplicationOptions`.
2. Register the appropriate callbacks on the `GameApplicationOptions` properties.
3. And pass the `GameApplicationOptions` it to the `GameApplication.CreateBuilder(options)`.

```csharp
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

builder.Services.AddSingleton<IDemo, Demo>();

using var game = builder.Build();

await game.RunAsync();
```

### Configuration in MonoGame

The `GameApplicationBuilder` class provides access to the configuration of the application through the `Configuration` property.

No default configuration sources are registered.

You can register configuration sources by calling `AddJsonFile`, `AddXmlFile`, `AddIniFile`, `AddCommandLine`, and more on the `ConfigurationManager`.

```csharp
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();

builder.Configuration
    .AddIniFile("appsettings.ini", optional: true, reloadOnChange: true);

using var game = builder.Build();

await game.RunAsync();
```

## Further Reading

- [.NET Generic Host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host)
- [Dependency injection in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Configuration providers in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration-providers)
