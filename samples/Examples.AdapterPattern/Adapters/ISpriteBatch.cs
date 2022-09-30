using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

/// <remarks>
/// The target interface used to hide the details of the the <see cref="SpriteBatch"/> type using the adapter design pattern.
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
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
/// Simple wraper for the <see cref="SpriteBatch"/> type.
/// </summary>
/// <remarks>
/// Since we don't own the underlying <see cref="SpriteBatch"/> type, we need to wrap the type using the adapter design pattern.
/// All invocations (properties and methods) are delegated to the underlying type (the adaptee).
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
internal sealed class SpriteBatchWrapper : ISpriteBatch
{
    public SpriteBatchWrapper(SpriteBatch spriteBatch) => SpriteBatchAdaptee = spriteBatch;

    /// <summary>
    /// Get the underlying <see cref="SpriteBatch"/> wraped component.
    /// </summary>
    public SpriteBatch SpriteBatchAdaptee { get; }

    public void Draw(ITexture2D texture, Vector2 position, Color color) =>
        SpriteBatchAdaptee.Draw(texture.GetUnderlyingTexture(), position, color);

    public void Draw(ITexture2D texture, Rectangle rectangle, Color color) =>
        SpriteBatchAdaptee.Draw(texture.GetUnderlyingTexture(), rectangle, color);

    public void Draw(ITexture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color) =>
        SpriteBatchAdaptee.Draw(texture.GetUnderlyingTexture(), destinationRectangle, sourceRectangle, color);

    public void Begin() => SpriteBatchAdaptee.Begin();

    public void End() => SpriteBatchAdaptee.End();

    public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color)  =>
        SpriteBatchAdaptee.DrawString(spriteFont.GetUnderlyingSpriteFont(), text, position, color);

    public void DrawString(ISpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        SpriteBatchAdaptee.DrawString(spriteFont.GetUnderlyingSpriteFont(), text, position, color, rotation, origin, scale, effects, layerDepth);
}
