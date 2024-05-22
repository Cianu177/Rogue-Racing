using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public GameObject[] checkpointsTransform;
    private void Awake()
    {
        GameObject[] checkpointsTransform = GameObject.FindGameObjectsWithTag("Checkpoints");
        
        foreach (GameObject checkpointSingleTrasnform in checkpointsTransform)
        {
            Debug.Log(checkpointsTransform);

        }

       
    }
}
