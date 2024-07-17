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
        // Load the menu scene additively
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }

        // Set the menu scene as the active scene
        Scene menuScene = SceneManager.GetSceneByName("MenuScene");
        SceneManager.SetActiveScene(menuScene);

        // Unload all other scenes except the menu scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene != menuScene)
            {
                AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(scene);
                while (!unloadOp.isDone)
                {
                    yield return null;
                }
            }
        }

        // Reload the menu scene to ensure a clean state
        AsyncOperation reloadOp = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
        while (!reloadOp.isDone)
        {
            yield return null;
        }

        // Optionally, you can ensure that all persistent objects are destroyed before loading the menu scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Persistent")) // Assuming you tag persistent objects as "Persistent"
            {
                Destroy(obj);
            }
        }
    }
}
