using UnityEngine;

public class GameStartController : MonoBehaviour
{
    public GameObject PlayerPaddle;
    public GameObject ComputerPaddle;
    public GameObject Ball;
    public GameObject Timer;
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Disable movement/physics components on paddles and ball
        DisableComponents(PlayerPaddle);
        DisableComponents(ComputerPaddle);
        DisableComponents(Ball);
        DisableTimer();
        HideCanvas();
    }

    void DisableComponents(GameObject obj)
    {
        // Disable Rigidbody components to freeze objects
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Freeze movement along X and Y axes
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Disable any other movement or physics-related components
        // For example, disable scripts that control movement
    }

    public void StartGame()
{

    // Enable movement/physics components on paddles and ball
    
    Ball ballScript = Ball.GetComponent<Ball>();
    ComputerPaddle computerPaddleScript = ComputerPaddle.GetComponent<ComputerPaddle>();
        if (ballScript != null)
        {
            handleDifficulty(ballScript, computerPaddleScript);
            ballScript.StartBall();
            
        }
        else
        {
            Debug.LogError("Ball script not found!");
        }
    EnableComponents(Ball);
    EnableComponents(PlayerPaddle);
    EnableComponents(ComputerPaddle);
    EnableTimer();
    ShowCanvas();
    
    }

    void DisableTimer()
    {
        CountdownTimer timerScript = Timer.GetComponent<CountdownTimer>();
        if (timerScript != null)
        {
            timerScript.enabled = false;
        }
        else
        {
            Debug.LogError("Timer script not found!");
        }
    }

    void HideCanvas()
    {
        Canvas.SetActive(false);
    }

    void ShowCanvas()
    {
        Canvas.SetActive(true);
    }

    void EnableTimer()
    {
        CountdownTimer timerScript = Timer.GetComponent<CountdownTimer>();
        if (timerScript != null)
        {
            timerScript.enabled = true;
        }
        else
        {
            Debug.LogError("Timer script not found!");
        }
    }

    void handleDifficulty(Ball ballScript, ComputerPaddle computerPaddleScript){
    Debug.Log("Selected Difficulty: " + GameData.selectedDifficulty);
    switch (GameData.selectedDifficulty)
    {
        case GameData.Difficulty.Easy:
            ballScript.speed = 80.0f;
            computerPaddleScript.ComputerPaddleSpeed = 3.0f;
            computerPaddleScript.reactionDelay = 1.0f;
            break;
        case GameData.Difficulty.Medium:
            ballScript.speed = 200.0f;
            computerPaddleScript.ComputerPaddleSpeed = 8.0f;
            computerPaddleScript.reactionDelay = 0.5f;
            break;
        case GameData.Difficulty.Hard:
            ballScript.speed = 2000.0f;
            computerPaddleScript.ComputerPaddleSpeed = 15.0f;
            computerPaddleScript.reactionDelay = 0.1f;
            break;
    }    
}
    void EnableComponents(GameObject obj)
    {
        // Enable Rigidbody components to allow objects to move
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            // Unfreeze only the X-axis constraints for paddles
            if (obj == PlayerPaddle || obj == ComputerPaddle)
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
            // Unfreeze both X and Y axes constraints for the ball
            else if (obj == Ball)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }

        // Enable any other movement or physics-related components
        // For example, enable scripts that control movement
    }


}