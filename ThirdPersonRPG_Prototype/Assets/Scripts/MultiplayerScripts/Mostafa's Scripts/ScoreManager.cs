﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour {

    [SyncVar]
    public float totalScore;

    public Text scoreText;

    public float individualScore;

    public Text individiualScoreText;
   

    // Use this for initialization
    void Start () {

        //totalScore = 0;	
	}
	
	// Update is called once per frame
	void Update () {

        CmdTimer();

        individiualScoreText.text = "you scored " + individualScore.ToString() + " points";

	}

    [Command]
    void CmdTimer() {
        RpcScore();
    }

    [ClientRpc]
    void RpcScore() {

        //scoreText.text = "total points: " + totalScore.ToString() +"points";

        scoreText.text = "total points: " + totalScore.ToString();



    }
}
