﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerHostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 4;
    private string roomName;

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;

        if (networkManager.matchMaker == null) {
            networkManager.StartMatchMaker();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRoomName(string _name) {
        roomName = _name;
    }

    public void CreateRoom() {
        if(roomName != "" && roomName != null) {
            Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players");

            //Create Room;
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0 , 0, networkManager.OnMatchCreate);
        }
    }
}
