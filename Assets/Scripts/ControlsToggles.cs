using UnityEngine;
using UnityEngine.UI;


public class ControlsToggles : MonoBehaviour
{
    public Toggle bci;
    public Toggle arrows;
    // Start is called before the first frame update
    void Start()
    {
        bci.isOn = true;
        arrows.isOn = false;
        bci.onValueChanged.AddListener(OnBciChanged);
        arrows.onValueChanged.AddListener(OnArrowsChanged);
    }

    // Update is called once per frame
     void OnBciChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            arrows.isOn = false;
            GameData.selectedControls = GameData.Controls.Bci;
        }
        else
        {
            // Prevent both toggles from being off
            if (!arrows.isOn)
            {
                bci.isOn = true;
                GameData.selectedControls = GameData.Controls.Bci;
            }
        }
    }

     void OnArrowsChanged(bool isOn)
    {
        if (isOn)
        {
            // Ensure the other toggle is off
            bci.isOn = false;
            GameData.selectedControls = GameData.Controls.Arrows;
        }
        else
        {
            // Prevent both toggles from being off
            if (!bci.isOn)
            {
                arrows.isOn = true;
                GameData.selectedControls = GameData.Controls.Arrows;
            }
        }
    }
}
