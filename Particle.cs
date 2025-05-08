using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public float Size;
    public float Life;
    public float MaxLife;

    public Particle(Vector2 position, Vector2 velocity, Color color, float size, float life)
    {
        this.Position = position;
        this.Velocity = velocity;
        this.Color = color;
        this.Size = size;
        this.Life = life;
    }

    public void Update(float deltaTime)
    {
        Position += Velocity * deltaTime;
        Life -= deltaTime;
        Size *= 0.95f; // Gradually decrease size
        Color *= 0.95f; // Gradually fade out
    }

    public bool IsDead => Life <= 0;
}