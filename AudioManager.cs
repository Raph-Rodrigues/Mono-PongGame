using Microsoft.Xna.Framework.Audio;

namespace Mono_PongGame;

public class AudioManager
{
    private SoundEffect _hitSound;
    private SoundEffect _scoreSound;
    
    public void LoadContent(Game1 game)
    {
        _hitSound = game.Content.Load<SoundEffect>("Hit SFX");
        _scoreSound = game.Content.Load<SoundEffect>("Ring");
    }

    public void PlayHitSound()
    {
        _hitSound?.Play();
    }

    public void PlayScoreSound()
    {
        _scoreSound?.Play();
    }
} 