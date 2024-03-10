using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RightTurn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public static RightTurn instance = null;

	private bool _on = false;
	public bool On {
		get {return _on;}
	}

	private float _volume = 0f;
	public float Volume {
		get {return _volume;}
	}

	private float smooth = 3f;

	void Awake () {

		if (instance != null) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}

	}

	void Update () {
		if (_on) {
			if (_volume < 1f) {
				_volume += smooth * Time.deltaTime;
			} 
			if (_volume > 1f) {
				_volume = 1f;
			}
		} else {
			if (_volume > 0f) {
				_volume -= smooth * Time.deltaTime;
			} 
			if (_volume < 0f) {
				_volume = 0f;
			}
		}
	}

	public void OnPointerDown (PointerEventData data) {
		_on = true;
	}

	public void OnPointerUp (PointerEventData data) {
		_on = false;
	}
		
	void OnApplicationFocus(bool focusStatus) {
		if (!focusStatus) {
			_on = false;
			_volume = 0f;
		}
	}

}