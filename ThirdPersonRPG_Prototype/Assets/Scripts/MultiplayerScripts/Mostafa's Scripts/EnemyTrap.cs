using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour {
    private Animator doorAnimator;

    // Use this for initialization
    void Start () {
        doorAnimator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Enemy") {

            //_col.GetComponent<MultiplayerEnemyAI>().enabled = false;
            
           
                doorAnimator.SetBool("OpenDoor", true);
            
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == "Enemy ") {
            doorAnimator.SetBool("OpenDoor", false);
        }
    }


}
