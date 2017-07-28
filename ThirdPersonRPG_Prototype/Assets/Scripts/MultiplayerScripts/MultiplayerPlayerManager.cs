using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerPlayerManager : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    private Camera sceneCamera;

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    private bool _isDead = false;

    private bool[] wasEnabled;

    public bool isDead {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer) {
            DisableComponent();
            AssignRemoteLayer();
        } else {
            //Disable Scene Camera when player log in
            sceneCamera = Camera.main;

            if (sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        SetupOnStart();
    }

    // Update is called once per frame
    /*void Update () {
		
	}*/

    public void SetupOnStart() {

        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++) {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();

    }

    public override void OnStartClient() {
        base.OnStartClient();

        string _netID = this.GetComponent<NetworkIdentity>().netId.ToString();
        MultiplayerGameManager.RegisterPlayer(_netID, this);
    }

    private void AssignRemoteLayer() {
        this.gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponent() {
        for (int i = 0; i < componentsToDisable.Length; i++) {
            componentsToDisable[i].enabled = false;
        }
    }

    public void SetDefaults() {
        isDead = false;
        MultiplayerGameManager.instance.isPlayerDie = isDead;
        MultiplayerGameManager.instance.isPlayerInGame = true;

        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        currentHealth = maxHealth;

        /*CharacterController _charCtr = GetComponent<CharacterController>();
        if(_charCtr != null) {
            _charCtr.enabled = true;
        }*/
    }

    //This method is called on the server, but runs on Clients
    [ClientRpc]
    public void RpcTakeDamage(int _damageAmount) {

        if(isDead == true) {
            return;
        }

        currentHealth -= _damageAmount;

        if (currentHealth <= 0) {
            Die();
        }

        Debug.Log(this.transform.name + " is hit! Current health is " + currentHealth);
    }

    private void Die() {
        isDead = true;

        Debug.Log(this.transform.name + " is Dead!");

        //Call disable components on Death;
        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = false;
        }

        /*CharacterController _charCtr = GetComponent<CharacterController>();
        if(_charCtr != null) {
            _charCtr.enabled = true;
        }*/

        //Call respawn method after a few seconds;
        StartCoroutine(Respawn(MultiplayerGameManager.instance.gameSettings.respawnDelayTime));
    }

    //Respawn player;
    private IEnumerator Respawn(float _timer) {
        yield return new WaitForSeconds(_timer);

        SetDefaults();

        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        this.transform.position = _spawnPoint.position;
        this.transform.rotation = _spawnPoint.rotation;
    }

    //On Disable this instance
    private void OnDisable() {
        if (sceneCamera != null) {
            sceneCamera.gameObject.SetActive(true);
        }

        MultiplayerGameManager.UnRegisterPlayer(this.transform.name);
    }
}
