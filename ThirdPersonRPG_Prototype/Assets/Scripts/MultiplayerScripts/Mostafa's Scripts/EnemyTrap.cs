using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


        if (_col.isTrigger) { }
        else {
            if (_col.tag == "EnemyBody") {

              
                _col.GetComponentInParent<MultiplayerEnemyAI>().enabled = false;
                _col.GetComponentInParent<NavMeshAgent>().enabled = false;
                _col.GetComponentInParent<Rigidbody>().isKinematic = false;



                doorAnimator.SetBool("OpenDoor", true);

            }
        }
    }

    private void OnTriggerExit(Collider _col) {

        if (_col.isTrigger) { }
        else {
            if (_col.tag == "EnemyBody") {
                doorAnimator.SetBool("OpenDoor", false);
            }
        }
    }


}
