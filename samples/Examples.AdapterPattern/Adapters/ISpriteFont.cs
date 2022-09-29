using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

public interface ISpriteFont
{
    Vector2 MeasureString(string text);
}

internal class SpriteFontAdapter : ISpriteFont
{
    private readonly SpriteFont _texture;

    public SpriteFontAdapter(SpriteFont texture)
    {
        _texture = texture;
    }

    /// <summary>
    /// Get the underlying <see cref="SpriteFont"/> wraped component.
    /// </summary>
    public SpriteFont Adaptee => _texture;

    public Vector2 MeasureString(string text)
    {
        return _texture.MeasureString(text);
    }
}