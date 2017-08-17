using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CargoScript : NetworkBehaviour {
    public Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        originalPosition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if(_col.tag == "Player") {
            string _playerName = _col.transform.name;
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            if (_player.hasCargo == false) {
                CmdTakeCargo(_playerName);
                _player.hasCargo = true;
                this.gameObject.SetActive(false);
            }
        }

        
    }

    [Command]
    private void CmdTakeCargo(string _playerName) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeCargo();
    }

    [ClientRpc]
    public void RpcDeactivateObject() {
        this.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
