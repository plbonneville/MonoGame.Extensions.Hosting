namespace MonoGame.Extensions.Hosting;

public class GameApplicationOptions
{
    public string[]? Args { get; init; }

    public Action<GameApplication>? OnStarted { get; init; }
    public Action<GameApplication>? OnStopping { get; init; }
    public Action<GameApplication>? OnStopped { get; init; }
}
