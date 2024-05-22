using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    public bool hasPassed;
    public LapCounter lapCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint!");
            hasPassed = true;
            lapCounter.checkpointCounter += 1;
        }
        
        

    }
}
