using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour {

    [HideInInspector] public bool isLocalInstance = true;

	// Use this for initialization
	void Start () {
        isLocalInstance = isLocalPlayer;
        if(isLocalInstance == true) {
            CameraDynamicOrbit.followingTarget = this.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
