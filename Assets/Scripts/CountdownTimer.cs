using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text timerText;

    private float currentTime;

    private void Awake()
    {
        // Ensure that the timer text is assigned before Start method
        if(timerText == null)
        {
            Debug.LogError("Timer Text is not assigned in CountdownTimer script!");
        }

        // Ensure that there's only one instance of the CountdownTimer object
        if (FindObjectsOfType<CountdownTimer>().Length > 1)
        {
            Destroy(gameObject);
        }

        // Make the GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentTime = GameData.timerValue * 60; // Convert minutes to seconds
        UpdateTimerText();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        if(currentTime <= 0)
        {
            // Load the ending scene
            SceneManager.LoadScene("EndingScene");
        }
    }

    void UpdateTimerText()
    {
        if(timerText == null)
        {
            return;
        }
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
