using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerTimeManager : NetworkBehaviour {

    [SyncVar]
    public float timer=120;
    [SyncVar]
    public bool masterTimer = false;
    [SyncVar]
    public float gameTime;

    public Text timerText;
    private string minSec;


    

    // Use this for initialization
    void Start () {
        //CmdTimer
        if (isServer) {
            if (isLocalPlayer) {
               
            }
        }
    }

  

    // Update is called once per frame
    void Update () {
        CmdTimer();

        minSec = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);

        if (timer > 0) {
            //timerText.text = timer.ToString("F0");

            timerText.text = minSec;
        }else {
            timerText.text = "Time's up!";
        }

        //if(timer>0){ timerText.text = timer.ToString("F0");}


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
