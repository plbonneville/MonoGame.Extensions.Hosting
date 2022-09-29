using Examples.WindowsDX;
using Microsoft.Extensions.DependencyInjection;
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();
builder.Services.AddSingleton<IMyDependency, MyDependency>();
using var game = builder.Build();
await game.RunAsync();
