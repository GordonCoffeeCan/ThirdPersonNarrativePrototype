using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KeyScript : NetworkBehaviour {

    [SyncVar]
    [SerializeField]
    private bool keyGreen = false;

    [SyncVar]
    [SerializeField]
    private bool keyRed = false;

    [SyncVar]
    private Color32 color;

    private Renderer objectGFX;

    private float roationSpeed = 250;

	// Use this for initialization
	void Start () {
        objectGFX = this.transform.Find("Key_GFX").GetComponent<Renderer>();
        

        if((keyGreen == true && keyRed == true) || (keyGreen == false && keyRed == false)) {
            Debug.LogError("Key color is not set correctly! Check one of the boxes mutually-exclusivly");
        }

        if (keyGreen == true) {
            color = new Color32((byte)10, (byte)219, (byte)37, (byte)255);
        } else if (keyRed == true) {
            color = new Color32((byte)238, (byte)14, (byte)14, (byte)255);
        }

        objectGFX.material.SetColor("_Color", color);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, roationSpeed * Time.deltaTime, 0);
    }

    public override void OnStartServer() {
        base.OnStartServer();
    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player" && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "") {
            _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName = this.gameObject.name;
            RpcOnPickUp();
        }
    }

    [ClientRpc]
    private void RpcOnPickUp() {
        this.gameObject.SetActive(false);
    }
}
