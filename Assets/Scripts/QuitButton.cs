using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void QuitGame()
    {
        // Check if we are running in the editor
        #if UNITY_EDITOR
            // If in the editor, stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If in a built game, quit the application
            Application.Quit();
        #endif
    }
}
