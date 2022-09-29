using Examples.Mediatr;
using MediatR;
using MonoGame.Extensions.Hosting;
using System.Reflection;

var builder = GameApplication.CreateBuilder(args).UseGame<Game1>();

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

using var game = builder.Build();

await game.RunAsync();
