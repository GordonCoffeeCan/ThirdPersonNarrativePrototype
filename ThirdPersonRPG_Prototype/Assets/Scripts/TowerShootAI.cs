using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootAI : MonoBehaviour {
    [SerializeField]
    private float damage = 20;
    [SerializeField]
    private float stoneSpeed = 30;
    [SerializeField]
    private float intervalTimer = 3;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private List<MultiplayerPlayerManager> players;
    [SerializeField]
    private float attackRange = 18;
    [SerializeField]
    private StoneScript stone;
    private SphereCollider trigger;
    private float currentIntervalTimer;

    private const string PLAYER_PREFIX = "Player";

    private void Awake() {
        players = new List<MultiplayerPlayerManager>();
        trigger = this.GetComponent<SphereCollider>();
        trigger.radius = attackRange;
        if (!trigger.isTrigger) {
            trigger.isTrigger = true;
        }

        if(stone == null) {
            Debug.LogError("No Stone reference!");
        }
    }

    // Use this for initialization
    void Start () {
        currentIntervalTimer = intervalTimer;
    }
	
	// Update is called once per frame
	void Update () {
        currentIntervalTimer -= Time.deltaTime;
        if (currentIntervalTimer <= 0) {
            AttackPlayer();
            currentIntervalTimer = intervalTimer;
        }
        
	}

    private void AttackPlayer() {
        for (int i = 0; i < players.Count; i++) {
            if (Vector3.Distance(players[i].transform.position, firePoint.position) <= attackRange) {
                if(i == 0) {
                    //Debug.Log(this.transform.name + " is attacking " + players[i].name + "!");
                    Vector3 _dir = players[i].transform.position - firePoint.position;
                    _dir.Normalize();
                    StoneScript _stoneClone = Instantiate(stone, firePoint.position, Quaternion.identity) as StoneScript;
                    Rigidbody _stoneRig = _stoneClone.GetComponent<Rigidbody>();
                    _stoneRig.AddForce(stoneSpeed * _dir, ForceMode.Impulse);
                    //_stoneClone.damage = damage;
                }
                Debug.Log(this.transform.name + " is watching " + players[i].name + "!");
            }
        }
    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == PLAYER_PREFIX) {
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            players.Add(_player);
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == PLAYER_PREFIX) {
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            players.Remove(_player);
        }
    }
}
