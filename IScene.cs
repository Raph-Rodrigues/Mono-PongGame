using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public interface IScene
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void Reset();
}
