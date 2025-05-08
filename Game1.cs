using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_PongGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public SceneManager SceneManager { get; private set; }
    private SpriteFont _scoreFont;
    private SpriteFont _titleFont;
    public AudioManager AudioManager { get; private set; }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
        _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        SceneManager = new SceneManager(this);
        AudioManager = new AudioManager();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _scoreFont = Content.Load<SpriteFont>("Score");
        _titleFont = Content.Load<SpriteFont>("Title");
        AudioManager.LoadContent(this);

        // Register all the scenes first
        SceneManager.RegisterScene(GameStates.Menu, new MenuScene(this, _titleFont));
        SceneManager.RegisterScene(GameStates.Play, new PlayScene(this, _scoreFont));
        SceneManager.RegisterScene(GameStates.GameOver, new GameOverScene(this, _titleFont));
        
        // Starts with Menu Scene
        SceneManager.ChangeScene(GameStates.Menu);
    }

    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        SceneManager.UpdateScene(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Constants.DarkGreen);
        _spriteBatch.Begin();
        SceneManager.DrawScene(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}