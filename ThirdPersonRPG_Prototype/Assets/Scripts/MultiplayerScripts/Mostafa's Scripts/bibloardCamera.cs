using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bibloardCamera : MonoBehaviour {
    private Camera playerCam;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        /*Transform target = Camera.main.transform;
        if (target != null)
            transform.rotation = target.rotation;*/

        // transform.LookAt(Camera.main.transform);

        if (playerCam == null) {
            playerCam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        }
        transform.LookAt(playerCam.transform);

    }
}
