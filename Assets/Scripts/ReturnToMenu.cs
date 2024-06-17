using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void ReturnToMenuScene()
    {
        StartCoroutine(UnloadAllAndLoadMenu());
    }

    private IEnumerator UnloadAllAndLoadMenu()
    {
        // First, load the menu scene asynchronously
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }

        // Then, unload all other scenes except the menu scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "MenuScene")
            {
                AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(scene);
                while (!unloadOp.isDone)
                {
                    yield return null;
                }
            }
        }

        // Finally, set the menu scene as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScene"));
    }
}
