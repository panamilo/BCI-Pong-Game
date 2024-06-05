using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    public  Text scoreText;
    public int PlayerScore = 0;

    public int PcScore = 0;

    public int maxScore;
    // Start is called before the first frame update
    void Start()
    {   
        maxScore = (int)GameData.scoreLimiterValue;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "(You) " + PlayerScore + " - " + PcScore + " (PC)";
        if(PlayerScore == maxScore){
            Debug.Log("You Win");
        }else if(PcScore == maxScore){
            Debug.Log("You Lose");
        }
    }
    
      public void IncrementPlayerScore()
    {
        PlayerScore++;
        UpdateScoreText();
    }

    public void IncrementPCScore()
    {
        PcScore++;
        UpdateScoreText();
    }
    void EndGame(){
        
    }
}
