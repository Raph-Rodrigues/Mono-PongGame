using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class CpuPaddle : Sprite
{
    public float Speed;
    private const float ReactionThreshold = 0.3f; // Delay Rection

    public CpuPaddle(Texture2D texture, Vector2 position) 
        : base(texture, position, 1.0f)
    {
    }

    public override void Update(GameTime gameTime, Ball ball, GameWindow window)
    {
        base.Update(gameTime, ball, window);
        
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var targetY = ball.Position.Y - (Rect.Height / 2);

        // Smoothed movement with velocity limit
        if (Position.Y + ReactionThreshold < targetY)
        {
            Position.Y += Speed * deltaTime;
        }
        else if (Position.Y - ReactionThreshold > targetY)
        {
            Position.Y -= Speed * deltaTime;
        }
    }
}