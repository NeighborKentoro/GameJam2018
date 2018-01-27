using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public FrequencyRange frequencyRange;

	public float currentFrequency;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable () {
		EventManager.SendFrequencyEvent += SetFrequency;
	}

	void OnDisable () {
		EventManager.SendFrequencyEvent -= SetFrequency;
	}

	public void Activate (float frequency) {
		
	}

	public void SetFrequency(float frequency) {
		
	}
}
