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
    private Transform firePoint;
    
    [SerializeField]
    private BulletScript bulletEX;

    [SerializeField]
    private ObstacleCrateScript obstacleCrate;

    [SerializeField]
    private LayerMask layerMask;

    private Transform cameraPivot;
    private Transform rotationPivot;

    private bool isFired = false;

    // Use this for initialization
    void Start () {
        if (playerCamera == null) {
            Debug.LogError("No Player Camera referenced!");
            this.enabled = false;
        } else {
            rotationPivot = this.GetComponent<PlayerController>().rotationPivot;
            cameraPivot = this.GetComponent<PlayerController>().cameraPivot.transform;
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

        if (rotationPivot == null) {
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
            Debug.DrawRay(_rayPostion, _rayDirection, Color.red, 1);
        }

        if (isLocalPlayer) {
            CmdOnShoot(cameraPivot.transform.rotation, _hit.distance);
        } else {
            CmdOnShoot(Quaternion.Euler(cameraPivot.transform.localRotation.eulerAngles + rotationPivot.transform.rotation.eulerAngles), _hit.distance);
        }
        
    }

    [Command]
    private void CmdOnShoot(Quaternion _bulletRotation, float _distance) {
        RpcShootEX(_bulletRotation, _distance);
    }

    [ClientRpc]
    private void RpcShootEX(Quaternion _bulletRotation, float _distance) {
        /*if (bulletEX != null) {
            //Instantiate(bulletEX, firePoint.position, Quaternion.Euler(new Vector3(0, rotationPivot.transform.localEulerAngles.y + this.transform.eulerAngles.y, 0)));
            BulletScript _bullet = (BulletScript)Instantiate(bulletEX, firePoint.position, _bulletRotation);
            if (_distance <= 0) {
                _distance = 200;
            }
            _bullet.shootDistance = _distance;
        } else {
            Debug.LogError("No Bullet Effect game object reference!");
        }*/

        if(obstacleCrate != null) {
            ObstacleCrateScript _obstacle = (ObstacleCrateScript)Instantiate(obstacleCrate, firePoint.position, _bulletRotation);
        } else {
            Debug.LogError("No Obstacle Crate game object reference!");
        }
    }

    //this method only runing on the server;
    [Command]
    private void CmdPlayerShot(string _playerName, int _damage) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);
    }
}
