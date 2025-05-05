using Microsoft.Xna.Framework;

namespace Mono_PongGame;

public static class Constants
{
    // Colors
    public static Color Green = new Color(38, 185, 154);
    public static Color DarkGreen = new Color(20, 160, 133);
    public static Color LightGreen = new Color(129, 204, 184);
    public static Color Yellow = new Color(243, 213, 91);

    // Game Configurations
    public const int MaxScore = 10;
    public const int ScreenWidth = 1080;
    public const int ScreenHeight = 720;

    public static void ResetScores()
    {
        PlayerScore = 0;
        CpuScore = 0;
    }

    public static int PlayerScore { get; set; }
    public static int CpuScore { get; set; }
}