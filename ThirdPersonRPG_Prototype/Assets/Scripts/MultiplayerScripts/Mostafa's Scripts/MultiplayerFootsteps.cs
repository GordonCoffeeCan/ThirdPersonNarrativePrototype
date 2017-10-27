using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerFootsteps : MonoBehaviour {

    CharacterController cc;

    private AudioSource sound;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();

        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cc.isGrounded==true && cc.velocity.magnitude > 2f && sound.isPlaying == false){

            sound.volume = Random.Range(0.8f, 1);
            sound.pitch = Random.Range(0.8f, 1.1f);

            sound.Play();
        }

	}
}
