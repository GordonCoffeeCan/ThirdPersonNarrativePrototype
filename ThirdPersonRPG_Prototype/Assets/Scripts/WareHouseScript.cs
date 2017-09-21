using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WareHouseScript : NetworkBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            string _playerName = _col.transform.name;
            MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
            if (_player.hasCargo == true) {
                CmdDestroyCargo(_playerName);
                
                
            }
        }
    }
    
    [Command]
    private void CmdDestroyCargo(string _playerName) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        RpcStealCargo();
        _player.RpcDestroyCargo();
       
    }

    [ClientRpc]
    void RpcStealCargo() {
        MultiplayerGameManager.instance.cargoesStolen++;
    }
}
