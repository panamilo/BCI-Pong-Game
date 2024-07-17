using UnityEngine;
using UnityEngine.UI; // Required for working with Text

public class EndingText : MonoBehaviour
{
    public Text endText;
    public GameData.gameEnding status; // Ensure this matches the enum type in GameData

    private void Awake()
    {
        // Ensure that there's only one instance of the EndingText object
        if (FindObjectsOfType<EndingText>().Length > 1)
        {
            Destroy(gameObject);
        }

        // Make the GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        status = GameData.selectedEnding;
        UpdateEndText(status);
    }

    void UpdateEndText(GameData.gameEnding status)
    {
        switch (status)
        {
            case GameData.gameEnding.Win:
                endText.text = "You Win!";
                endText.color = Color.green;
                break;
            case GameData.gameEnding.Lose:
                endText.text = "You Lose!";
                endText.color = Color.red;
                break;
            case GameData.gameEnding.Tie:
                endText.text = "It's a Tie!";
                endText.color = Color.yellow;
                break;
        }
    }
}