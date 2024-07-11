using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	[SerializeField] private Text posText;
	[SerializeField] private Text infoText;
	[SerializeField] private Text lapCount;
	[Space (10)]
	[SerializeField] private DistanceTracker playerShip;
	private Transform playerBody;
	[Space(10)]
	[SerializeField] private DistanceTracker[] aiShips;
	private float playerDistance;
	private int maxPos;
	private int racePosition;
	private int oldRacePos;
	private bool vwVisible;


	void OnEnable () => GameManager.OnShipStart += ShowStart;
	void OnDisable () => GameManager.OnShipStart -= ShowStart;


	private void Start () {
		playerBody = playerShip.GetComponent<IShip> ().ShipBody;
	}


	// Calculate Racing Position
	IEnumerator PositionCheck () {
		maxPos = aiShips.Length + 1;
		WaitForSeconds delay = new WaitForSeconds (0.1f);
		while (true) {
			racePosition = maxPos;
			playerDistance = playerShip.progressDistance;
			// Calculate Position
			for (int i = 0; i < aiShips.Length; ++i) {
				if (aiShips[i].progressDistance < playerDistance) {
					racePosition--;
				}
			}
			if (oldRacePos != racePosition) {
				oldRacePos = racePosition;
				posText.text = racePosition.ToString ();
			}

			yield return delay;
		}
	}

	// Wrong Way Detection
	IEnumerator WrongWayCheck () {
		WaitForSeconds delay = new WaitForSeconds (0.1f);
		while (true) {
			if (Vector3.Angle (playerBody.forward, playerShip.tWay) > 120f) {
				if (!vwVisible) {
					infoText.text = "WRONG WAY!";
					vwVisible = true;
				}
			} else {
				if (vwVisible) {
					infoText.text = "";
					vwVisible = false;
				}
			}

			yield return delay;
		}
	}


	// Update is called once per frame
	void ShowStart () {
		Invoke ("HideInfo", 2f);

		if(playerShip != null && aiShips.Length != 0) StartCoroutine (PositionCheck ());
		if (playerShip != null) StartCoroutine (WrongWayCheck ());
	}

	void HideInfo () {
		infoText.text = " ";
	}
}
