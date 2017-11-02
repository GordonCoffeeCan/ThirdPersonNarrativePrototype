using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerTimeManager : NetworkBehaviour {

    [SyncVar]
    public float timer=30;
    [SyncVar]
    public bool masterTimer = false;
    [SyncVar]
    public float gameTime;

    [SyncVar]
    public bool started = false;


    public float roundTimer;
    private float preTimer = 10;

    public Text timerText;
    private string minSec;

    public GameObject gate;
    public GameObject panel;


    

    // Use this for initialization
    void Start () {
        gate = GameObject.Find("gate");
        if (panel == null) {
            panel = GameObject.Find("EndPanel");
        }
        panel.SetActive(false);

        //CmdTimer
        timer = preTimer;


        if (isServer) {
            if (isLocalPlayer) {
               
            }
        }
    }

  

    // Update is called once per frame
    void Update () {
        minSec = string.Format("{0}:{1:00}", (int)timer / 60, (int)timer % 60);
        CmdTimer();
        if (started == false) {
            timerText.text = "match starts in " +minSec;
            if (timer <= 1) {
                started = true;
                timer = roundTimer;
                Destroy(gate);

            }
        }
        else {
            if (timer > 0) {
                //timerText.text = timer.ToString("F0");

                timerText.text = minSec;
            }
            else {
                timerText.text = "Time's up!";
                panel.SetActive(true);
            }
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
