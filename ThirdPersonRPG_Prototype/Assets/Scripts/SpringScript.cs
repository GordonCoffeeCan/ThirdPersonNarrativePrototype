using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour {

    private const string PLAYER_TAG = "Player";
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if(_col.tag == PLAYER_TAG) {
            playerController = _col.GetComponent<PlayerController>();

            playerController.upSpeed = 0;
        }
    }
}
