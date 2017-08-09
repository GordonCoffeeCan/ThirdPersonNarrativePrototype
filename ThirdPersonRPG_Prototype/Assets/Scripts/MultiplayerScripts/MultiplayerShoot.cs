using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerShoot : NetworkBehaviour {
    private const string PLAYER_TAG = "Player";
    private const string OBSTACLE_TAG = "DynamicObstacle";

    public PlayerWeapon weapon;

    public int obstacleNumLimit;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Transform firePoint;
    
    [SerializeField]
    private BulletScript bulletEX;

    [SerializeField]
    private MultiplayerObstacleCrateScript obstacleCrate;

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
            obstacleNumLimit = weapon.obstacleNumberLimit;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //If in Game menu panel is on, player cannot shoot;
        if (UIManager.isMenuPanelOn) {
            return;
        }

        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            weapon.currentWeapon = PlayerWeapon.Weapon.Glock;
        }else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            weapon.currentWeapon = PlayerWeapon.Weapon.ObstacleCrate;
        }

        if (ControllerManager.instacne.OnFire() && isFired == false) {
            switch (weapon.currentWeapon) {
                case PlayerWeapon.Weapon.Glock:
                    shoot();
                    break;
                case PlayerWeapon.Weapon.ObstacleCrate:
                    createObstacle();
                    break;
                default:
                    break;
            }
            isFired = true;
        }else if (ControllerManager.instacne.OnFire() == false) {
            isFired = false;
        }
    }

    //Shoot bullet
    //this method only runing on the client;
    [Client]
    private void shoot() {
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

        CmdOnShoot(cameraPivot.transform.rotation, _hit.distance);
    }

    //Shoot Bullet runing on Server
    [Command]
    private void CmdOnShoot(Quaternion _bulletRotation, float _distance) {
        RpcShootEX(_bulletRotation, _distance);
    }

    ////Shoot Bullet Response effect on clients
    [ClientRpc]
    private void RpcShootEX(Quaternion _bulletRotation, float _distance) {
        if (bulletEX != null) {
            BulletScript _bullet = (BulletScript)Instantiate(bulletEX, firePoint.position, _bulletRotation);
            if (_distance <= 0) {
                _distance = 200;
            }
            _bullet.shootDistance = _distance;
        } else {
            Debug.LogError("No Bullet Effect game object reference!");
        }
    }

    //Create and Destory Obstacle
    [Client]
    private void createObstacle() {
        RaycastHit _hit;

        if (ControllerManager.instacne.OnAim()) {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, weapon.range, layerMask)) {
                if (_hit.collider.tag == OBSTACLE_TAG) {
                    
                } else {
                    CmdCreateObstacle();
                }
            } else {
                CmdCreateObstacle();
            }
        } else {
            CmdCreateObstacle();
        }

    }

    [Command]
    private void CmdCreateObstacle() {
        RpcCreateObstacle();
    }

    [ClientRpc]
    private void RpcCreateObstacle() {
        if(obstacleCrate != null) {
            if(obstacleNumLimit > 0) {
                MultiplayerObstacleCrateScript _obstacle = (MultiplayerObstacleCrateScript)Instantiate(obstacleCrate, firePoint.position, firePoint.rotation);
                obstacleNumLimit--;
                _obstacle.playerName = this.transform.name;
            }
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
