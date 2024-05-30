using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timer;


    public void Start()
    {
        
    }

    public void Update()
    {
        StartCoroutine(waittostart());
    }

    IEnumerator waittostart()
    {
        yield return new WaitForSeconds(3f);
        StartTimer();
    }

    void StartTimer()
    {
        timer += 1 * Time.deltaTime;
    }
}
