using UnityEngine;
using UnityEngine.UI;

public class TimerSettings : MonoBehaviour
{
    public Slider timerSlider;
    public Text sliderValueText; // Text element to display the slider value

    private void Start()
    {
       
         // Set a default value if needed
        timerSlider.value = GameData.timerValue;
        UpdateSliderValueText();
        timerSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

    }

    // This function is called when the slider value changes
    public void OnSliderValueChanged()
    {
      
        GameData.timerValue = timerSlider.value;
        UpdateSliderValueText();
    }
    private void UpdateSliderValueText()
    {
        if (sliderValueText != null)
        {
            sliderValueText.text = string.Format("{0:00}:00", timerSlider.value);
        }
    }
}
