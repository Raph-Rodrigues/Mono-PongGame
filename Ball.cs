using Microsoft.Xna.Framework;
using System;

namespace Mono_PongGame;

public class Ball
{
    public Vector2 Position;
    public int Radius;
    public float SpeedX, SpeedY;

    public void Update(GameTime gameTime, GameWindow window)
    {
        Position += new Vector2(SpeedX, SpeedY) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Colisão com as bordas
        if (Position.Y + Radius >= window.ClientBounds.Height || Position.Y - Radius <= 0)
        {
            SpeedY *= -1;
        }

        // Pontuação
        if (Position.X + Radius >= window.ClientBounds.Width)
        {
            Constants.CpuScore++;
            Reset(window);
        }

        if (Position.X - Radius <= 0)
        {
            Constants.PlayerScore++;
            Reset(window);
        }
    }

    public void Reset(GameWindow window)
    {
        Position = new Vector2(window.ClientBounds.Width/2, window.ClientBounds.Height/2);
        var rand = new Random();
        SpeedX = Math.Abs(SpeedX) * (rand.Next(0, 2) == 0 ? -1 : 1);
        SpeedY = Math.Abs(SpeedY) * (rand.Next(0, 2) == 0 ? -1 : 1);
    }
}