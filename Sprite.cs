using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class Sprite
{
    protected Texture2D Texture;
    public Vector2 Position;
    protected float Scale;

    public Rectangle Rect => new Rectangle(
        (int)Position.X,
        (int)Position.Y,
        (int)(Texture.Width * Scale),
        (int)(Texture.Height * Scale)
    );

    public Sprite(Texture2D texture, Vector2 position, float scale = 1.0f)
    {
        Texture = texture;
        Position = position;
        Scale = scale;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Rect, Color.White);
    }

    public virtual void Update(GameTime gameTime, Ball ball, GameWindow window)
    {
        // Mantém a posição dentro dos limites da tela
        Position.Y = MathHelper.Clamp(Position.Y, 0, window.ClientBounds.Height - Rect.Height);
    }
}