using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_PongGame;

public class MenuScene : IScene
{
    private readonly SpriteFont _font;
    private readonly Game1 _game;
    private int _selectedOption;

    public MenuScene(Game1 game, SpriteFont font)
    {
        _game = game;
        _font = font;
    }

    public void Update(GameTime gameTime)
    {
        if (Input.IsKeyJustPressed(Keys.Up))
        {
            _selectedOption = (_selectedOption - 1 + 2) % 2;
        }

        if (Input.IsKeyJustPressed(Keys.Down))
        {
            _selectedOption = (_selectedOption + 1) % 2;
        }

        if (Input.IsKeyJustPressed(Keys.Enter))
        {
            if (_selectedOption == 0)
                _game.SceneManager.ChangeScene(GameStates.Play);
            else
                _game.Exit();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var title = "PONG GAME";
        var options = new[] { "Play Game", "Exit" };

        var titlePos = new Vector2(Constants.ScreenWidth / 2 - _font.MeasureString(title).X / 2, 200);

        spriteBatch.DrawString(_font, title, titlePos, Color.White);

        for (int i = 0; i < options.Length; i++)
        {
            var color = i == _selectedOption ? Color.Yellow : Color.White;
            var position = new Vector2(Constants.ScreenWidth / 2 - _font.MeasureString(options[i]).X / 2, 300 + i * 100);

            spriteBatch.DrawString(_font, options[i], position, color);
        }
    }

    public void Reset() => _selectedOption = 0;
}
