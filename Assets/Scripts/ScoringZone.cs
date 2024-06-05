// using UnityEngine;
// using UnityEngine.EventSystems;

// public class ScoringZone : MonoBehaviour
// {
//     public EventTrigger.TriggerEvent scoreTrigger;
//     private void OnCollisionEnter2D(Collision2D collision) 
//     {
//         Ball ball = collision.gameObject.GetComponent<Ball>();

//         if(ball != null)
//         {
//             BaseEventData eventData = new BaseEventData(EventSystem.current);
//             this.scoreTrigger.Invoke(eventData);
//         }
//     } 
// }



//========================================================================================================================

using UnityEngine;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{
    public EventTrigger.TriggerEvent scoreTrigger;

    private ScoreBoard scoreBoard;
    public bool isTopWall;

    private void Start()
    {
        scoreBoard = GameObject.FindObjectOfType<ScoreBoard>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.scoreTrigger.Invoke(eventData);

            if (isTopWall)
            {
                scoreBoard.IncrementPlayerScore();
            }
            else
            {
                scoreBoard.IncrementPCScore();
            }
        }
    }
}

