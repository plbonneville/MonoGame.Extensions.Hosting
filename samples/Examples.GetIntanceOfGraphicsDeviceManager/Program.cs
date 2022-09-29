using Examples.GetIntanceOfGraphicsDeviceManager;
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();
using var game = builder.Build();
await game.RunAsync();
