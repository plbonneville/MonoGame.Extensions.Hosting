using Examples.AdapterPattern.Adapters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Examples.AdapterPattern;
public class Game1 : Game
{
    private readonly IGameStateFactory _gameStateFactory;
    private readonly ISpriteBatchFactory _spriteBatchFactory;

    private ISpriteBatch _spriteBatch;
    private IGameState _gameState;

    public Game1(IGameStateFactory gameStateFactory, ISpriteBatchFactory spriteBatchFactory)
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _gameStateFactory = gameStateFactory;
        _spriteBatchFactory = spriteBatchFactory;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = _spriteBatchFactory.CreateSpriteBatch();

        _gameState = _gameStateFactory.CreateGameState<GameplayState>();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _gameState.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _gameState.Render(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
