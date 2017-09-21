using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerTimeManager : NetworkBehaviour {

    [SyncVar]
    private float timer=30;
    [SyncVar]
    public bool masterTimer = false;
    [SyncVar]
    public float gameTime;

    public Text timerText;

    

    // Use this for initialization
    void Start () {
        //CmdTimer()

        if (isServer) {
            if (isLocalPlayer) {
               
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        CmdTimer();
        timerText.text = timer.ToString("F2");
	}

    [Command]
    void CmdTimer() {
        RpcTimer();
    }

    [ClientRpc]
    void RpcTimer() {

        timer -= Time.deltaTime;

    }
}
