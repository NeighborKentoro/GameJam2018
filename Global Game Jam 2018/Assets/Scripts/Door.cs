using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public FrequencyRange frequencyRange;

	public float currentFrequency;

	bool isActivated;

	// Use this for initialization
	void Start () {
		
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
		
	}

	public void Deactivate () {
		
	}

	public void SetFrequency(float frequency) {
		currentFrequency = frequency;
	}
}
