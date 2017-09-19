using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootAI : MonoBehaviour {
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private float stoneSpeed = 30;
    [SerializeField]
    private float attackRange = 18;
    [SerializeField]
    private float intervalTimer = 3;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Collider poleCollider;
    [SerializeField]
    private List<MultiplayerPlayerManager> players;
    [SerializeField]
    private TowerStoneScript stone;
    private SphereCollider trigger;
    private float currentIntervalTimer;

    private const string PLAYER_TAG = "Player";

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

        if (poleCollider == null) {
            Debug.LogError("No Pole Collider reference!");
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
                    RaycastHit _hit;
                    Vector3 _dir = new Vector3(players[i].transform.position.x, players[i].transform.position.y + 1.5f, players[i].transform.position.z) - firePoint.position;
                    _dir.Normalize();

                    Debug.DrawRay(firePoint.position, _dir * (new Vector3(players[i].transform.position.x, players[i].transform.position.y + 1.5f, players[i].transform.position.z) - firePoint.position).magnitude, Color.yellow, 0.5f);
                    /*If raycast reaches the player, tower will throw stone to the player. 
                    If there is other object between the tower firepoint and the player, raycast cannot reach to the player,
                    so that tower will not throw stone to the object in between; */
                    if (Physics.Raycast(firePoint.position, _dir, out _hit, attackRange, layerMask)) {
                        if(_hit.collider.tag == PLAYER_TAG) {
                            ShootStone(_dir);
                        }
                        Debug.Log(_hit.collider.tag == PLAYER_TAG);
                    }
                }
            }
        }
    }

    private void ShootStone(Vector3 _dir) {
        //Debug.Log(this.transform.name + " is attacking " + players[i].name + "!");
        
        TowerStoneScript _stoneClone = Instantiate(stone, firePoint.position, Quaternion.identity) as TowerStoneScript;
        Rigidbody _stoneRig = _stoneClone.GetComponent<Rigidbody>();
        Collider _stoneCollider = _stoneClone.GetComponent<Collider>();
        Physics.IgnoreCollision(_stoneCollider, poleCollider);
        _stoneClone.damage = damage;
        _stoneRig.AddForce(stoneSpeed * _dir, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == PLAYER_TAG) {
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            players.Add(_player);
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == PLAYER_TAG) {
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            players.Remove(_player);
        }
    }
}
