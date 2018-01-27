using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTile : MonoBehaviour {

	public bool snapToCenter;

	public bool isCorner;

	// Use this for initialization
	void Start () {
		Snap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Snap() {
		float roundedZ, roundedX, offsetZ, offsetX, finalZ, finalX;
		float halfZScale = transform.localScale.z/2;
		float height = 0 + (transform.localScale.y/2);
		roundedZ = Mathf.Round(transform.position.z);
		roundedX = Mathf.Round(transform.position.x);
		offsetZ = roundedZ - transform.position.z;
		offsetX = roundedX - transform.position.x;

		if(snapToCenter) {
			if(offsetZ > 0) {
				finalZ = roundedZ - 0.5f;
			} else {
				finalZ = roundedZ + 0.5f;
			}

			if(offsetX > 0) {
				finalX = roundedX - 0.5f;
			} else {
				finalX = roundedX + 0.5f;
			}
			transform.position = new Vector3(finalX, height, finalZ);
		} else if(isCorner) {
			if(offsetZ > 0) {
				finalZ = roundedZ - halfZScale;
			} else {
				finalZ = roundedZ + halfZScale;
			}

			if(offsetX > 0) {
				finalX = roundedX - halfZScale;
			} else {
				finalX = roundedX + halfZScale;
			}
			transform.position = new Vector3(finalX, height, finalZ);
		} else {
			float yRot = (Mathf.Floor(transform.rotation.eulerAngles.y/90) * 90) + ((transform.rotation.eulerAngles.y % 90) < 45 ? 0 : 90);
			if(yRot > 0) {
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRot, transform.rotation.eulerAngles.z);
				if(offsetZ > 0) {
					finalZ = roundedZ - halfZScale;
				} else {
					finalZ = roundedZ + halfZScale;
				}
				finalX = roundedX;
			} else {
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRot, transform.rotation.eulerAngles.z);
				if(offsetX > 0) {
					finalX = roundedX - halfZScale;
				} else {
					finalX = roundedX + halfZScale;
				}
				finalZ = roundedZ;
			}
			transform.position = new Vector3(finalX, height, finalZ);
		}

	}
}
