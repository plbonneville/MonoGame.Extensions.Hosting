using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;

/// <remarks>
/// The target interface used to hide the details of the the <see cref="SpriteFont"/> type using the adapter design pattern.
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
public interface ISpriteFont
{
    Vector2 MeasureString(string text);
}

/// <summary>
/// Simple wraper for the <see cref="SpriteFont"/> type.
/// </summary>
/// <remarks>
/// Since we don't own the underlying <see cref="SpriteFont"/> type, we need to wrap the type using the adapter design pattern.
/// All invocations (properties and methods) are delegated to the underlying type (the adaptee).
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
internal class SpriteFontAdapter : ISpriteFont
{
    public SpriteFontAdapter(SpriteFont texture) => SpriteFontAdaptee = texture;

    /// <summary>
    /// Get the underlying <see cref="SpriteFont"/> wraped component.
    /// </summary>
    public SpriteFont SpriteFontAdaptee { get; }

    public Vector2 MeasureString(string text) => SpriteFontAdaptee.MeasureString(text);
}

internal static class ISpriteFontExtensions
{
    public static SpriteFont GetUnderlyingSpriteFont(this ISpriteFont texture) => ((SpriteFontAdapter)texture).SpriteFontAdaptee;
}