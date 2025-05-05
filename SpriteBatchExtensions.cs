using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public static class SpriteBatchExtensions
{
    public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
    {
        var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });
        spriteBatch.Draw(pixel, rect, color);
    }

    public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, Color color)
    {
        var texture = CreateCircleTexture(spriteBatch.GraphicsDevice, (int)radius * 2, color);
        spriteBatch.Draw(texture, center - new Vector2(radius), color);
    }

    private static Texture2D CreateCircleTexture(GraphicsDevice device, int diameter, Color color)
    {
        var texture = new Texture2D(device, diameter, diameter);
        var colorData = new Color[diameter * diameter];
        var radius = diameter / 2f;

        for (int y = 0; y < diameter; y++)
        {
            for (int x = 0; x < diameter; x++)
            {
                var pos = new Vector2(x - radius, y - radius);
                colorData[y * diameter + x] = pos.Length() < radius ? color : Color.Transparent;
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    public static void DrawLine(this SpriteBatch spriteBatch, int x1, int y1, int x2, int y2, Color color)
    {
        var pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        var edge = new Vector2(x2 - x1, y2 - y1);
        var angle = MathHelper.ToRadians(90f) + (float)System.Math.Atan2(edge.Y, edge.X);
        spriteBatch.Draw(pixel, new Rectangle(x1, y1, (int)edge.Length(), 1), null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
    }
}