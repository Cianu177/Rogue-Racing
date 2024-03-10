using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager> {

	public bool mobileGame;
	public GameObject mobileControls;

	public LayerMask roadLayer;

	public float startDelay = 1f;

	public static event Action OnShipStart = delegate { };


	new void Awake () {
		base.Awake ();

		// Set FixedUpdate Time
		Time.fixedDeltaTime = 1f / 60f;

		// Make sure that we have EventSystem, needed for mobile controls
		if (mobileGame) {
			Instantiate (mobileControls);
			if (FindObjectOfType<EventSystem> () == null) {
				var es = new GameObject ("EventSystem", typeof (EventSystem));
				es.AddComponent<StandaloneInputModule> ();
			}
		}

	}


	IEnumerator Start () {
		yield return new WaitForSeconds (startDelay);
		OnShipStart ();
	}

}
