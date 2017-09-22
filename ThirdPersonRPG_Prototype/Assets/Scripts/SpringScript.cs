using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour {

    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private float speedToPlayer = 25;

    [SerializeField]
    private Animator springAnimController;
    private Collider triggerCollider;
    private bool isSpringReleased = false;
    
    private PlayerController playerCtr;

	// Use this for initialization
	void Start () {
        if(springAnimController == null) {
            Debug.LogError("No Spring Animation Controller reference!");
        }

        triggerCollider = this.GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if(_col.tag == PLAYER_TAG) {
            playerCtr = _col.GetComponent<PlayerController>();

            playerCtr.popSpeed = speedToPlayer;
            playerCtr.isPopped = true;
            springAnimController.SetTrigger("ReleaseSpring");
            triggerCollider.enabled = false;
        }
    }
}
