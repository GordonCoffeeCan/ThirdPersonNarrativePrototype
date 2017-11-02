using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MultiplayerEnemyAI : NetworkBehaviour {

  
    public float speed = 4.5f;
    private Vector3 initialPos;

    private NavMeshAgent navigator;
    // Use this for initialization
    void Start()
    {
        initialPos = this.transform.position; //startingPos


        navigator = this.gameObject.GetComponent<NavMeshAgent>();
        navigator.speed = speed;
        navigator.stoppingDistance = 2;
    }

    // Update is called once per frame
    void Update(){


    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "Player") {
            navigator.ResetPath();
            navigator.SetDestination(_col.transform.position);
        }
    }

    private void OnTriggerStay(Collider _col) {
        if (_col.tag == "Player") {
            navigator.SetDestination(_col.transform.position);
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == "Player") {
            //navigator.ResetPath();
            navigator.SetDestination(initialPos);
        }
    }

    [Command]
    public void CmdPlayerDie(string _playerName, int _damage) {
        //navigator.ResetPath();
        MultiplayerPlayerManager _player = MultiplayerGameManager.GetPlayer(_playerName);
        _player.RpcTakeDamage(_damage);

        navigator.SetDestination(initialPos);
       // Debug.Log(initialPos.position);
        //_player.RpcDestroyCargo();
    }
}
