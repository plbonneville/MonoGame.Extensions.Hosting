using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

/// <remarks>
/// The target interface used to hide the details of the the <see cref="ContentManager"/> type using the adapter design pattern.
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
public interface IContentManager
{
    ITexture2D LoadTexture(string textureName);
    ISpriteFont LoadFont(string fontName);
    //SoundEffect LoadSound(string soundName);

    T Load<T>(string assetName);
}

/// <summary>
/// Simple wraper for the <see cref="ContentManager"/> type.
/// </summary>
/// <remarks>
/// Since we don't own the underlying <see cref="ContentManager"/> type, we need to wrap the type using the adapter design pattern.
/// All invocations (properties and methods) are delegated to the underlying type (the adaptee).
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
internal sealed class ContentManagerWrapper : IContentManager
{
    public ContentManagerWrapper(ContentManager contentManager)
    {
        ContentManagerAdaptee = contentManager;
    }

    /// <summary>
    /// Get the underlying <see cref="ContentManager"/> wraped component.
    /// </summary>
    public ContentManager ContentManagerAdaptee { get; }

    public T Load<T>(string assetName)
    {
        if (typeof(T) == typeof(ITexture2D))
        {
            var texture = LoadTexture(assetName);
            return (T)texture;
        }
        else if (typeof(T) == typeof(ISpriteFont))
        {
            var font = LoadFont(assetName);
            return (T)font;
        }

        throw new NotSupportedException($"Can't load asset '{assetName}' for the requested type '{typeof(T).Name}'");
    }

    public ITexture2D LoadTexture(string assetName)
    {
        var texture = ContentManagerAdaptee.Load<Texture2D>(assetName);
        return new Texture2DAdapter(texture);
    }

    public ISpriteFont LoadFont(string assetName)
    {
        var texture = ContentManagerAdaptee.Load<SpriteFont>(assetName);
        return new SpriteFontAdapter(texture);
    }
}