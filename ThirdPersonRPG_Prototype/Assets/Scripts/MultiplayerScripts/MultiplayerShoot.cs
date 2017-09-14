using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerShoot : NetworkBehaviour {
    private const string PLAYER_TAG = "Player";
    private const string OBSTACLE_TAG = "DynamicObstacle";
    private const string ENEMY_TAG = "Enemy"; //Mostafa- Enemy Tag

    public PlayerWeapon weapon;

    public int obstacleNumLimit;
    public int obstacleRange=2; //Mostafa- int to measure how far away from player the obstacle spawns
    public bool weaponInitialized = false;

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
    private Vector3 currentFirePointPosition;
    private float coolDownTime;
    private float coolDownTimeLimit;
    private int obstacleID;
    private bool readyToFire;
    private bool isFired = false;

    // Use this for initialization
    void Start()
    {
        if (playerCamera == null){
            Debug.LogError("No Player Camera referenced!");
            this.enabled = false;
        }
        else{
            rotationPivot = this.GetComponent<PlayerController>().rotationPivot;
            cameraPivot = this.GetComponent<PlayerController>().cameraPivot.transform;
            obstacleNumLimit = weapon.obstacleNumberLimit;

            currentFirePointPosition = firePoint.localPosition;

            coolDownTime = 0;
            coolDownTimeLimit = 1;
        }
    }

    public override void OnStartClient(){
        base.OnStartClient();
    }

    // Update is called once per frame
    void Update(){
        //If in Game menu panel is on, player cannot shoot;
        if (MultiplayerUIManager.isMenuPanelOn){
            return;
        }

        if (!isLocalPlayer){
            return;
        }

        if (weaponInitialized == true) {
            coolDownTime += Time.deltaTime;

            if (coolDownTime >= coolDownTimeLimit) {
                coolDownTime = coolDownTimeLimit;
                readyToFire = true;
            } else {
                readyToFire = false;
            }
        }

        SetCoolDownLevel();

        if (MobileInputManager.instance.isGamepadConnected == false) {
            if (MobileInputManager.instance.OnFire() && isFired == false) {
                OnWeaponSelect();
                isFired = true;
            } else if (MobileInputManager.instance.OnFire() == false) {
                isFired = false;
            }
        } else {
            if (ControllerManager.instance.OnFire() && isFired == false) {
                OnWeaponSelect();
                isFired = true;
            } else if (ControllerManager.instance.OnFire() == false) {
                isFired = false;
            }
        }
    }

    private void OnWeaponSelect() {
        switch (weapon.currentWeapon) {
            case PlayerWeapon.Weapon.freezer:
                if (readyToFire == true) {
                    shoot();
                    coolDownTime = 0;
                }
                break;
            case PlayerWeapon.Weapon.obstacleCreator:
                OnObstacleAction();
                break;
            default:
                break;
        }
    }

    //Initialize weapon ---------------
    public void InitializeWeapon() {
        if (weapon.currentWeapon == PlayerWeapon.Weapon.freezer) {
            firePoint.localPosition = currentFirePointPosition;
        } else if (weapon.currentWeapon == PlayerWeapon.Weapon.obstacleCreator) {
            firePoint.localPosition = new Vector3(currentFirePointPosition.x, 0.35f, currentFirePointPosition.z + 1);
        }

        coolDownTime = weapon.SetupCoolDownTime(weapon.currentWeapon);
        coolDownTimeLimit = coolDownTime;

        weaponInitialized = true;
    }
    //Initialize weapon --------------- End

    //Shoot bullet
    //this method only runing on the client;
    [Client]
    private void shoot()
    {
        Vector3 _rayPostion;
        Vector3 _rayDirection;

        if (rotationPivot == null){
            Debug.LogError("No Roation Pivot referenced!");
            return;
        }

        RaycastHit _hit;

        //adjust ray cast position on Aim or not;
        if (MobileInputManager.instance.isGamepadConnected == false) {
            if (MobileInputManager.instance.isAim == true) {
                _rayPostion = playerCamera.transform.position;
                _rayDirection = playerCamera.transform.forward;
            } else {
                _rayPostion = new Vector3(this.transform.position.x, this.transform.position.y + 1.39f, this.transform.position.z);
                _rayDirection = rotationPivot.transform.forward;
            }
        } else {
            if (ControllerManager.instance.OnAim()) {
                _rayPostion = playerCamera.transform.position;
                _rayDirection = playerCamera.transform.forward;
            } else {
                _rayPostion = new Vector3(this.transform.position.x, this.transform.position.y + 1.39f, this.transform.position.z);
                _rayDirection = rotationPivot.transform.forward;
            }
        }
        //End ------ adjust ray cast position on Aim or not;

        if (Physics.Raycast(_rayPostion, _rayDirection, out _hit, weapon.range, layerMask)){

            if (_hit.collider.tag == PLAYER_TAG){
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }

            if (_hit.collider.tag == ENEMY_TAG) { //If it hits enemy tag
                CmdEnemyFreeze(_hit.collider.name);
            }
            Debug.DrawRay(_rayPostion, _rayDirection, Color.red, 1);
        }

        CmdOnShoot(cameraPivot.transform.rotation, _hit.distance);
    }

    //Shoot Bullet runing on Server
    [Command]
    private void CmdOnShoot(Quaternion _bulletRotation, float _distance)
    {
        RpcShootEX(_bulletRotation, _distance);
    }

    ////Shoot Bullet Response effect on clients
    [ClientRpc]
    private void RpcShootEX(Quaternion _bulletRotation, float _distance)
    {
        if (bulletEX != null)
        {
            BulletScript _bullet = (BulletScript)Instantiate(bulletEX, firePoint.position, _bulletRotation);
            if (_distance <= 0)
            {
                _distance = 200;
            }
            _bullet.shootDistance = _distance;
        }
        else
        {
            Debug.LogError("No Bullet Effect game object reference!");
        }
    }

    //Create and Destory Obstacle
    [Client]
    private void OnObstacleAction(){
        RaycastHit _hit;

        if (MobileInputManager.instance.isGamepadConnected == false) {
            if (MobileInputManager.instance.isAim) {
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, weapon.range, layerMask)) {
                    if (_hit.collider.tag == OBSTACLE_TAG) {
                        CmdDestroyObstacle(_hit.collider.name);
                    } else {
                        CreateObstacle();
                    }
                } else {
                    CreateObstacle();
                }
            } else {
                CreateObstacle();
            }
        } else {
            if (ControllerManager.instance.OnAim()) {
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, weapon.range, layerMask)) {
                    if (_hit.collider.tag == OBSTACLE_TAG) {
                        CmdDestroyObstacle(_hit.collider.name);
                    } else {
                        CreateObstacle();
                    }
                } else {
                    CreateObstacle();
                }
            } else {
                CreateObstacle();
            }
        }
    }

    private void CreateObstacle(){
        if (readyToFire == true){
            CmdCreateObstacle();
        }
    }

    [Command]
    private void CmdCreateObstacle(){
        RpcCreateObstacle();
    }

    [ClientRpc]
    private void RpcCreateObstacle(){
        if (obstacleCrate != null){
            if (obstacleNumLimit > 0){
                //MultiplayerObstacleCrateScript _obstacle = (MultiplayerObstacleCrateScript)Instantiate(obstacleCrate, (firePoint.position), firePoint.rotation); //Mostafa - original line

                MultiplayerObstacleCrateScript _obstacle = (MultiplayerObstacleCrateScript)Instantiate(obstacleCrate, (firePoint.position+(firePoint.transform.forward* obstacleRange)), firePoint.rotation); //Mostafa - Obstacle spawns a bit ahead of player

                string _nameID = this.transform.name + obstacleID;
                MultiplayerGameManager.StoreObstacle(_nameID, _obstacle);
                obstacleID++;
                obstacleNumLimit--;
                coolDownTime = 0;
                _obstacle.playerName = this.transform.name;
            }
        }else{
            Debug.LogError("No Obstacle Crate game object reference!");
        }
    }

    //Will be called out of the script. To check if the player is hit by AI;
    public void AIHitPlayer(string _playerName, int _damage) {
        CmdPlayerShot(_playerName, _damage);
    }

    //this method only runing on the server;
    [Command] //Mostafa - Sets speed to 0, saves original speed
    private void CmdPlayerShot(string _playerName, int _damage){
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);
    }

    [Command]
    private void CmdEnemyFreeze(string _enemyName){

        Collider enemyCollider = GameObject.Find(_enemyName).GetComponent<Collider>();
        float enemyOriginalSpeed = enemyCollider.GetComponent<MultiplayerEnemyAI>().speed;

        enemyCollider.GetComponent<MultiplayerEnemyAI>().speed = 0;
        enemyCollider.tag = "FrozenEnemy";
        enemyCollider.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(CmdUnfreeze(4, enemyCollider, enemyOriginalSpeed));
    }

    private IEnumerator CmdUnfreeze(float _timer, Collider _enemyCollider, float _enemyOriginalSpeed) { //Mostafa - Unfreeze Enemy, inherits original speed
        yield return new WaitForSeconds(_timer);

        _enemyCollider.GetComponent<MultiplayerEnemyAI>().speed = _enemyOriginalSpeed;
        _enemyCollider.tag = ENEMY_TAG;

        _enemyCollider.GetComponent<SphereCollider>().enabled = true;

    }

    [Command]
    private void CmdDestroyObstacle(string _obstacleName){
        RpcDestroyObstacle(_obstacleName);
    }

    [ClientRpc]
    private void RpcDestroyObstacle(string _obstacleName){
        MultiplayerObstacleCrateScript _obstacle = MultiplayerGameManager.GetObstacle(_obstacleName);
        MultiplayerGameManager.UnstoreObstacle(_obstacleName);
        Destroy(_obstacle.gameObject);
    }

    private void SetCoolDownLevel(){
        MultiplayerGameManager.instance.coolDownLevel = coolDownTime / coolDownTimeLimit;
    }
}
