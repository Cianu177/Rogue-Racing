using UnityEngine;
using UnityEditor;
using System.Linq;

[RequireComponent (typeof (BoxCollider))]
public class TurboPad : MonoBehaviour {

	public GameObject turboPrefab;
	public LayerMask roadLayers;
	[Range (0.05f, 0.2f)] public float snapHeight = 0.1f;

	private BoxCollider col;


	private void OnValidate () {
		if (!gameObject.activeInHierarchy) return;

		col = GetComponent<BoxCollider> ();
		col.isTrigger = true;
	}


	void OnTriggerEnter (Collider other) {
		other.GetComponentInParent<IShip> ()?.Turbo_On ();
	}


#if UNITY_EDITOR
	private Color gizmoColor;
	private Vector3 lastPos;
	private Quaternion lastRot;

	private void OnDrawGizmos () {
		if (transform.position != lastPos || transform.rotation != lastRot) {

			if (Physics.Raycast (transform.position, -transform.up, out RaycastHit hit, 256f, roadLayers)) {
				transform.position = hit.point;
				transform.rotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
				transform.position += transform.up * 6f;
				// Delete existing children
				var quadList = transform.Cast<Transform> ().ToList ();
				foreach (var child in quadList) DestroyImmediate (child.gameObject);
				// Instantiate new quad prefab
				GameObject clone = PrefabUtility.InstantiatePrefab (turboPrefab, transform) as GameObject;
				clone.transform.position = hit.point + (transform.up * snapHeight);
				clone.transform.rotation = Quaternion.FromToRotation (-transform.forward, hit.normal) * transform.rotation;
				if (col == null) col = GetComponent<BoxCollider> ();
				clone.transform.localScale = new Vector3 (col.size.x, col.size.z, 1f);
				// Change gizmo to happy color beacuse hit found a road
				gizmoColor = new Color (0.5f, 0f, 0.5f, 1f);
			} else {
				// Delete existing children
				var quadList = transform.Cast<Transform> ().ToList ();
				foreach (var child in quadList) DestroyImmediate (child.gameObject);
				// Change gizmo color to red because hit lost road
				gizmoColor = new Color (1f, 0.2f, 0f, 1f);
			}

			lastPos = transform.position;
			lastRot = transform.rotation;
		}

		Gizmos.color = gizmoColor;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube (Vector3.zero + col.center, new Vector3 (col.size.x, col.size.y, col.size.z));
	}
#endif

}
