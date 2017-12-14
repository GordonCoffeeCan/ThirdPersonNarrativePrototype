using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayAudio : MonoBehaviour {

    private bool hasTriggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (hasTriggered == false) {
            if (_col.tag == "Player") {

                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                hasTriggered = true;
            }
        }
    }
}
