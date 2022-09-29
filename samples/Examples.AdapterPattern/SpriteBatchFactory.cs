using Examples.AdapterPattern.Adapters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Examples.AdapterPattern;

public interface ISpriteBatchFactory
{
    ISpriteBatch CreateSpriteBatch();
}

public sealed class SpriteBatchFactory : ISpriteBatchFactory
{
    private IServiceProvider _serviceProvider;

    public SpriteBatchFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISpriteBatch CreateSpriteBatch()
    {
        return _serviceProvider.GetRequiredService<ISpriteBatch>();
    }
}
