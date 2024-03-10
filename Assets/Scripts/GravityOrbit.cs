using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    //the centre that all objects orbit

    public float Gravity;

    public bool FixedDirection; //if the gravity of this section is only pushing the player down

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityCtrl>())
        {
            //if thi object has a gravity script, set this as the planet

            other.GetComponent<GravityCtrl>().Gravity = this.GetComponent<GravityOrbit>();

        }

    }
}
