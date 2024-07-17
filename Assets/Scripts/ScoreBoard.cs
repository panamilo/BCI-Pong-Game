using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreBoard : MonoBehaviour
{
    public  Text scoreText;

    public GameObject ball;
    private Vector3 ballInitialPosition;
    private float PlayerScore = 0;

    private float PcScore = 0;

    private float maxScore;
    // Start is called before the first frame update
    private void Awake()
    {

        // Ensure that there's only one instance of the CountdownTimer object
        if (FindObjectsOfType<ScoreBoard>().Length > 1)
        {
            Destroy(gameObject);
        }

        // Make the GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {   
        ballInitialPosition = ball.transform.position;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        maxScore = GameData.scoreLimiterValue;
        scoreText.text = "(You) " + PlayerScore + " - " + PcScore + " (PC)";
        if(PlayerScore >= maxScore){
            Debug.Log("You Win");
            GameData.selectedEnding = GameData.gameEnding.Win;
            EndGame();
        }else if(PcScore >= maxScore){
            Debug.Log("You Lose");
            GameData.selectedEnding = GameData.gameEnding.Lose;
            EndGame();
        }
    }
    
      public void IncrementPlayerScore()
    {
        PlayerScore++;
        UpdateScoreText();
        ResetPositions();
    }

    public void IncrementPCScore()
    {
        PcScore++;
        UpdateScoreText();
        ResetPositions();
    }
    void EndGame(){
        SceneManager.LoadScene("EndingScene");
    }
    void ResetPositions(){
        ball.transform.position = ballInitialPosition;
    }
}
