using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerObstacleCrateScript : NetworkBehaviour {

    [HideInInspector]
    public string playerName;

    //private float bulletSpeed = 75;

    [SerializeField]
    private float lifeSpan = 2;

    // Use this for initialization
    void Start () {
        StartCoroutine(SelfDestroy(lifeSpan));
	}

    // Update is called once per frame
    void Update () {
        
    }
    private IEnumerator SelfDestroy(float _delay) {
        yield return new WaitForSeconds(_delay);
        Destroy(this.gameObject);
    }

    private void OnDestroy() {
        GameObject.Find(playerName).GetComponent<MultiplayerShoot>().obstacleNumLimit++;
        MultiplayerGameManager.UnstoreObstacle(this.transform.name);
    }
}
