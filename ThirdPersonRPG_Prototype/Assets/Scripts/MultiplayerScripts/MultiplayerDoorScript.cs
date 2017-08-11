using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerDoorScript : NetworkBehaviour {

    [SyncVar]
    [SerializeField]
    private bool DoorGreen = false;

    [SyncVar]
    [SerializeField]
    private bool DoorRed = false;

    private Renderer objectGFX;
   
    [SyncVar]
    private Color32 color;

    private Animator doorAnimator;

    // Use this for initialization
    void Start () {
        objectGFX = this.transform.Find("Door_GFX").GetComponent<Renderer>();
        doorAnimator = this.GetComponent<Animator>();

        if ((DoorGreen == true && DoorRed == true) || (DoorGreen == false && DoorRed == false)) {
            Debug.LogError("Door color is not set correctly! Check one of the boxes mutually-exclusivly");
        }

        if (DoorGreen == true) {
            color = new Color32((byte)10, (byte)219, (byte)37, (byte)255);
        } else if (DoorRed == true) {
            color = new Color32((byte)238, (byte)14, (byte)14, (byte)255);
        }

        objectGFX.material.SetColor("_Color", color);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            if(DoorGreen == true && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "Key_Green") {
                doorAnimator.SetBool("OpenDoor", true);
            }else if (DoorRed == true && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "Key_Red") {
                doorAnimator.SetBool("OpenDoor", true);
            }
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == "Player") {
            doorAnimator.SetBool("OpenDoor", false);
        }
    }
}
