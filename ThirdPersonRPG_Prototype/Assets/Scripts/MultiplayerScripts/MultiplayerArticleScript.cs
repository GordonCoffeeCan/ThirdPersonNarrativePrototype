using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerArticleScript : NetworkBehaviour {

    private enum Article {
        keyGreen,
        keyRed,
        freezGun,
        obstacleMagic
    }

    [SerializeField]
    [SyncVar]
    private Article chosenArticle;

    [SyncVar]
    private Color32 color;

    private Renderer objectGFX;

    private float roationSpeed = 250;

	// Use this for initialization
	void Start () {

        switch (chosenArticle) {
            case Article.keyGreen:
                objectGFX = this.transform.Find("Key_GFX").GetComponent<Renderer>();

                color = new Color32((byte)10, (byte)219, (byte)37, (byte)255);

                objectGFX.material.SetColor("_Color", color);
                break;
            case Article.keyRed:
                objectGFX = this.transform.Find("Key_GFX").GetComponent<Renderer>();

                color = new Color32((byte)238, (byte)14, (byte)14, (byte)255);

                objectGFX.material.SetColor("_Color", color);
                break;
            case Article.freezGun:
                break;
            case Article.obstacleMagic:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, roationSpeed * Time.deltaTime, 0);
    }

    public override void OnStartServer() {
        base.OnStartServer();
    }

    private void OnTriggerEnter(Collider _col) {

        switch (chosenArticle) {
            case Article.keyGreen:
                if (_col.tag == "Player" && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "") {
                    _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName = this.gameObject.name;
                    CmdOnPickUp();
                }
                break;
            case Article.keyRed:
                if (_col.tag == "Player" && _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName == "") {
                    _col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName = this.gameObject.name;
                    CmdOnPickUp();
                }
                break;
            case Article.freezGun:
                if (_col.tag == "Player" && _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon == PlayerWeapon.Weapon.none) {
                    _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon = PlayerWeapon.Weapon.freezer;
                    CmdOnPickUp();
                }
                break;
            case Article.obstacleMagic:
                if (_col.tag == "Player" && _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon == PlayerWeapon.Weapon.none) {
                    _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon = PlayerWeapon.Weapon.obstacleCreator;
                    CmdOnPickUp();
                }
                break;
        }
    }

    [Command]
    private void CmdOnPickUp() {
        RpcOnPickUp();
    }

    [ClientRpc]
    private void RpcOnPickUp() {
        this.gameObject.SetActive(false);
    }
}
