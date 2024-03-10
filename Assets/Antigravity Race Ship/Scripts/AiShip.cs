using UnityEngine;

public class AiShip : MonoBehaviour, IShip {
	// Setup
	[SerializeField] private bool rigidBodyHover = true;
	[SerializeField] private bool bounceFromWall;

	[Header ("Ship Parameters:")]
	[Range (0.005f, 0.02f)] public float acceleration = 0.01f;
	[Range (6000f, 10000f)] public float normalThrust = 8000f;
	private float turboThrust;
	private float collideThrust;
	[Range (1f, 4f)] public float turboTime = 2.5f;
	[Range (4f, 6f)] public float hoverHeight = 4f;
	[Header ("Ship Tilt On Turn:")]
	[Range (0, 0.4f)] public float maxTilt = 0.3f;
	private Quaternion tiltRot;
	private float tilt;
	private float tiltVec;
	private float tiltAngle;

	[Header ("Engine Sound Parameters:")]
	[SerializeField] private float minVolume = 0.1f;
	[SerializeField] private float maxVolume = 0.6f;
	[SerializeField] private float minPitch = 0.3f;
	[SerializeField] private float maxPitch = 0.8f;
	private float sfxLerp;
	private AudioSource engineAudio;

	// HOVERING
	private float maximumHeight;
	private LayerMask roadLayer;
	private float fakeGrav;
	private Quaternion rotateToRoad;
	private RaycastHit hit;

	[Header ("Engine Particle Parameters:")]
	[SerializeField] private float minParticleSpeed = 4f;
	[SerializeField] private float maxParticleSpeed = 16f;

	[Header ("Ship Objects:")]
	public Transform _shipBody;
	public Transform ShipBody {
		get { return _shipBody; }
		set { _shipBody = value; }
	}
	public Transform wayPoint;
	public ParticleSystem afterBurner;
	private ParticleSystem.MainModule module;
	[HideInInspector] public bool inRace;
	private float thrust;
	private float thrustForce;
	private float accel;
	private float turboAcceleration = 0.1f;

	private Rigidbody rb;
	private Transform lookHelp;


	void Awake () {
		// Get RigidBody
		rb = GetComponent<Rigidbody> ();
		// Set Sound
		engineAudio = GetComponent<AudioSource> ();
		// Set Particle Module
		module = afterBurner.main;
	}

	void OnEnable () => GameManager.OnShipStart += StartRace;
	void OnDisable () => GameManager.OnShipStart -= StartRace;
	private void StartRace () => inRace = true;


	void Start () {
		thrust = normalThrust;
		accel = acceleration;
		turboThrust = normalThrust * 1.3f;
		collideThrust = normalThrust * 0.5f;
		maximumHeight = hoverHeight * 2f;

		// SET HELP TRANSFORM
		lookHelp = new GameObject ().transform;
		lookHelp.parent = transform;
		lookHelp.localPosition = Vector3.zero;

		// HOVERING
		roadLayer = GameManager.Instance.roadLayer;
	}


	void Update () {
		// Engine Sound & VFX
		sfxLerp = thrustForce / turboThrust;
		engineAudio.volume = Mathf.Lerp (minVolume, maxVolume, sfxLerp);
		engineAudio.pitch = Mathf.Lerp (minPitch, maxPitch, sfxLerp);
		module.startSpeed = Mathf.Lerp (minParticleSpeed, maxParticleSpeed, sfxLerp);
	}


	void FixedUpdate () {
		// HOVERING & ROTATE
		if (Physics.Raycast (transform.position, -transform.up, out hit, maximumHeight, roadLayer)) {
			lookHelp.LookAt (hoverHeight * hit.normal + wayPoint.position);
			rotateToRoad = Quaternion.LookRotation (lookHelp.forward, hit.normal);

			if (rigidBodyHover) {
				rb.MoveRotation (Quaternion.Lerp (rb.rotation, rotateToRoad, 0.1f));
				rb.MovePosition (Vector3.Lerp (rb.position, transform.up * hoverHeight + hit.point, 0.5f));
			} else {
				transform.rotation = Quaternion.Lerp (transform.rotation, rotateToRoad, 0.1f);
				transform.position = Vector3.Lerp (transform.position, transform.up * hoverHeight + hit.point, 0.5f);
			}

		} else {    // Fake Gravity Fall and Turn
			fakeGrav = Vector3.Dot (transform.forward, Vector3.up);
			fakeGrav = fakeGrav > 0f ? fakeGrav : 0f;
			rb.AddForce (Vector3.down * ((1000f * fakeGrav) + 1000f));

			if (rigidBodyHover) {
				rb.MoveRotation (Quaternion.Slerp (rb.rotation, Quaternion.LookRotation (rb.velocity.normalized), 0.1f));
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (rb.velocity.normalized), 0.1f);
			}
			
		}

		if (!inRace) return;

		// THRUST
		thrustForce = Mathf.Lerp (thrustForce, thrust, accel);
		rb.AddForce (_shipBody.forward * thrustForce);

		// TILT
		tiltAngle = tiltVec - transform.eulerAngles.y; // Checking heading changes from last fixedupdate
		tiltRot = _shipBody.localRotation;
		if (Mathf.Abs (tiltAngle) >= 0.3f) {
			if (tiltAngle > 0)
				tilt = Mathf.Lerp (tilt, maxTilt, 0.1f);
			else if (tiltAngle < 0)
				tilt = Mathf.Lerp (tilt, -maxTilt, 0.1f);
		} else tilt = Mathf.Lerp (tilt, 0, 0.2f);

		tiltRot.z = tilt;
		// Tilt the ship via heading change
		_shipBody.localRotation = Quaternion.Slerp (_shipBody.localRotation, tiltRot, 0.03f);

		tiltVec = transform.eulerAngles.y;

	} // END of FIXED UPDATE


	// TURBO Operations
	public void Turbo_On () {
		thrust = turboThrust;
		accel = turboAcceleration;
		CancelInvoke ("Turbo_Off");
		Invoke ("Turbo_Off", turboTime);
	}

	public void Turbo_Off () {
		thrust = normalThrust;
		accel = acceleration;
	}


	public void SlowDown (Vector3 hitNormal) {
		thrustForce = Mathf.Lerp (thrustForce, collideThrust, 0.1f);

		if (!bounceFromWall) return;

		// Bounce from wall
		rb.AddForce (5 * -hitNormal, ForceMode.VelocityChange);
		// Turn away from wall
		if (Vector3.Dot (hitNormal, rb.velocity.normalized) > 0)
			rb.MoveRotation (Quaternion.Slerp (rb.rotation, Quaternion.LookRotation (-hitNormal, transform.up), 0.01f));
	}

}
