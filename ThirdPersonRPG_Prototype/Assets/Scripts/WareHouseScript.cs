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
            Transform _cargo = _col.transform.Find("CargoCrate");
            _col.GetComponent<MultiplayerPlayerManager>().hasCargo = false;
            _cargo.gameObject.SetActive(false);
        }
    }
}
