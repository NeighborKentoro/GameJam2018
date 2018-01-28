﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction {NW, NE, SW, SE};

public class Laser : MonoBehaviour {

	private LineRenderer line;
	public FrequencyRange frequencyRange;
	public float currentFrequency;
	bool isActivated;
	public LayerMask layerMask;
	private Vector3 forward;
	public Direction dir;

    public RectTransform labelTransform;
    public Text labelText;

    // Use this for initialization
    void Start () {
		line = GetComponent<LineRenderer>();
		if(dir == Direction.NE) {
			forward = new Vector3(1, 0, 0);
		} else if(dir == Direction.NW) {
			forward = new Vector3(0, 0, 1);
		} else if(dir == Direction.SE) {
			forward = new Vector3(0, 0, -1);
		} else {
			forward = new Vector3(-1, 0, 0);
		}
	}

	// Update is called once per frame
	void Update () {
		if(currentFrequency >= frequencyRange.min && currentFrequency <= frequencyRange.max && !isActivated) {
			isActivated = true;
			Activate();
		} else if( (currentFrequency < frequencyRange.min || currentFrequency > frequencyRange.max) && isActivated) {
			isActivated = false;
			Deactivate();
		}
		if(isActivated){
			CalculateLaserLength();
		}
	}

	void OnEnable () {
		EventManager.SendFrequencyEvent += SetFrequency;

        //set the label to show max and min frequency range
        labelText.text = string.Format("{0}-{1}", frequencyRange.min, frequencyRange.max);

        //rotate the door's frequency range label (hopefully after parent gameobject calls Snap() )
        float facingCameraY = 45 - transform.parent.rotation.eulerAngles.y;
        labelTransform.localRotation = Quaternion.Euler(labelTransform.rotation.eulerAngles.x, facingCameraY, labelTransform.rotation.eulerAngles.z);

    }

    void OnDisable () {
		EventManager.SendFrequencyEvent -= SetFrequency;
	}

	public void Activate () {
		CalculateLaserLength();
		line.enabled = true;

	}

	public void Deactivate () {
		line.enabled = false;
	}

	public void SetFrequency(float frequency) {
		currentFrequency = frequency;
	}

	void CalculateLaserLength() {
		RaycastHit hit;
		Vector3 localOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		Physics.Raycast(localOffset, forward, out hit, 50, layerMask);
		if(hit.collider.CompareTag("Player")) {
			hit.collider.GetComponent<PlayerController>().KillPlayer();
		}
		line.SetPosition(0, transform.position);
		line.SetPosition(1, hit.point);
	}
}