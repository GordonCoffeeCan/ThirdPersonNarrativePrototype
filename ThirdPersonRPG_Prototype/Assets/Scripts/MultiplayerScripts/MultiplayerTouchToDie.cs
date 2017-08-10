using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerTouchToDie : NetworkBehaviour {

    private BoxCollider _deathTrigger;

    private void Awake() {
        _deathTrigger = this.GetComponent<BoxCollider>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            CmdPlayerDie(_col.transform.name, 1000);
        }
    }

    [Command]
    private void CmdPlayerDie(string _playerName, int _damage) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);
    }
}
