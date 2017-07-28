using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class MultiplayerJoinGame : MonoBehaviour {

    private NetworkManager networkManager;

    private List<GameObject> roomList = new List<GameObject>();
    [SerializeField]
    private Text listStatus;
    [SerializeField]
    private GameObject roomListItem;
    [SerializeField]
    private Transform roomListItemParent;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null) {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshRoomList() {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        listStatus.text = "Loading...";
    }

    public void OnMatchList(bool _success, string _extendedInfo, List<MatchInfoSnapshot> _matchList) {
        listStatus.text = "";

        if(_matchList == null) {
            listStatus.text = "Cannot Find Room List!";
            return;
        }

        ClearRoomList();
        foreach (MatchInfoSnapshot _match in _matchList) {
            GameObject _roomListItemInstance = Instantiate(roomListItem, roomListItemParent);

            //Set name and player count for each server(match) item (button);
            MultiplayerRoomListItem _roomListItem = _roomListItemInstance.GetComponent<MultiplayerRoomListItem>();
            //Check if Server item is empty or not;
            if (_roomListItem != null) {
                _roomListItem.Setup(_match, JoinRoom);
            }

            roomList.Add(_roomListItemInstance);
        }

        if (roomList.Count == 0) {
            listStatus.text = "No Room is created!";
        }
    }

    private void ClearRoomList() {
        for (int i  = 0; i < roomList.Count; i++) {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }

    //This is the real method to join in Room
    public void JoinRoom(MatchInfoSnapshot _match) {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        listStatus.text = "Joining...";
    }
}
