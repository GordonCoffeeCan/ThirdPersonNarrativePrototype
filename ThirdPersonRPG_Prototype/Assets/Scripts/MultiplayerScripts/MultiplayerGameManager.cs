using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour {

    public static MultiplayerGameManager instance;

    private static Dictionary<string, MultiplayerPlayer> players = new Dictionary<string, MultiplayerPlayer>();

    private void Awake() {
        if(instance != null) {
            Debug.LogError("More than one Multiplayer Game Manager in the scene!");
        } else {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
