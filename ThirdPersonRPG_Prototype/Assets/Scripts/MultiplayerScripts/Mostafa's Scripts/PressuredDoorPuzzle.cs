using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PressuredDoorPuzzle : NetworkBehaviour {
    private enum TypeOfDoor {
        singleSwitch,
        doubleSwitch

    }

    [SerializeField]
    private TypeOfDoor puzzleDoor;

    public GameObject door;
    public GameObject presssurePlate1;
    public GameObject pressurePlate2;

    


	// Use this for initialization
	void Start () {
        

		
	}
	
	// Update is called once per frame
	void Update () {

        switch (puzzleDoor) {
            case TypeOfDoor.singleSwitch:


                if (presssurePlate1.GetComponent<platformScript>().isTriggered == true) {

                    door.GetComponent<Animator>().SetBool("OpenDoor", true);
                }
                else
                if (pressurePlate2.GetComponent<platformScript>().isTriggered == true) {

                    door.GetComponent<Animator>().SetBool("OpenDoor", true);
                }
                else {
                    door.GetComponent<Animator>().SetBool("OpenDoor", false);
                }
                break;
            case TypeOfDoor.doubleSwitch:

                if (presssurePlate1.GetComponent<platformScript>().isTriggered == true && pressurePlate2.GetComponent<platformScript>().isTriggered == true) {

                    door.GetComponent<Animator>().SetBool("OpenDoor", true);
                }
                else {
                    door.GetComponent<Animator>().SetBool("OpenDoor", false);
                }

                break;



        }

		
	}
}
