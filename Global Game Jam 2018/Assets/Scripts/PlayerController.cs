using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rbody;

	public float maxSpeed;

	public FrequencyRange freqRange;

	public float currentFrequency = 1;

	public Transform[] startPoints;

	private int currentLevel = 0;

	AudioSource shuffleSound;

	private bool controlsEnabled = true;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
		shuffleSound = GetComponent<AudioSource>();

		transform.position = startPoints[currentLevel].transform.position;
		currentLevel += 1;
	}
	
	// Update is called once per frame
	void Update () {
		float xSpeed = 0;
		float zSpeed = 0;
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");

		#region PlayerMovement
		if(controlsEnabled) {
			if(yAxis > 0) {
				if(xAxis > 0.5f) {
					xSpeed = maxSpeed;
				} else if(xAxis < -0.5f) {
					zSpeed = maxSpeed;
				} else {
					xSpeed = maxSpeed/1.5f;
					zSpeed = maxSpeed/1.5f;
				}
			} else if(yAxis < -0) {
				if(xAxis > 0.5f) {
					zSpeed = -maxSpeed;
				} else if(xAxis < -0.5f) {
					xSpeed = -maxSpeed;
				} else {
					xSpeed = -maxSpeed/1.5f;
					zSpeed = -maxSpeed/1.5f;
				}
			} else if(Mathf.Approximately(yAxis, 0)) {
				if(xAxis > 0.5f) {
					xSpeed = maxSpeed/1.5f;
					zSpeed = -maxSpeed/1.5f;
				} else if(xAxis < -0.5f) {
					xSpeed = -maxSpeed/1.5f;
					zSpeed = maxSpeed/1.5f;
				}
			}

			if(Input.GetButtonDown("UpFrequency") && currentFrequency < freqRange.max) {
				currentFrequency += 1;
				EventManager.SendFrequency(currentFrequency);
			} else if(Input.GetButtonDown("DownFrequency") && currentFrequency > freqRange.min) {
				currentFrequency -= 1;
				EventManager.SendFrequency(currentFrequency);
			}
		}

		if( (xSpeed != 0 || zSpeed != 0) && !shuffleSound.isPlaying) {
			shuffleSound.Play();
		} else if( Mathf.Approximately(xSpeed, 0) && Mathf.Approximately(zSpeed, 0) ) {
			shuffleSound.Stop();
		}

		rbody.velocity = new Vector3(xSpeed, rbody.velocity.y, zSpeed);
		#endregion

	}

	void OnEnable () {
		EventManager.ExitLevelEvent += ExitLevel;
	}

	void OnDisable () {
		EventManager.ExitLevelEvent -= ExitLevel;
	}

	public void ExitLevel () {
		if(currentLevel < startPoints.Length) {
			transform.position = startPoints[currentLevel].transform.position;
			currentLevel += 1;
			currentFrequency = 1;
			EventManager.SendFrequency(1);
			controlsEnabled = false;
			StartCoroutine(DisableControls());
		}
	}

	IEnumerator DisableControls() {
		yield return new WaitForSeconds(2.5f);
		controlsEnabled = true;
	}
}
