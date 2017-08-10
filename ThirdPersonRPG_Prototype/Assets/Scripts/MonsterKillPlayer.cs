using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKillPlayer : MonoBehaviour {

    [SerializeField]
    private MultiplayerEnemyAI parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            parent.CmdPlayerDie(_col.transform.name, 1000);
        }
    }
}
