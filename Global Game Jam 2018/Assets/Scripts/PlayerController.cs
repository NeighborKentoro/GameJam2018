using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rbody;

	public float maxSpeed;

	public FrequencyRange freqRange;

	public float currentFrequency = 1;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float xSpeed = 0;
		float zSpeed = 0;
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");

		#region PlayerMovement
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

		rbody.velocity = new Vector3(xSpeed, rbody.velocity.y, zSpeed);
		#endregion

		#region FrequencyControls
		if(Input.GetButtonDown("UpFrequency") && currentFrequency < freqRange.max) {
			currentFrequency += 1;
			EventManager.SendFrequency(currentFrequency);
		} else if(Input.GetButtonDown("DownFrequency") && currentFrequency > freqRange.min) {
			currentFrequency -= 1;
			EventManager.SendFrequency(currentFrequency);
		}
		#endregion
	}
}
