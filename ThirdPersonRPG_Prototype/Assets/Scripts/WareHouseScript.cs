using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            MultiplayerPlayerManager _player = _col.GetComponent<MultiplayerPlayerManager>();
            if (_player.hasCargo == true) {
                CargoScript _cargo = _col.transform.Find("CargoCrate").GetComponent<CargoScript>();
                _cargo.RpcDeactivateObject();
                _player.hasCargo = false;
            }
        }
    }
}
