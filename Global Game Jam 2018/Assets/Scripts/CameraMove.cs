using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public Transform[] levels;

	private Vector3 velocity = Vector3.zero;

	bool moving = false;

	private int nextLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(nextLevel < levels.Length) {
			if(moving) {
				transform.position = Vector3.SmoothDamp(transform.position, levels[nextLevel].position, ref velocity, 0.6f);
				if(Mathf.Approximately(transform.position.y, levels[nextLevel].position.y)) {
					moving = false;
					nextLevel += 1;
				}
			}
		}
	}

	void OnEnable () {
		EventManager.ExitLevelEvent += ExitLevel;
	}

	void OnDisable () {
		EventManager.ExitLevelEvent -= ExitLevel;
	}

	public void ExitLevel () {
		moving = true;
	}
}
