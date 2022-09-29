using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

public interface ISpriteBatch
{
    void Draw(ITexture2D texture, Vector2 position, Color color);
    void Draw(ITexture2D texture, Rectangle rectangle, Color color);
    void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color);

    void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color);

    void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

    void Begin();
    void End();
}

/// <summary>
/// Since we don't 'own' the underlying <see cref="SpriteBatch"/> type, we need to wrap the type using the adapter design pattern.
/// </summary>
/// <remarks>
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
internal class SpriteBatchWrapper : ISpriteBatch
{
    private readonly SpriteBatch _spriteBatch;

    public SpriteBatchWrapper(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }

    public void Draw(ITexture2D texture, Vector2 position, Color color)
    {
        var underlyingTexture = ((Texture2DAdapter)texture).Adaptee;

        _spriteBatch.Draw(underlyingTexture, position, color);
    }

    public void Draw(ITexture2D texture, Rectangle rectangle, Color color)
    {
        var underlyingTexture = ((Texture2DAdapter)texture).Adaptee;

        _spriteBatch.Draw(underlyingTexture, rectangle, color);
    }

    public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color)
    {
        var underlyingTexture = ((Texture2DAdapter)texture).Adaptee;

        _spriteBatch.Draw(underlyingTexture, destinationRectangle, sourceRectangle, color);
    }

    public void Begin()
    {
        _spriteBatch.Begin();
    }

    public void End()
    {
        _spriteBatch.End();
    }

    public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)
    {
        var underlyingFont = ((SpriteFontAdapter)spriteFont).Adaptee;

        _spriteBatch.DrawString(underlyingFont, text, position, color);
    }

    public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        var underlyingFont = ((SpriteFontAdapter)spriteFont).Adaptee;

        _spriteBatch.DrawString(underlyingFont, text, position, color, rotation, origin, scale, effects, layerDepth);
    }
}
