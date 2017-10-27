using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerGameManager : NetworkBehaviour {

    public static MultiplayerGameManager instance;

    public static int obstacleNumber;

    [HideInInspector]
    public float cargoesStolen;

    [HideInInspector]
    public bool isPlayerDie = false;

    [HideInInspector]
    public bool isPlayerInGame = false;

    [HideInInspector]
    public float staminaLevel;

    [HideInInspector]
    public float coolDownLevel;

    [HideInInspector]
    public float playerHealthLevel;

    [HideInInspector]
    public float playerScore; //personal score

    private const string PLAYER_NAME_PREFIX = "Player ";
    private const string OBSTACLE_NAME_PREFIX = "Obstacle";

    private static Dictionary<string, MultiplayerPlayerManager> players = new Dictionary<string, MultiplayerPlayerManager>();

    private static Dictionary<string, MultiplayerObstacleCrateScript> obstacles = new Dictionary<string, MultiplayerObstacleCrateScript>();

    private static Dictionary<string, CargoScript> cargos = new Dictionary<string, CargoScript>();

    private static Dictionary<string, CargoScript> envCargos = new Dictionary<string, CargoScript>();

    [HideInInspector]
    public MultiplayerGameSettings gameSettings;

    private void Awake() {
        if(instance != null) {
            Debug.LogError("More than one Multiplayer Game Manager in the scene!");
        } else {
            instance = this;
            if(players.Count > 0) {
                players.Clear();
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //Register and UnRegister Players
    public static void RegisterPlayer(string _netID, MultiplayerPlayerManager _player) {
        string _playerNameID = PLAYER_NAME_PREFIX + _netID;
        players.Add(_playerNameID, _player);
        _player.transform.name = _playerNameID;
    }

    public static void UnRegisterPlayer(string _playerNetID) {
        players.Remove(_playerNetID);
    }
    //Register and UnRegister Players --- end

    //Store and Unstore Obstacles
    public static void StoreObstacle(string _nameID, MultiplayerObstacleCrateScript _obstacle) {
        string _obstacleNameID = OBSTACLE_NAME_PREFIX + _nameID;
        obstacles.Add(_obstacleNameID, _obstacle);
        _obstacle.transform.name = _obstacleNameID;
    }

    public static void UnstoreObstacle(string _obstacleNameID) {
        obstacles.Remove(_obstacleNameID);
    }
    //Store and Unstore Obstacles --- end

    //Store and Unstore Cargos
    public static void StoreCargo(string _nameID, CargoScript _cargo) {
        cargos.Add(_nameID, _cargo);
        _cargo.transform.name = _nameID;
    }

    public static void UnstoreCargo(string _nameID) {
        cargos.Remove(_nameID);
    }
    //Store and Unstore Cargos --- end

    public static void StoreEnvCargo(string _nameID, CargoScript _cargo) {
        envCargos.Add(_nameID, _cargo);
    }

    public static void UnstoreEnvCargo(string _nameID) {
        envCargos.Remove(_nameID);
    }

    public static MultiplayerPlayerManager GetPlayer(string _playerNameID) {
        return players[_playerNameID];
    }

    public static MultiplayerObstacleCrateScript GetObstacle(string _obstacleNameID) {
        return obstacles[_obstacleNameID];
    }

    public static CargoScript GetCargo(string _cargoNameID) {
        return cargos[_cargoNameID];
    }

    public static CargoScript GetEnvCargo(string _nameID) {
        return envCargos[_nameID];
    } 
}
