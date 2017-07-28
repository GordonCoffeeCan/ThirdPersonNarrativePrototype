using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIManager : NetworkBehaviour {

    public static bool isMenuPanelOn = false;

    [SerializeField]
    private Image reticleUI;

    [SerializeField]
    private Image menuPanel;

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {

        if (reticleUI != null) {
            reticleUI.gameObject.SetActive(false);
        }

        if(menuPanel != null) {
            menuPanel.gameObject.SetActive(false);
        }

        networkManager = NetworkManager.singleton;

        isMenuPanelOn = false;
    }
	
	// Update is called once per frame
	void Update () {

        //show reticle when aim;
        if (ControllerManager.instacne.OnAim() && MultiplayerGameManager.instance.isPlayerInGame == true && MultiplayerGameManager.instance.isPlayerDie == false && isMenuPanelOn == false) {
            reticleUI.gameObject.SetActive(true);
        } else {
            reticleUI.gameObject.SetActive(false);
        }

        //Show in game menu;
        if (ControllerManager.instacne.OnMenu()) {
            isMenuPanelOn = !menuPanel.gameObject.activeSelf;
            menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeSelf);
        }
	}

    public void LeaveRoom() {
        networkManager.matchMaker.DropConnection(networkManager.matchInfo.networkId, networkManager.matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}
