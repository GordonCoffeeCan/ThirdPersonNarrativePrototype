using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MultiplayerRoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    private JoinRoomDelegate joinRoomCallback;

    private MatchInfoSnapshot match;

    [SerializeField]
    private Text roomName;

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback) {
        match = _match;
        joinRoomCallback = _joinRoomCallback;
        roomName.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    //The method to join room on click. then JoinRoom() in MultiplayerJoinGame will work to do real thing;
    public void JoinRoom() {
        joinRoomCallback.Invoke(match);
    }
}
