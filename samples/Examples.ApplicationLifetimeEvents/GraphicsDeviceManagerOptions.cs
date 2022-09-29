namespace Examples.ApplicationLifetimeEvents;

internal sealed class GraphicsDeviceManagerOptions
{
    public const string GraphicsDeviceManager = nameof(GraphicsDeviceManager);

    public int PreferredBackBufferWidth { get; set; } = 800;
    public int PreferredBackBufferHeight { get; set; } = 600;
    public bool HardwareModeSwitch { get; set; }
    public bool IsFullScreen { get; set; }
}