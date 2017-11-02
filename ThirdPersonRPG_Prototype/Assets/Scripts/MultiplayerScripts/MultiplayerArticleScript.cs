using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerArticleScript : NetworkBehaviour {

    private enum Article {
        keyGreen,
        keyRed,
        freezGun,
        obstacleMagic,
        windZone,
        coin
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
            case Article.windZone:
                objectGFX = this.transform.Find("Key_GFX").GetComponent<Renderer>();

                color = new Color32((byte)66, (byte)206, (byte)244, (byte)255);

                objectGFX.material.SetColor("_Color", color);
                break;
            case Article.freezGun:
                break;
            case Article.obstacleMagic:
                break;
            case Article.coin:
                objectGFX = this.transform.Find("Key_GFX").GetComponent<Renderer>();

                color = new Color32((byte)255, (byte)232, (byte)0, (byte)255);

                objectGFX.material.SetColor("_Color", color);
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
                    _col.GetComponent<MultiplayerShoot>().InitializeWeapon();
                    CmdOnPickUp();
                }
                break;
            case Article.obstacleMagic:
                if (_col.tag == "Player" && _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon == PlayerWeapon.Weapon.none) {
                    _col.GetComponent<MultiplayerShoot>().weapon.currentWeapon = PlayerWeapon.Weapon.obstacleCreator;
                    _col.GetComponent<MultiplayerShoot>().InitializeWeapon();
                    CmdOnPickUp();
                }
                break;
            case Article.coin:
                if (_col.tag == "Player") {
                    //_col.GetComponent<MultiplayerPlayerManager>().pickedUpKeyName = this.gameObject.name;
                    GameObject scoreManager = GameObject.Find("ScoreManager");

                    //scoreManager.GetComponent<ScoreManager>().individualScore = scoreManager.GetComponent<ScoreManager>().individualScore + 10;

                    MultiplayerGameManager.instance.playerScore = MultiplayerGameManager.instance.playerScore + 10;

                    //_col.GetComponent<MultiplayerPlayerManager>().individualScore = _col.GetComponent<MultiplayerPlayerManager>().individualScore + 10;


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

        AudioClip coinPickupSFX = this.gameObject.GetComponent<AudioSource>().clip;


        //this.gameObject.GetComponent<AudioSource>().PlayOneShot(coinPickupSFX);

        AudioSource.PlayClipAtPoint(coinPickupSFX, this.transform.position);


        this.gameObject.SetActive(false);
        
    }
}
