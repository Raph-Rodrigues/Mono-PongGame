using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace Mono_PongGame;

public static class Input
{
    private static KeyboardState _currentState;
    private static KeyboardState _previousState;

    public static void Update()
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();
    }

    public static bool IsKeyDown(Keys key) => _currentState.IsKeyDown(key);

    public static bool IsKeyJustPressed(Keys key) => _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
}
