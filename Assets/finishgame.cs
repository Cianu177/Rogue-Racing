using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishgame : MonoBehaviour
{
    public LapCounter lapCounter;
    public GameObject FinishUI;
    public GameTimer Timer;
    public void Start()
    {
        FinishUI.SetActive(false);
    }

    public void Update()
    {
        if (lapCounter.lapCount <= 3)
        {
            FinishUI.SetActive(true);
            Timer.PauseTimer();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
