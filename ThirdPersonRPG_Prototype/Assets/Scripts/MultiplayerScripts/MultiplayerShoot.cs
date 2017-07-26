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
        if (ControllerManager.instacne.OnFire()) {
            shoot();
        }
	}

    //this method only called on the client;
    [Client]
    private void shoot() {

        Vector3 _rayPostion;
        Vector3 _rayDirection;

        if(rotationPivot == null) {
            Debug.LogError("No Roation Pivot referenced!");
            return;
        }

        RaycastHit _hit;

        if (ControllerManager.instacne.OnAim()) {
            _rayPostion = playerCamera.transform.position;
            _rayDirection = playerCamera.transform.forward;
        } else {
            _rayPostion = new Vector3(this.transform.position.x, this.transform.position.y + 1.39f, this.transform.position.z);
            _rayDirection = rotationPivot.transform.forward;
        }

        if (Physics.Raycast(_rayPostion, _rayDirection, out _hit, weapon.range, layerMask)) {
            if (_hit.collider.tag == PLAYER_TAG) {
                CmdPlayerShot(_hit.collider.name);
            }
            debugInfo.text = _hit.collider.name + " is Hit!";
            Debug.DrawRay(_rayPostion, _rayDirection, Color.red, 1);
        }
    }

    //this method only called on the server;
    [Command]
    private void CmdPlayerShot(string _playerName) {
        Debug.Log(_playerName + " has been shot!");
    }
}
