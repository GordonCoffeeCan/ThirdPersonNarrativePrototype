using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;

    private Camera sceneCamera;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer) {
            for(int i = 0; i < componentsToDisable.Length; i++) {
                componentsToDisable[i].enabled = false;
            }
        } else {
            sceneCamera = Camera.main;

            if(sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }

            CameraDynamicOrbit.followingTarget = this.transform;
        }
	}

    //On Disable this instance
    private void OnDisable() {
        if(sceneCamera != null) {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
