using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

public interface IContentManager
{
    ITexture2D LoadTexture(string textureName);
    ISpriteFont LoadFont(string fontName);
    //SoundEffect LoadSound(string soundName);

    T Load<T>(string assetName);
}

internal class ContentManagerWrapper : IContentManager
{
    private readonly ContentManager _contentManager;

    public ContentManagerWrapper(ContentManager contentManager)
    {
        _contentManager = contentManager;
    }


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
        var texture = _contentManager.Load<Texture2D>(assetName);
        return new Texture2DAdapter(texture);
    }

    public ISpriteFont LoadFont(string assetName)
    {
        var texture = _contentManager.Load<SpriteFont>(assetName);
        return new SpriteFontAdapter(texture);
    }
}