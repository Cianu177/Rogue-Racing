using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;

    private void Update()
    {
        
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Paused");
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
