using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimate : MonoBehaviour {

	private Renderer rend;

	public float scrollSpeed;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		float newOffsetX = Time.time * scrollSpeed;

		//if(newOffsetX >= 1) {
		//	mat.mainTextureOffset.Set(0, 0);
		//} else {
		rend.material.mainTextureOffset = new Vector2(newOffsetX, 0);
		//}
	}
}
