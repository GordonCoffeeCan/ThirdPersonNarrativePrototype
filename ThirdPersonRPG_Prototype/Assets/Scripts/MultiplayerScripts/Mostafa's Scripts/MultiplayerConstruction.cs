using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerConstruction : NetworkBehaviour {

    public GameObject[] scaffolds;
    public float constructionMaterialsCollected;

    // Use this for initialization
    void Start() {

        for(int i = 0; i < scaffolds.Length; i++) {

            Color transparentColor = scaffolds[i].GetComponent<Renderer>().material.color;

            transparentColor.a = 0;

            scaffolds[i].GetComponent<Renderer>().material.color = transparentColor;
            GameObject scaffoldChild = scaffolds[i].transform.GetChild(0).gameObject;
            scaffoldChild.SetActive(false);
            scaffolds[i].GetComponent<Collider>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            string _playerName = _col.transform.name;
            MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
            if (constructionMaterialsCollected < scaffolds.Length) {
                if (_player.hasCargo == true) {
                    CmdDestroyCargo(_playerName);
                    constructionMaterialsCollected++;
                    for (int i = 0; i < constructionMaterialsCollected; i++) {

                        //Color transparentColor = scaffolds[i].GetComponent<Renderer>().material.color;

                        // transparentColor.a = 256;

                        //scaffolds[i].GetComponent<Renderer>().material.color = transparentColor;

                        scaffolds[i].GetComponent<MeshRenderer>().enabled = false;

                        GameObject scaffoldChild = scaffolds[i].transform.GetChild(0).gameObject;
                        scaffoldChild.SetActive(true);

                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                        // scaffolds[i].GetComponent<Collider>().enabled = true;
                    }

                }
            }
        }
    }

    [Command]
    private void CmdDestroyCargo(string _playerName) {
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        RpcStealCargo();
        _player.RpcDestroyCargo();

    }

    [ClientRpc]
    void RpcStealCargo() {
        MultiplayerGameManager.instance.cargoesStolen++;
    }
}
