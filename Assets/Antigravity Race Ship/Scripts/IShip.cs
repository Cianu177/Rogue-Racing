using UnityEngine;
public interface IShip {
	Transform ShipBody { get; }
	void Turbo_On ();
	void Turbo_Off ();
	void SlowDown (Vector3 hitPosition);
}
