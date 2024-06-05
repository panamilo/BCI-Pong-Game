using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenManager : MonoBehaviour
{
    private void Start()
    {
        // Load the main game scene additively
        SceneManager.LoadScene("PongGameScene", LoadSceneMode.Additive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "PongGameScene")
        {
            SceneManager.LoadScene("MenuScene");
        }
        else if (currentSceneName == "MenuScene")
        {
            Application.Quit();
        }
    }
    public void OnPlayButtonClicked()
    {
        // Unload the home screen scene
        SceneManager.UnloadSceneAsync("MenuScene");

        // Find the game scene
        Scene gameScene = SceneManager.GetSceneByName("PongGameScene");

        // Check if the game scene is valid
        if (gameScene.IsValid())
        {
            // Find the GAMECONTROLLER GameObject in the game scene
            GameObject[] rootObjects = gameScene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                if (obj.name == "GAMECONTROLLER")
                {
                    // Get the GameStartController component and start the game
                    GameStartController gameStartController = obj.GetComponent<GameStartController>();
                    if (gameStartController != null)
                    {
                        gameStartController.StartGame();
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Game scene is not valid.");
        }
    }
}
