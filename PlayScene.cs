using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono_PongGame;

public class PlayScene : IScene
{
    private readonly Game1 _game;
    private readonly SpriteFont _font;
    private readonly GameWindow _gameWindow;
    private Ball _ball;
    private List<Sprite> _sprites;
    private Texture2D _paddleTexture;
    private ParticleSystem _particleSystem;

    public PlayScene(Game1 game, SpriteFont font)
    {
        _game = game;
        _font = font;
        _gameWindow = _game.Window;
        
        // Create the textures for the paddles with right size
        _paddleTexture = new Texture2D(_game.GraphicsDevice, 20, 120);
        Color[] colorData = new Color[20 * 120];
        Array.Fill(colorData, Color.White);
        _paddleTexture.SetData(colorData);

        _particleSystem = new ParticleSystem(_game.GraphicsDevice);
        
        
        InitializeEntities();
    }

    private void InitializeEntities()
    {
        _sprites = new List<Sprite>();
        
        // Player
        var player = new Paddle(_paddleTexture, new Vector2(20, Constants.ScreenHeight/2 - 60))
        {
            Speed = 500
        };
        
        // CPU
        var cpu = new CpuPaddle(_paddleTexture, new Vector2(Constants.ScreenWidth - 40, Constants.ScreenHeight/2 - 60))
        {
            Speed = 450
        };

        _sprites.Add(player);
        _sprites.Add(cpu);

        // Ball
        _ball = new Ball(_game)
        {
            Radius = 12,
            SpeedX = 400,
            SpeedY = 400,
            Position = new Vector2(Constants.ScreenWidth/2, Constants.ScreenHeight/2)
        };

        ResetBall(true);
    }

    public void Update(GameTime gameTime)
    {
        if (Input.IsKeyJustPressed(Keys.Escape))
            _game.SceneManager.ChangeScene(GameStates.Menu);

        _ball.Update(gameTime, _gameWindow);

        foreach (var sprite in _sprites)
        {
            sprite.Update(gameTime, _ball, _gameWindow);
        }

        _particleSystem.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        CheckCollisions();
        CheckGameOver();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Field
        spriteBatch.DrawRectangle(new Rectangle(Constants.ScreenWidth/2, 0, Constants.ScreenWidth/2, Constants.ScreenHeight), Constants.Green);
        spriteBatch.DrawCircle(new Vector2(Constants.ScreenWidth/2, Constants.ScreenHeight/2), 150, Constants.LightGreen);
        spriteBatch.DrawLine(Constants.ScreenWidth/2, 0, Constants.ScreenWidth/2, Constants.ScreenHeight, Color.White);

        // Entities
        spriteBatch.DrawCircle(_ball.Position, _ball.Radius, Constants.Yellow);
        foreach (var sprite in _sprites)
        {
            sprite.Draw(spriteBatch);
        }

        // Draw Particles
        _particleSystem.Draw(spriteBatch);

        // Score
        spriteBatch.DrawString(_font, Constants.CpuScore.ToString(), new Vector2(Constants.ScreenWidth/4 - 20, 20), Color.White);
        spriteBatch.DrawString(_font, Constants.PlayerScore.ToString(), new Vector2(3 * Constants.ScreenWidth/4 - 20, 20), Color.White);
    }

    public void Reset()
    {
        Constants.ResetScores();
        InitializeEntities();
    }

    private void CheckCollisions()
    {
        foreach (var sprite in _sprites)
        {
            if (CheckCollision(_ball, sprite.Rect))
            {
                _ball.SpeedX *= -1;
                _ball.SpeedY += new Random().Next(-50, 50); // Add variation
                _ball.IncreaseSpeed(); // Increase ball speed after paddle hit
                _game.AudioManager.PlayHitSound();

                // Emit particles at collision point
                _particleSystem.Emit(_ball.Position, 20, Constants.Yellow);
                break;
            }
        }
    }

    private bool CheckCollision(Ball ball, Rectangle rect)
    {
        var closestX = MathHelper.Clamp(ball.Position.X, rect.Left, rect.Right);
        var closestY = MathHelper.Clamp(ball.Position.Y, rect.Top, rect.Bottom);
        var distance = Vector2.Distance(ball.Position, new Vector2(closestX, closestY));
        return distance < ball.Radius;
    }

    private void CheckGameOver()
    {
        if (Constants.PlayerScore >= Constants.MaxScore || Constants.CpuScore >= Constants.MaxScore)
        {
            _game.AudioManager.PlayScoreSound();
            _game.SceneManager.ChangeScene(GameStates.GameOver);
        }
    }

    private void ResetBall(bool initial = false)
    {
        if (!initial)
        {
            _ball.Position = new Vector2(Constants.ScreenWidth/2, Constants.ScreenHeight/2);
        }
        
        var rand = new Random();
        _ball.SpeedX = Math.Abs(_ball.SpeedX) * (rand.Next(0, 2) == 0 ? -1 : 1);
        _ball.SpeedY = Math.Abs(_ball.SpeedY) * (rand.Next(0, 2) == 0 ? -1 : 1);
    }
}