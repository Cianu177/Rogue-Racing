using UnityEngine;

public class RacerCamera : MonoBehaviour {

	public Transform playerBody;
	private const float cameraSmoothTime = 0.005f;
	private Vector3 cameraVelocity = new Vector3 (0f, 0f, 0f);

	void Start () {
		if (playerBody == null) playerBody = GameObject.FindGameObjectWithTag ("Player").transform;
		transform.position = playerBody.transform.position;
		transform.rotation = playerBody.transform.rotation;
	}

	void FixedUpdate () {
		if (playerBody != null) {
			transform.position = Vector3.SmoothDamp (transform.position, playerBody.position, ref cameraVelocity, cameraSmoothTime);
			transform.rotation = Quaternion.Slerp (transform.rotation, playerBody.rotation, 0.12f);
		}
	}

}
