using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour {

    [HideInInspector]
    public float speed = 0;
    [HideInInspector]
    public float damage = 0;

    private float destoryTimer = 8;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, destoryTimer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision _col) {
        if(_col.gameObject.tag == "Player") {
            Debug.Log("Hit Player!");
        }
    }
}
