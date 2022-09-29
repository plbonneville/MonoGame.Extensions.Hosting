using Examples.AdapterPattern.Adapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.AdapterPattern;

public interface IGameState
{
    void Update(GameTime gameTime);
    void Render(ISpriteBatch spriteBatch);
}

public class GameplayState : IGameState
{
    private readonly ITexture2D _texture;
    private Vector2 _position;

    public GameplayState(IContentManager contentManager)
    {
        _texture = contentManager.LoadTexture("redbox");
    }

    public void Update(GameTime gameTime)
    {
        const float speed = 0.5f;

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            _position.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            _position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
        }

        if (keyboardState.IsKeyDown(Keys.Up))
        {
            _position.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            _position.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
        }
    }

    public void Render(ISpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}