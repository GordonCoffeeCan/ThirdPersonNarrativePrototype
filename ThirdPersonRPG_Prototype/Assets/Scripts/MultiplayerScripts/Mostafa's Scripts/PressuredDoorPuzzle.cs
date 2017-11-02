using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressuredDoorPuzzle : MonoBehaviour {
    public GameObject door;
    public GameObject presssurePlate1;
    public GameObject pressurePlate2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (presssurePlate1.GetComponent<platformScript>().isTriggered == true) {

            door.GetComponent<Animator>().SetBool("OpenDoor", true);
        }
        else {
            door.GetComponent<Animator>().SetBool("OpenDoor", false);
        }
		
	}
}
