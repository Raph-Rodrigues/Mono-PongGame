using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class CpuPaddle : Sprite
{
    public float Speed;
    private const float ReactionThreshold = 0.3f; // Atraso na reação

    public CpuPaddle(Texture2D texture, Vector2 position) 
        : base(texture, position, 1.0f)
    {
    }

    public override void Update(GameTime gameTime, Ball ball, GameWindow window)
    {
        base.Update(gameTime, ball, window);
        
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var targetY = ball.Position.Y - (Rect.Height / 2);
        
        // Movimento suavizado com limite de velocidade
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