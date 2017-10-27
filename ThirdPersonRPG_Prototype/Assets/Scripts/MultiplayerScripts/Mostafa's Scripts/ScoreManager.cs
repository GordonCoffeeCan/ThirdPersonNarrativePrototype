using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour {

    [SyncVar]
    public float totalScore;

    public Text scoreText;

    [HideInInspector]
    public float individualScore;

    public Text individiualScoreText;
   

    // Use this for initialization
    void Start () {
        if (isServer) {
            if (isLocalPlayer) {

            }
        }
        //totalScore = 0;	
    }
	
	// Update is called once per frame
	void Update () {

        CmdScore();

        individualScore = MultiplayerGameManager.instance.playerScore;

        individiualScoreText.text = "you scored " + individualScore.ToString() + " points";

	}

    [Command]
    void CmdScore() {
        RpcScore();
    }

    [ClientRpc]
    void RpcScore() {

        //scoreText.text = "total points: " + totalScore.ToString() +"points";

        scoreText.text = "total points: " + totalScore.ToString();



    }
}
