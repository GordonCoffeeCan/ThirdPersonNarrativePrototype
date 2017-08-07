using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMove : MonoBehaviour {

    public Transform target;
    public Transform self;
    private NavMeshAgent navigator;
    // Use this for initialization
    void Start()
    {
        self = this.transform;
        navigator = this.gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("SinglePlayer") != null)
        {
            target = GameObject.FindGameObjectWithTag("SinglePlayer").transform;

        }
        else
        { target = self; }

            navigator.SetDestination(target.position);




    }
}
