using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneScript : MonoBehaviour {

    [SerializeField]
    private float windForce = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider _col) {
        if (_col.tag == "Player") {
            Debug.Log("Push!");
            _col.GetComponent<PlayerController>().currentGlidingGraivity = -windForce;
        }
    }
}
