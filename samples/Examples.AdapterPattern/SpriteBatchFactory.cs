using Examples.AdapterPattern.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Examples.AdapterPattern;

/// <summary>
/// The abstract factory used to create <see cref="ISpriteBatch"/>.
/// </summary>
public interface ISpriteBatchFactory
{
    ISpriteBatch CreateSpriteBatch();
}

/// <summary>
/// The concrete implementation of the abstract factory for type <see cref="ISpriteBatch"/>.
/// </summary>
public sealed class SpriteBatchFactory : ISpriteBatchFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SpriteBatchFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISpriteBatch CreateSpriteBatch()
    {
        return _serviceProvider.GetRequiredService<ISpriteBatch>();
    }
}
