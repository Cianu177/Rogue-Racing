using UnityEngine;

public class SideWallCollider : MonoBehaviour {

	void OnCollisionStay (Collision collisionInfo) {
		collisionInfo.collider.GetComponentInParent<IShip> ()?.SlowDown (collisionInfo.GetContact (0).normal);
	}

}
