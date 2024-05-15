using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject PauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();

        }
        if (isPaused)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

    }

}


