using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerObstacleCrateScript : NetworkBehaviour {

    [HideInInspector]
    public string playerName;

    //private float bulletSpeed = 75;

    [SerializeField]
    private float delayTime;

    // Use this for initialization
    void Start () {
        delayTime = 1; //Mostafa - reduced delay from 15 to 1
        StartCoroutine(SelfDestroy(delayTime));
	}

    // Update is called once per frame
    void Update () {
        
    }
    private IEnumerator SelfDestroy(float _timer) {
        yield return new WaitForSeconds(_timer);
        Destroy(this.gameObject);
    }

    private void OnDestroy() {
        GameObject.Find(playerName).GetComponent<MultiplayerShoot>().obstacleNumLimit++;
        MultiplayerGameManager.UnstoreObstacle(this.transform.name);
    }
}
