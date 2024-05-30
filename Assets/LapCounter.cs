using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LapCounter : MonoBehaviour
{
    public int checkpointCounter;
    public int lapCount;
    public GameObject FinishUI;
    public GameTimer Timer;
    // Update is called once per frame
    void Update()
    {
        if (checkpointCounter >= 10)
        {
            lapCount += 1;
            checkpointCounter = 0;
        }

        if (lapCount >= 3)
        {
            FinishUI.SetActive(true);
            Timer.PauseTimer();
        }

        if (lapCount <= 3)
        {
            FinishUI.SetActive(false);
        }
     
    }
}