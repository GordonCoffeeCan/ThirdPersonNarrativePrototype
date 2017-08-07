using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MultiplayerEnemyAIMove : NetworkBehaviour {

    [SerializeField]
    private float speed = 2.5f;

    private NavMeshAgent navigator;
    // Use this for initialization
    void Start()
    {
        navigator = this.gameObject.GetComponent<NavMeshAgent>();
        navigator.speed = speed;
        navigator.stoppingDistance = 2;
    }

    // Update is called once per frame
    void Update(){


    }

    private void OnTriggerStay(Collider _col) {
        if (_col.tag == "Player") {
            navigator.SetDestination(_col.transform.position);
            navigator.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider _col) {
        if (_col.tag == "Player") {
            navigator.SetDestination(this.transform.position);
            navigator.isStopped = true;
        }
    }
}
