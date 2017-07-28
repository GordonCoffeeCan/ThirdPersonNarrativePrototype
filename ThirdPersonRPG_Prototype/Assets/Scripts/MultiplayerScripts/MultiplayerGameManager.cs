using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerGameManager : MonoBehaviour {

    public static MultiplayerGameManager instance;

    [HideInInspector]
    public bool isPlayerDie = false;

    [HideInInspector]
    public bool isPlayerInGame = false;

    private const string PLAYER_NAME_PREFIX = "Player ";

    private static Dictionary<string, MultiplayerPlayerManager> players = new Dictionary<string, MultiplayerPlayerManager>();

    [HideInInspector]
    public MultiplayerGameSettings gameSettings;

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

    public static void RegisterPlayer(string _netID, MultiplayerPlayerManager _player) {
        string _playerNameID = PLAYER_NAME_PREFIX + _netID;
        players.Add(_playerNameID, _player);
        _player.transform.name = _playerNameID;
    }

    public static void UnRegisterPlayer(string _playerNetID) {
        players.Remove(_playerNetID);
    }

    public static MultiplayerPlayerManager GetPlayer(string _playerNameID) {
        return players[_playerNameID];
    }

    /*private void OnGUI() {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string _playerNetID in players.Keys) {
            GUILayout.Label(_playerNetID + " - " + players[_playerNetID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
}
