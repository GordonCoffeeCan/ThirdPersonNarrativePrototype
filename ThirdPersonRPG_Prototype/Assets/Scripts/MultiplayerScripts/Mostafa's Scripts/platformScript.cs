using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class platformScript : MonoBehaviour {

    
    public bool isTriggered = false;

    private Animator platformAnimator;

	// Use this for initialization
	void Start () {
        platformAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            Debug.Log("PlatformPressed");
            isTriggered = true;
            platformAnimator.SetBool("isPressed", true);

        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == "Player") {
            Debug.Log("PlatformReleased");
            isTriggered = false;
            platformAnimator.SetBool("isPressed", false);

        }
    }

}
