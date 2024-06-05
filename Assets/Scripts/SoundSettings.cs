using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Toggle toggleOn;
    public Toggle toggleOff;
    public AudioSource audioSource;

    private static SoundSettings instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Set the initial state
        if (toggleOn.isOn)
        {
            AudioListener.volume = 1f;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (toggleOff.isOn)
        {
            AudioListener.volume = 0f;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Add listeners to handle value changes
        toggleOn.onValueChanged.AddListener(OnToggleChanged);
        toggleOff.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (toggleOn.isOn)
        {
            AudioListener.volume = 1f;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (toggleOff.isOn)
        {
            AudioListener.volume = 0f;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
