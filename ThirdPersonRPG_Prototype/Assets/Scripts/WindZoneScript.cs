using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneScript : MonoBehaviour {

    [SerializeField]
    private float windForce = 20;

    private PlayerController playerCtr;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            playerCtr = _col.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider _col) {
        if (_col.tag == "Player") {
            Debug.Log("Push!");
            playerCtr.currentGlidingGraivity = -windForce;
        }
    }
}
