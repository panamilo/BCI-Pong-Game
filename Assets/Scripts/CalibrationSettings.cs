using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationSettings : MonoBehaviour
{
    public GameObject calibrationPanel;
    public Text instructionText;
    public Text counterText;
    public GameObject playerPaddle;
    private PlayerPaddle playerPaddleScript;

    private bool arrowsSelected;



    private void Start()
    {
        calibrationPanel.SetActive(false);
        // Ensure playerPaddle is assigned
        if (playerPaddle == null)
        {
            Debug.LogError("PlayerPaddle GameObject is not assigned in the Inspector!");
            return;
        }

        // Get the PlayerPaddle component
        playerPaddleScript = playerPaddle.GetComponent<PlayerPaddle>();

        if (playerPaddleScript == null)
        {
            Debug.LogError("PlayerPaddle script not found on the assigned PlayerPaddle GameObject!");
            return;
        }
    }

    public void StartCalibration()
    {
        Debug.Log("Starting calibration...");
        arrowsSelected = GameData.selectedControls == GameData.Controls.Arrows;
        if(arrowsSelected){
            Debug.Log("Arrows Selected. Skipping Calibration...");
            return;
        }
        calibrationPanel.SetActive(true);
        StartCoroutine(CalibrationSequence());
    }

    private IEnumerator CalibrationSequence()
    {
        // Right arm calibration
        instructionText.text = "Relax. Prepare to calibrate your right arm";
        yield return StartCoroutine(Countdown(5));
        for (int i = 0; i < 3; i++)
        {
            instructionText.text = "Use Right Arm";
            yield return StartCoroutine(Countdown(3));
            playerPaddleScript.CollectRightArmData();

            instructionText.text = "Relax";
            yield return StartCoroutine(Countdown(5));
        }

        // Rest before switching arms
        instructionText.text = "Relax. Prepare to calibrate your left arm";
        yield return StartCoroutine(Countdown(5));

        // Left arm calibration
        for (int i = 0; i < 3; i++)
        {
            instructionText.text = "Use Left Arm";
            yield return StartCoroutine(Countdown(3));
            playerPaddleScript.CollectLeftArmData();

            instructionText.text = "Relax";
            yield return StartCoroutine(Countdown(3));
        }

        // Final relaxation
        instructionText.text = "Calibration complete. Relax - Game is about to start...";
        yield return StartCoroutine(Countdown(10));

        // End calibration and hide the panel
        calibrationPanel.SetActive(false);
        // Optionally trigger any post-calibration actions here
    }

    private IEnumerator Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            counterText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        // Clear the counter after each countdown
        counterText.text = "";
    }
}
