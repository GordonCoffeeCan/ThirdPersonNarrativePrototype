using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(this.transform.forward * 90 * Time.deltaTime, Space.World);

        Destroy(this.gameObject, 1);
    }
}
