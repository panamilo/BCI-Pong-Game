using UnityEngine;
using UnityEngine.UI;

public class ScoreSettings : MonoBehaviour
{
    public Slider scoreSlider;
    public Text scoreValueText; // Text element to display the slider value

    private void Start()
    {
       
         // Set a default value if needed
        scoreSlider.value = GameData.scoreLimiterValue;
        UpdateSliderValueText();
        scoreSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

    }

    // This function is called when the slider value changes
    public void OnSliderValueChanged()
    {
      
        GameData.scoreLimiterValue = scoreSlider.value;
        UpdateSliderValueText();
    }
    private void UpdateSliderValueText()
    {
        if (scoreValueText != null)
        {
            scoreValueText.text = Mathf.RoundToInt(scoreSlider.value).ToString();        
        }
    }
}
