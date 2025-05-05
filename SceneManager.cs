using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class SceneManager
{
    private readonly Game1 _game;
    private readonly Dictionary<GameStates, IScene> _scenes = new();
    private IScene _currentScene;

    public SceneManager(Game1 game)
    {
        _game = game;
    }

    public void RegisterScene(GameStates state, IScene scene)
    {
        _scenes[state] = scene;
    }

    public void ChangeScene(GameStates newState)
    {
        if (_scenes.TryGetValue(newState, out var scene))
        {
            _currentScene = scene;
            _currentScene.Reset();
        }
    }

    public void UpdateScene(GameTime gameTime) => _currentScene?.Update(gameTime);
    public void DrawScene(SpriteBatch spriteBatch) => _currentScene?.Draw(spriteBatch);
}