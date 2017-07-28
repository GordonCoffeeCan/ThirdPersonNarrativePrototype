using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerShoot : NetworkBehaviour {
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Transform rotationPivot;

    [SerializeField]
    private LayerMask layerMask;

    private bool isFired = false;

    private Text debugInfo;

    // Use this for initialization
    void Start () {
        if (playerCamera == null) {
            Debug.LogError("No Player Camera referenced!");
            this.enabled = false;
        } else {
            debugInfo = GameObject.Find("DebugInfo").GetComponent<Text>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (ControllerManager.instacne.OnFire() && isFired == false) {
            shoot();
            isFired = true;
        }else if (ControllerManager.instacne.OnFire() == false) {
            isFired = false;
        }

        //Debug.Log(isFired);
	}

    //this method only runing on the client;
    [Client]
    private void shoot() {

        //If in Game menu panel is on, player cannot shoot;
        if (UIManager.isMenuPanelOn) {
            return;
        }

        Vector3 _rayPostion;
        Vector3 _rayDirection;

        if(rotationPivot == null) {
            Debug.LogError("No Roation Pivot referenced!");
            return;
        }

        RaycastHit _hit;

        //adjust ray cast position on Aim or not;
        if (ControllerManager.instacne.OnAim()) {
            _rayPostion = playerCamera.transform.position;
            _rayDirection = playerCamera.transform.forward;
        } else {
            _rayPostion = new Vector3(this.transform.position.x, this.transform.position.y + 1.39f, this.transform.position.z);
            _rayDirection = rotationPivot.transform.forward;
        }
        //End ------ adjust ray cast position on Aim or not;

        if (Physics.Raycast(_rayPostion, _rayDirection, out _hit, weapon.range, layerMask)) {
            if (_hit.collider.tag == PLAYER_TAG) {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
            debugInfo.text = _hit.collider.name + " is Hit!";
            Debug.DrawRay(_rayPostion, _rayDirection, Color.red, 1);
        }
    }

    //this method only runing on the server;
    [Command]
    private void CmdPlayerShot(string _playerName, int _damage) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);
    }
}
