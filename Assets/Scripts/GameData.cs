using UnityEngine;

public static class GameData
{
    // Static variable to store the timer value
    public static float timerValue = 3; // Default value, you can set this to any initial value you want
    public static float scoreLimiterValue = 3;

    public static float thresholdRight = 1000f;
    public static float thresholdLeft = 1000f;

    // Enum to represent the difficulty levels
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    // Enum to represent the control types
    public enum Controls
    {
        Bci,
        Arrows
    }

    public enum gameEnding
    {
        Win,
        Lose,
        Tie
    }



    // Static variable to store the selected difficulty
    public static Difficulty selectedDifficulty = Difficulty.Easy;
    public static Controls selectedControls = Controls.Bci;
    public static gameEnding selectedEnding = gameEnding.Tie;
}
