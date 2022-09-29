using Microsoft.Extensions.DependencyInjection;

namespace Examples.AdapterPattern;

/// <summary>
/// The abstract factory used to create game states.
/// </summary>
public interface IGameStateFactory
{
    IGameState CreateGameState<TGameState>() where TGameState : class, IGameState;
}


/// <summary>
/// The concrete implementation of the abstract factory for type <see cref="IGameState"/>.
/// </summary>
public sealed class GameStateFactory : IGameStateFactory, IDisposable
{
    private readonly IServiceScope _serviceScope;

    public GameStateFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScope = serviceScopeFactory.CreateScope();
    }

    public IGameState CreateGameState<TGameState>() where TGameState : class, IGameState
    {
        return _serviceScope.ServiceProvider.GetRequiredService<TGameState>();
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}
