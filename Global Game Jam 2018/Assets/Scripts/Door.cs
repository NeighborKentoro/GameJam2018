using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public FrequencyRange frequencyRange;

	public float currentFrequency;

	bool isActivated;

	public bool isSlidingDoor;

    public RectTransform labelTransform;
    public Text labelText;

    public Material slidingMaterial;

	AudioSource doorSound;

	// Use this for initialization
	void Start () {
		doorSound = GetComponent<AudioSource>();

        //override for object material
        if (isSlidingDoor) {
            MeshRenderer thisRenderer = GetComponent<MeshRenderer>();
            thisRenderer.material = slidingMaterial;
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
	}

    void OnEnable() {
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
		if(isSlidingDoor) {
			transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
		} else {
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
		}
		doorSound.Play();
	}

	public void Deactivate () {
		if(isSlidingDoor) {
			transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
		} else {
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z);
		}
		doorSound.Play();
	}

	public void SetFrequency(float frequency) {
		currentFrequency = frequency;
	}
}
