using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public FrequencyRange frequencyRange;

	public float currentFrequency;

	bool isActivated;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentFrequency > frequencyRange.min && currentFrequency < frequencyRange.max && !isActivated) {
			isActivated = true;
			Activate();
		} else if( (currentFrequency < frequencyRange.min || currentFrequency > frequencyRange.max) && isActivated) {
			isActivated = false;
			Deactivate();
		}
	}

	void OnEnable () {
		EventManager.SendFrequencyEvent += SetFrequency;
	}

	void OnDisable () {
		EventManager.SendFrequencyEvent -= SetFrequency;
	}

	public void Activate () {
		transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
		audio.Play();
	}

	public void Deactivate () {
		transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
		audio.Play();
	}

	public void SetFrequency(float frequency) {
		currentFrequency = frequency;
	}
}
