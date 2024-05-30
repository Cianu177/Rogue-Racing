using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour {

    public Color color01;
    public Color color02;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        changeColor();
	}

    void changeColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(color01, color02, Mathf.PingPong(Time.time, 1));
        this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(color01, color02, Mathf.PingPong(Time.time, 1)));
    }
}
