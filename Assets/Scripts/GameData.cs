using UnityEngine;

public static class GameData
{
    // Static variable to store the timer value
    public static float timerValue = 3; // Default value, you can set this to any initial value you want
    public static float scoreLimiterValue = 3;

    // Enum to represent the difficulty levels
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    // Static variable to store the selected difficulty
    public static Difficulty selectedDifficulty = Difficulty.Easy;
}
