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
        float freqMod = Input.GetAxisRaw("Frequency") * .25f;

        #region PlayerMovement
        if(controlsEnabled) {
			if(yAxis > 0) {
				if(xAxis > 0.5f) {
					xSpeed = maxSpeed;
					transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
				} else if(xAxis < -0.5f) {
					zSpeed = maxSpeed;
					transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
				} else {
					xSpeed = maxSpeed/1.5f;
					zSpeed = maxSpeed/1.5f;
					transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
				}
			} else if(yAxis < -0) {
				if(xAxis > 0.5f) {
					zSpeed = -maxSpeed;
					transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
				} else if(xAxis < -0.5f) {
					xSpeed = -maxSpeed;
					transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
				} else {
					xSpeed = -maxSpeed/1.5f;
					zSpeed = -maxSpeed/1.5f;
					transform.rotation = Quaternion.Euler(new Vector3(0, 225, 0));
				}
			} else if(Mathf.Approximately(yAxis, 0)) {
				if(xAxis > 0.5f) {
					xSpeed = maxSpeed/1.5f;
					zSpeed = -maxSpeed/1.5f;
					transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
				} else if(xAxis < -0.5f) {
					xSpeed = -maxSpeed/1.5f;
					zSpeed = maxSpeed/1.5f;
					transform.rotation = Quaternion.Euler(new Vector3(0, 315, 0));
				}
			}

            if(Input.GetButtonDown("UpFrequency") && currentFrequency < freqRange.max) {
                currentFrequency += 1;
                EventManager.SendFrequency(currentFrequency);
            } else if(Input.GetButtonDown("DownFrequency") && currentFrequency > freqRange.min) {
                currentFrequency -= 1;
                EventManager.SendFrequency(currentFrequency);
            } else {
                float newFrequency = currentFrequency + freqMod;
                if(newFrequency >= freqRange.min && newFrequency <= freqRange.max) {
                    currentFrequency = newFrequency;
                    EventManager.SendFrequency(currentFrequency);
                }
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

	public void KillPlayer () {
		transform.position = startPoints[currentLevel - 1].position;
	}
}
