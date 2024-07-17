using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    public GameObject PlayerPaddle;
    public GameObject ComputerPaddle;
    public GameObject Ball;
    public GameObject Timer;
    public GameObject Canvas;
    public GameObject ScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        // Disable movement/physics components on paddles and ball
        DisableComponents(PlayerPaddle);
        DisableComponents(ComputerPaddle);
        DisableComponents(Ball);
        DisableTimer();
        DisableScoreBoard();
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
    UpdateControls(GameData.selectedControls);
    EnableTimer();
    EnableScoreBoard();
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

    void DisableScoreBoard()
    {
        ScoreBoard scoreBoardScript = ScoreBoard.GetComponent<ScoreBoard>();
        if (scoreBoardScript != null)
        {
            scoreBoardScript.enabled = false;
        }
        else
        {
            Debug.LogError("scoreBoard script not found!");
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

    void EnableScoreBoard()
    {
        ScoreBoard scoreBoardScript = ScoreBoard.GetComponent<ScoreBoard>();
        if (scoreBoardScript != null)
        {
            scoreBoardScript.enabled = true;
        }
        else
        {
            Debug.LogError("scoreBoard script not found!");
        }
    }
     void UpdateControls(GameData.Controls selectedControls)
    {
        PlayerPaddleArrowKeys PlayerPaddleArrowKeysScript = PlayerPaddle.GetComponent<PlayerPaddleArrowKeys>();
        PlayerPaddle PlayerPaddleScript = PlayerPaddle.GetComponent<PlayerPaddle>();
         switch (selectedControls)
        {
            case GameData.Controls.Bci:
                PlayerPaddleScript.enabled = true;
                PlayerPaddleArrowKeysScript.enabled = false;
                Debug.Log("BCI");
                break;
            case GameData.Controls.Arrows:
                PlayerPaddleScript.enabled = false;
                PlayerPaddleArrowKeysScript.enabled = true;
                Debug.Log("Arrows");
                break;
            default:
                PlayerPaddleScript.enabled = true;
                PlayerPaddleArrowKeysScript.enabled = false;
                Debug.Log("Default");
                break;
        }
    }
    void handleDifficulty(Ball ballScript, ComputerPaddle computerPaddleScript){
    Debug.Log("Selected Difficulty: " + GameData.selectedDifficulty);
    switch (GameData.selectedDifficulty)
    {
        case GameData.Difficulty.Easy:
            ballScript.speed = 110.0f;
            computerPaddleScript.ComputerPaddleSpeed = 2.5f;
            computerPaddleScript.reactionDelay = 3.0f;
            break;
        case GameData.Difficulty.Medium:
            ballScript.speed = 200.0f;
            computerPaddleScript.ComputerPaddleSpeed = 8.0f;
            computerPaddleScript.reactionDelay = 1.5f;
            break;
        case GameData.Difficulty.Hard:
            ballScript.speed = 400.0f;
            computerPaddleScript.ComputerPaddleSpeed = 14.0f;
            computerPaddleScript.reactionDelay = 0.5f;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "PongGameScene")
        {
            #if UNITY_EDITOR
            // If in the editor, stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If in a built game, quit the application
            Application.Quit();
        #endif
        }
    }
}