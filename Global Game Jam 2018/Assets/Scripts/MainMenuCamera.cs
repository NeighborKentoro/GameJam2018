using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x + rotationSpeed * Time.deltaTime, -1 * transform.rotation.eulerAngles.x + rotationSpeed * Time.deltaTime, 0));
	}
}
