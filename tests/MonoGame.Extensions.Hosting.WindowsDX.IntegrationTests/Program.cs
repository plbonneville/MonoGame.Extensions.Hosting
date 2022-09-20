using Microsoft.Extensions.DependencyInjection;
using MonoGame.Extensions.Hosting;
using MonoGame.Extensions.Hosting.WindowsDX.IntegrationTests;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();
builder.Services.AddSingleton<IDemo, Demo>();
using var game = builder.Build();
await game.RunAsync();