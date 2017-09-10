using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerDoorScript : NetworkBehaviour {

    private enum Door {
        green,
        red
    }

    [SyncVar]
    [SerializeField]
    private Door door;

    private Renderer objectGFX;
   
    [SyncVar]
    private Color32 color;

    private Animator doorAnimator;

    // Use this for initialization
    void Start () {
        objectGFX = this.transform.Find("Door_GFX").GetComponent<Renderer>();
        doorAnimator = this.GetComponent<Animator>();

        if (door == Door.green) {
            color = new Color32((byte)10, (byte)219, (byte)37, (byte)255);
        } else if (door == Door.red) {
            color = new Color32((byte)238, (byte)14, (byte)14, (byte)255);
        }

        objectGFX.material.SetColor("_Color", color);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            if(door == Door.green && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "Key_Green") {
                doorAnimator.SetBool("OpenDoor", true);
            }else if (door == Door.red && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "Key_Red") {
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
