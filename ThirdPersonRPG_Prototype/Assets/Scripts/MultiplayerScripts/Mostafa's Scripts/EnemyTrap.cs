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
        if (_col.tag == "Player") {
           
                doorAnimator.SetBool("OpenDoor", true);
            
        }
    }


}
