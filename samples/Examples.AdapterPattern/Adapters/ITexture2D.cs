using Microsoft.Xna.Framework.Graphics;

namespace Examples.AdapterPattern.Adapters;


/// <remarks>
/// The target interface used to hide the details of the the <see cref="Texture2D"/> type using the adapter design pattern.
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
public interface ITexture2D
{
    int Height { get; }
    int Width { get; }
}

/// <summary>
/// Simple wraper for the <see cref="Texture2D"/> type.
/// </summary>
/// <remarks>
/// Since we don't own the underlying <see cref="Texture2D"/> type, we need to wrap the type using the adapter design pattern.
/// All invocations (properties and methods) are delegated to the underlying type.
/// https://en.wikipedia.org/wiki/Adapter_pattern
/// </remarks>
// adaptee
internal class Texture2DAdapter : ITexture2D
{
    // target
    private readonly Texture2D _texture;

    public Texture2DAdapter(Texture2D texture)
    {
        _texture = texture;
    }

    public int Height => _texture.Height;
    public int Width => _texture.Width;

    /// <summary>
    /// Get the underlying <see cref="Texture2D"/> wraped component.
    /// </summary>
    public Texture2D Adaptee => _texture;
}