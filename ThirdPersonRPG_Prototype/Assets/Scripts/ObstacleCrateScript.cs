using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCrateScript : MonoBehaviour {

    //public float shootDistance;

    //private float bulletSpeed = 75;

    // Use this for initialization

    public float delayTime;

	void Start () {
        delayTime = 15;

        Destroy(this.gameObject, delayTime);
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(this.transform.forward * bulletSpeed * Time.deltaTime, Space.World);

        //Destroy(this.gameObject, 5);
    }

    
}
