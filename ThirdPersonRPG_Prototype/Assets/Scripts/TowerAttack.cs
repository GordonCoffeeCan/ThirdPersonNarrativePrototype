using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private List<MultiplayerPlayerManager> players;
    [SerializeField]
    private float attackRange = 18;
    private SphereCollider trigger;

    private const string PLAYER_PREFIX = "Player";

    private void Awake() {
        players = new List<MultiplayerPlayerManager>();
        trigger = this.GetComponent<SphereCollider>();
        trigger.radius = attackRange;
        if (!trigger.isTrigger) {
            trigger.isTrigger = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < players.Count; i++) {
            Debug.Log(players[i].name);
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
