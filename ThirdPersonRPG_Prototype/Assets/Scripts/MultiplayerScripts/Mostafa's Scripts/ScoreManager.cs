using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour {

    [SyncVar]
    public float totalScore;

    public Text scoreText;

    // Use this for initialization
    void Start () {

        //totalScore = 0;	
	}
	
	// Update is called once per frame
	void Update () {

        scoreText.text = "total points: "+totalScore.ToString();

	}
}
