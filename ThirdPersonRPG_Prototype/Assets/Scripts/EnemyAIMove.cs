using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMove : MonoBehaviour {

    public Transform target;
    public Transform self;
    public string targetTag;

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

        if (GameObject.FindGameObjectWithTag(targetTag) != null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;

        }
        else
        { target = FindClosestEnemy().transform; }

            navigator.SetDestination(target.position);




    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
