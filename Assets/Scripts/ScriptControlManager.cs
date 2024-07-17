using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptControlManager : MonoBehaviour
{

    public MonoBehaviour bciControls;
    public MonoBehaviour arrowsControls;
    // Start is called before the first frame update
    void Start()
    {
        UpdateControls(GameData.selectedControls);
    }

    // Update is called once per frame
    public void UpdateControls(GameData.Controls selectedControls)
    {
        switch (selectedControls)
        {
            case GameData.Controls.Bci:
                bciControls.enabled = true;
                arrowsControls.enabled = false;
                Debug.Log("BCI");
                break;
            case GameData.Controls.Arrows:
                bciControls.enabled = false;
                arrowsControls.enabled = true;
                Debug.Log("Arrows");
                break;
            default:
                bciControls.enabled = true;
                arrowsControls.enabled = false;
                Debug.Log("Default");
                break;
        }
    }
}