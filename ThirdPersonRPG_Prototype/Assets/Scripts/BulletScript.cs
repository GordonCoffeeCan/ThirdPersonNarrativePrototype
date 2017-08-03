using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float shootDistance;

    private float bulletSpeed = 75;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(this.transform.forward * bulletSpeed * Time.deltaTime, Space.World);

        Destroy(this.gameObject, shootDistance/bulletSpeed);
    }
}
