using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    private Camera sceneCamera;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer) {
            DisableComponent();
            AssignRemoteLayer();
        } else {
            //Disable Scene Camera when player log in
            sceneCamera = Camera.main;

            if(sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
        }
	}

    private void AssignRemoteLayer() {
        this.gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponent() {
        for (int i = 0; i < componentsToDisable.Length; i++) {
            componentsToDisable[i].enabled = false;
        }
    }

    //On Disable this instance
    private void OnDisable() {
        if(sceneCamera != null) {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
