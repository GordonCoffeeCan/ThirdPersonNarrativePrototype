using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MultiplayerUIManager : NetworkBehaviour {

    public static bool isMenuPanelOn = false;

    [SerializeField]
    private Image reticleUI;

    [SerializeField]
    private Image menuPanel;

    [SerializeField]
    private Image staminaRefill;

    [SerializeField]
    private Image coolDownRefill;

    //[SerializeField]
    //private Text timerUI;

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

        //Check the current scene is single player mode or multiplayer mode;
        //show reticle when aim;
        if (SceneManager.GetActiveScene().name == "Singleplayer_Level") {
            if (ControllerManager.instance.OnAim() && isMenuPanelOn == false) {
                reticleUI.gameObject.SetActive(true);
            } else {
                reticleUI.gameObject.SetActive(false);
            }
        }else if(SceneManager.GetActiveScene().name == "Multiplayer_Level") {
            if (MobileInputManager.instance.enabled == true) {
                if (MobileInputManager.instance.isAim && MultiplayerGameManager.instance.isPlayerInGame == true && MultiplayerGameManager.instance.isPlayerDie == false && isMenuPanelOn == false) {
                    reticleUI.gameObject.SetActive(true);
                } else {
                    reticleUI.gameObject.SetActive(false);
                }
            } else {
                if (ControllerManager.instance.OnAim() && MultiplayerGameManager.instance.isPlayerInGame == true && MultiplayerGameManager.instance.isPlayerDie == false && isMenuPanelOn == false) {
                    reticleUI.gameObject.SetActive(true);
                } else {
                    reticleUI.gameObject.SetActive(false);
                }
            }
            
        }

        //Show in game menu;
        if (ControllerManager.instance.OnMenu()) {
            isMenuPanelOn = !menuPanel.gameObject.activeSelf;
            menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeSelf);
        }

        //timerUI.text = Mathf.Floor(MultiplayerGameManager.instance.gameTime).ToString();

        staminaRefill.fillAmount = MultiplayerGameManager.instance.staminaLevel;
        coolDownRefill.fillAmount = MultiplayerGameManager.instance.coolDownLevel;
    }

    public void LeaveRoom() {
        networkManager.matchMaker.DropConnection(networkManager.matchInfo.networkId, networkManager.matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}
