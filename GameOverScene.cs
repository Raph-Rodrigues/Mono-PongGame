using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_PongGame;

public class GameOverScene : IScene
{
    private readonly Game1 _game;
    private readonly SpriteFont _font;

    public GameOverScene(Game1 game, SpriteFont font)
    {
        _game = game;
        _font = font;
    }

    public void Update(GameTime gameTime)
    {
        if (Input.IsKeyJustPressed(Keys.Enter))
            _game.SceneManager.ChangeScene(GameStates.Play);

        if (Input.IsKeyJustPressed(Keys.Escape))
            _game.SceneManager.ChangeScene(GameStates.Menu);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var message = Constants.PlayerScore >= Constants.MaxScore
            ? "CPU Wins!"
            : "Player Wins!";

        var instruction = "Press Enter to Play Again\nEsc to Menu";

        var messagePos = new Vector2(
            Constants.ScreenWidth / 2 - _font.MeasureString(message).X / 2,
            200);

        var instructionPos = new Vector2(
            Constants.ScreenWidth / 2 - _font.MeasureString(instruction).X / 2,
            400);

        spriteBatch.DrawString(_font, message, messagePos, Color.White);
        spriteBatch.DrawString(_font, instruction, instructionPos, Color.White);
    }

    public void Reset() { }
}