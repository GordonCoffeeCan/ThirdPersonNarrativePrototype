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
            if (_col.GetComponent<MultiplayerPlayerManager>().hasCargo == false) {
                this.transform.position = new Vector3(_col.transform.position.x, _col.transform.position.y + 2, _col.transform.position.z);
                this.transform.SetParent(_col.transform);
                _col.GetComponent<MultiplayerPlayerManager>().hasCargo = true;
            }
        }
    }

    [ClientRpc]
    public void RpcDeactivateObject() {
        this.gameObject.SetActive(false);
    }
}
