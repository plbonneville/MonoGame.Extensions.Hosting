using Examples.AdapterPattern;
using Examples.AdapterPattern.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extensions.Hosting;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();

builder.Services.AddSingleton<SpriteBatch>();
builder.Services.AddSingleton<ISpriteBatch, SpriteBatchWrapper>();
builder.Services.AddSingleton<ISpriteBatchFactory, SpriteBatchFactory>();

builder.Services.AddSingleton<IContentManager, ContentManagerWrapper>();

builder.Services.AddTransient<GameplayState>();
builder.Services.AddSingleton<IGameStateFactory, GameStateFactory>();

using var game = builder.Build();

await game.RunAsync();
