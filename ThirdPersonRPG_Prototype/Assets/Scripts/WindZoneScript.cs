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
            if (_col.GetComponent<PlayerController>().isGlide == true) {
                _col.GetComponent<PlayerController>().popSpeed = 40;
                _col.GetComponent<PlayerController>().isWindPushed = true;
               
            }
        }
    }

    private void OnTriggerStay(Collider _col) {
        if (_col.tag == "Player") {
            Debug.Log("Push!");
            if (_col.GetComponent<PlayerController>().isGlide == true) {
                 _col.GetComponent<PlayerController>().popSpeed = 40;
                _col.GetComponent<PlayerController>().isWindPushed = true;

                _col.GetComponent<PlayerController>().isGlide = true;

            }
        }
    }
}
