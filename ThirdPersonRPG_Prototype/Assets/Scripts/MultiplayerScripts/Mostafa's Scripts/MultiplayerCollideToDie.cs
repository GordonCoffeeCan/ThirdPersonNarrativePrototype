using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerCollideToDie : NetworkBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision _col) {
        if (_col.collider.tag == "Player") {
            CmdPlayerDie(_col.collider.transform.name, 1000);
        }
    }

    [Command]
    private void CmdPlayerDie(string _playerName, int _damage) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);
        _player.RpcDestroyCargo();
    }
}
