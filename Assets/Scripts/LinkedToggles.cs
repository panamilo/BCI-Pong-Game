using UnityEngine;
using UnityEngine.UI;

public class LinkedToggles : MonoBehaviour
{
    public Toggle toggleOn;
    public Toggle toggleOff;

    void Start()
    {
        // Set the initial state
        toggleOn.isOn = true; // Sound is initially on
        toggleOff.isOn = false;

        // Add listeners to handle value changes
        toggleOn.onValueChanged.AddListener(OnToggleOnChanged);
        toggleOff.onValueChanged.AddListener(OnToggleOffChanged);
    }

    void OnToggleOnChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            toggleOff.isOn = false;
            // Unmute the sound
            AudioListener.volume = 1f;
        }
        else
        {
            // Prevent both toggles from being off
            if (!toggleOff.isOn)
            {
                toggleOn.isOn = true;
            }
        }
    }

    void OnToggleOffChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            toggleOn.isOn = false;
            // Mute the sound
            AudioListener.volume = 0f;
        }
        else
        {
            // Prevent both toggles from being off
            if (!toggleOn.isOn)
            {
                toggleOff.isOn = true;
            }
        }
    }
}
