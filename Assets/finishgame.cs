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
        Debug.Log("FinishUI Inactive");
    }

    public void Update()
    {
        if (lapCounter.lapCount == 4)
        {
            this.gameObject.SetActive(true);
         
           
    
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FinishFade());
        }

    }
    public IEnumerator FinishFade()
    {
        Timer.PauseTimer();
        FinishUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //enter upgrade ui
        
    }
}
