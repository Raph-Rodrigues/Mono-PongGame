using Microsoft.Xna.Framework;
using System;

namespace Mono_PongGame;

public class Ball
{
    private readonly Game1 _game;
    public Vector2 Position;
    public int Radius;
    public float SpeedX, SpeedY;
    private const float SpeedIncrease = 50f; // Speed increase per paddle hit
    private const float MaxSpeed = 1000f; 

    public Ball(Game1 game)
    {
        _game = game;
    }

    public void Update(GameTime gameTime, GameWindow window)
    {
        Position += new Vector2(SpeedX, SpeedY) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Collision with the boards
        if (Position.Y + Radius >= window.ClientBounds.Height || Position.Y - Radius <= 0)
        {
            SpeedY *= -1;
            _game.AudioManager.PlayHitSound();
        }

        // Scoring
        if (Position.X + Radius >= window.ClientBounds.Width)
        {
            Constants.CpuScore++;
            _game.AudioManager.PlayScoreSound();
            Reset(window);
        }

        if (Position.X - Radius <= 0)
        {
            Constants.PlayerScore++;
            _game.AudioManager.PlayScoreSound();
            Reset(window);
        }
    }

    public void Reset(GameWindow window)
    {
        Position = new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2);
        var rand = new Random();
        SpeedX = Math.Abs(SpeedX) * (rand.Next(0, 2) == 0 ? -1 : 1);
        SpeedY = Math.Abs(SpeedY) * (rand.Next(0, 2) == 0 ? -1 : 1);
    }

    public void IncreaseSpeed()
    {
        // Increase the speed while maintaining direction
        float currentSpeed = (float)Math.Sqrt(Math.Pow(SpeedX, 2) + Math.Pow(SpeedY, 2));
        float newSpeed = Math.Min(currentSpeed + SpeedIncrease, MaxSpeed);
        
        // Calculate new speed components while maintaining direction
        float speedRatio = newSpeed / currentSpeed;
        SpeedX *= speedRatio;
        SpeedY *= speedRatio;
    }

}