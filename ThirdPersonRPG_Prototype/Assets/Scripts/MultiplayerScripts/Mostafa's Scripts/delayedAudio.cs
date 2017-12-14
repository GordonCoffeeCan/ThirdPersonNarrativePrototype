using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayedAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayDelayed(2);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
