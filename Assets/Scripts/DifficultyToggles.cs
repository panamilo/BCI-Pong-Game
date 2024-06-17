using UnityEngine;
using UnityEngine.UI;


public class DifficultyToggles : MonoBehaviour
{
    public Toggle easy;
    public Toggle medium;
    public Toggle hard;
    // Start is called before the first frame update
    void Start()
    {
        easy.isOn = true;
        medium.isOn = false;
        hard.isOn = false;
        easy.onValueChanged.AddListener(OnEasyChanged);
        medium.onValueChanged.AddListener(OnMediumChanged);
        hard.onValueChanged.AddListener(OnHardChanged);
    }

    // Update is called once per frame
     void OnEasyChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            medium.isOn = false;
            hard.isOn = false;
            GameData.selectedDifficulty = GameData.Difficulty.Easy;
        }
        else
        {
            // Prevent both toggles from being off
            if (!medium.isOn && !hard.isOn)
            {
                easy.isOn = true;
                GameData.selectedDifficulty = GameData.Difficulty.Easy;
            }
        }
    }

     void OnMediumChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            easy.isOn = false;
            hard.isOn = false;
            GameData.selectedDifficulty = GameData.Difficulty.Medium;
        }
        else
        {
            // Prevent both toggles from being off
            if (!easy.isOn && !hard.isOn)
            {
                medium.isOn = true;
                GameData.selectedDifficulty = GameData.Difficulty.Medium;
            }
        }
    }
     void OnHardChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            easy.isOn = false;
            medium.isOn = false;
            GameData.selectedDifficulty = GameData.Difficulty.Hard;
        }
        else
        {
            // Prevent both toggles from being off
            if (!easy.isOn && !medium.isOn)
            {
                hard.isOn = true;
                GameData.selectedDifficulty = GameData.Difficulty.Hard;
            }
        }
    }

}
