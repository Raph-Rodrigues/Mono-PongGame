using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class ParticleSystem
{
    private readonly List<Particle> _particles;
    private readonly Random _random;
    private readonly Texture2D _particleTexture;

    public ParticleSystem(GraphicsDevice graphicsDevice)
    {
        _particles = new();
        _random = new();

        // Create a simple white circle texture for particles
        _particleTexture = new Texture2D(graphicsDevice, 8, 8);
        var colorData = new Color[8 * 8];
        for (int i = 0; i < colorData.Length; i++)
        {
            var x = i % 8;
            var y = i / 8;
            var distance = Vector2.Distance(new Vector2(x, y), new Vector2(4, 4));
            colorData[i] = distance <= 4 ? Color.White : Color.Transparent;
        }
        _particleTexture.SetData(colorData);
    }

    public void Emit(Vector2 position, int count, Color color)
    {
        for (int i = 0; i < count; i++)
        {
            var angle = _random.NextSingle() * MathHelper.TwoPi;
            var speed = _random.NextSingle() * 200f + 100f;
            var velocity = new Vector2((float)Math.Cos(angle) * speed,
                                        (float)Math.Sin(angle) * speed);

            var size = _random.NextSingle() * 4f + 2f;
            var life = _random.NextSingle() * 0.5f + 0.3f;

            _particles.Add(new Particle(position, velocity, color, size, life));
        }
    }

    public void Update(float deltaTime)
    {
        for (int i = _particles.Count - 1; i >= 0; i--)
        {
            _particles[i].Update(deltaTime);
            if (_particles[i].IsDead)
            {
                _particles.RemoveAt(i);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var particle in _particles)
        {
            var scale = particle.Size / 8f; // 8 is the texture size
            var origin = new Vector2(4, 4); // Center of the texture
            spriteBatch.Draw(_particleTexture, particle.Position, null, particle.Color, 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
}