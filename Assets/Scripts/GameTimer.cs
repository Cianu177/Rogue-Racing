using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timer;
    public bool isPaused = false;
    public TMP_Text TimerUI;
    public void Start()
    {
        
    }

    public void Update()
    {
        StartCoroutine(waittostart());

        string formattedTime = MinuteAndSeconds(timer);
    TimerUI.SetText(formattedTime.ToString());

     
}

    public string MinuteAndSeconds(float totaltime)
    {
        int minutes = Mathf.FloorToInt(totaltime / 60f);
        int seconds = Mathf.FloorToInt(totaltime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator waittostart()
    {
        yield return new WaitForSeconds(3f);
        StartTimer();
    }

    void StartTimer()
    {
     
        if (!isPaused)
        {
            timer += Time.deltaTime;
        }
        
    }

    public void PauseTimer()
    {
        isPaused = true;
    }


}

