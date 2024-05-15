using UnityEngine;

public class RaceShip : MonoBehaviour, IShip {

	public bool autoSpeed;

	[SerializeField] private bool rigidBodyHover = true;
	[SerializeField] private bool bounceFromWall = true;	

	// Setup
	[Header ("Ship Parameters:")]
	[Range (0.1f, 0.5f)] public float drift = 0.3f;
	[Range (1000f, 3000f)] public float turn = 1500f;
	[Range (0.005f, 0.02f)] public float acceleration = 0.01f;
	[Range (0.05f, 0.1f)] public float brakePower = 0.075f;
	[Range (6000, 10000)] public int normalThrust = 8000;
	private float turboThrust;
	private float collideThrust;
	[Range (1f, 4f)] public float turboTime = 2.5f;
	[Header ("Hover Parameters:")]
	[Range (4f, 6f)] public float hoverHeight = 4f;
	[Range (0, 0.4f)] public float maxTilt = 0.2f;

	[Header ("Engine Sound Parameters:")]
	[SerializeField] private float minVolume = 0.1f;
	[SerializeField] private float maxVolume = 0.6f;
	[SerializeField] private float minPitch = 0.3f;
	[SerializeField] private float maxPitch = 0.8f;
	private float sfxLerp;
	private AudioSource engineAudio;

	[Header ("Engine Particle Parameters:")]
	[SerializeField] private float minParticleSpeed = 4f;
	[SerializeField] private float maxParticleSpeed = 16f;


	[Header ("Ship Objects:")]
	public Transform _shipBody;
	public Transform ShipBody {
		get { return _shipBody; }
		set { _shipBody = value; }
	}
	public ParticleSystem afterBurner;
	private ParticleSystem.MainModule module;

	private bool inRace;
	private float thrust;
	private float manualThrust;
	private float thrustForce;
	private Vector3 smoothVector;
	private Vector3 vectorVelocity;
	private Quaternion rotateToRoad;
	private float fakeGrav;
	private float maximumHeight;
	private Rigidbody rb;
	private LayerMask roadLayer;
	private Quaternion tiltRot;
	private bool mobile;


	void Awake () {
		// Get RigidBody
		rb = GetComponent<Rigidbody> ();
		// Set Sound
		engineAudio = GetComponent<AudioSource>();
		// Set Particle Module
		module = afterBurner.main;
	}

	void OnEnable () => GameManager.OnShipStart += StartRace;
	void OnDisable () => GameManager.OnShipStart -= StartRace;

	private void StartRace () => inRace = true;


	void Start () {
		thrust = normalThrust;
		turboThrust = normalThrust * 1.3f;
		collideThrust = normalThrust * 0.5f;
		maximumHeight = hoverHeight * 2f;
		// HOVERING LAYER
		roadLayer = GameManager.Instance.roadLayer;
		// If mobile controls present
		if (GameManager.Instance.mobileGame) {
			mobile = true;
			autoSpeed = true;
		}
	}


	void Update () {
		// ENGINE SOUND & VFX
		sfxLerp = thrustForce / turboThrust;
		engineAudio.volume = Mathf.Lerp(minVolume, maxVolume, sfxLerp);
		engineAudio.pitch = Mathf.Lerp(minPitch, maxPitch, sfxLerp);
		module.startSpeed = Mathf.Lerp(minParticleSpeed, maxParticleSpeed, sfxLerp);
	}


	void FixedUpdate () {
		// HOVERING
		if (Physics.Raycast (transform.position, -transform.up, out RaycastHit hit, maximumHeight, roadLayer)) {
			rotateToRoad = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;

			if (rigidBodyHover) {
				rb.MoveRotation (Quaternion.Lerp (rb.rotation, rotateToRoad, 0.2f));
				rb.MovePosition (Vector3.Lerp (rb.position, transform.up * hoverHeight + hit.point, 0.5f));
			} else {
				transform.rotation = Quaternion.Lerp (transform.rotation, rotateToRoad, 0.2f);
				transform.position = Vector3.Lerp (transform.position, transform.up * hoverHeight + hit.point, 0.6f);
			}
			
		} else {    // FAKE GRAVITY FALL AND TURN
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

		// TURN & TILT
		if (mobile) {
			rb.AddTorque (transform.up * turn * (LeftTurn.instance.Volume + RightTurn.instance.Volume));
			tiltRot.Set (0f, 0f, -maxTilt * (LeftTurn.instance.Volume + RightTurn.instance.Volume), 1f);
		} else {
			rb.AddTorque (transform.up * turn * Input.GetAxis ("Horizontal"));
			tiltRot.Set (0f, 0f, -maxTilt * Input.GetAxis ("Horizontal"), 1f);
		}
		_shipBody.localRotation = Quaternion.Lerp (_shipBody.localRotation, tiltRot, 0.2f);

		// THRUST
		if (autoSpeed) {

			if (mobile) {
				if (Brake.instance.On)
					thrustForce = Mathf.Lerp (thrustForce, 0, brakePower);
				else
					thrustForce = Mathf.Lerp (thrustForce, thrust, acceleration);
			} else if (Input.GetAxisRaw ("Vertical") < 0)
				thrustForce = Mathf.Lerp (thrustForce, 0, brakePower);
			else thrustForce = Mathf.Lerp (thrustForce, thrust, acceleration);

		} else {    // IF NOT AUTO ACCELERATION

			manualThrust = thrust * Input.GetAxisRaw("Vertical");
			if (manualThrust < 0) {
				thrustForce = Mathf.Lerp (thrustForce, 0, brakePower);
			} else if (manualThrust > 0) {
				thrustForce = Mathf.Lerp (thrustForce, manualThrust, acceleration);
			} else {
				thrustForce = Mathf.Lerp (thrustForce, 0, 0.01f);
			}

		}

		// ADD THRUST FORCE
		smoothVector = Vector3.SmoothDamp (smoothVector, transform.forward, ref vectorVelocity, drift);
		rb.AddForce (smoothVector * thrustForce);

	} // END of FIXED UPDATE


	// TURBO Operations
	public void Turbo_On () {
		thrust = turboThrust;
		thrustForce = thrust;
		CancelInvoke ("Turbo_Off");
		Invoke ("Turbo_Off", turboTime);
	}

	public void Turbo_Off () {
		thrust = normalThrust;
	}

	// Slow down on collision
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
