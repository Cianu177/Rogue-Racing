using JetBrains.Annotations;
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
    public GameObject Finishline;
    public bool finalLap;
    // Update is called once per frame

    private void Start()
    {
        FinishUI.SetActive(false);
    }
    void Update()
    {
        if (lapCount == 3)
        {
            finalLap = true;
        }
        if (checkpointCounter >= 10)
        {
            lapCount += 1;
            checkpointCounter = 0;
        }

        

        if (lapCount==4 )
        {
            StartCoroutine(FinishUIfadeinfadeout());
            Debug.Log("Has Finished");
        }

        if (Finishline == null)
        {
            return;
        }

    }
    public IEnumerator FinishUIfadeinfadeout()
    {
        Finishline.SetActive(true);
        yield return new WaitForSeconds(3f);
        Finishline.SetActive(false);
    }
}
