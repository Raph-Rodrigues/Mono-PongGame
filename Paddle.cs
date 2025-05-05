using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class Paddle : Sprite
{
    public float Speed;

    public Paddle(Texture2D texture, Vector2 position) 
        : base(texture, position, 1.0f)
    {
    }

    public override void Update(GameTime gameTime, Ball ball, GameWindow window)
    {
        base.Update(gameTime, ball, window);
        
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (Input.IsKeyDown(Keys.W))
        {
            Position.Y -= Speed * deltaTime;
        }
        if (Input.IsKeyDown(Keys.S))
        {
            Position.Y += Speed * deltaTime;
        }
    }
}