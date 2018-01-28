using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyLabel : MonoBehaviour {

    void OnEnable() {
        //rotate the door's frequency range label (hopefully after parent gameobject calls Snap() )
        float facingCameraY = 45 - transform.parent.rotation.eulerAngles.y;
        Debug.Log("Parent: " + transform.parent.name);
        Debug.Log("Parent y: " + transform.parent.rotation.eulerAngles.y);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, facingCameraY, transform.rotation.eulerAngles.z);
    }
}
