using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCrateScript : MonoBehaviour {

    [HideInInspector]
    public string playerName;

    //private float bulletSpeed = 75;

    // Use this for initialization
    public float delayTime;

	void Start () {
        delayTime = 15;
        StartCoroutine(SelfDestroy(delayTime));
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(this.transform.forward * bulletSpeed * Time.deltaTime, Space.World);

        //Destroy(this.gameObject, 5);
    }
    private IEnumerator SelfDestroy(float _timer) {
        yield return new WaitForSeconds(_timer);
        Destroy(this.gameObject);
    }

    private void OnDestroy() {
        GameObject.Find(playerName).GetComponent<MultiplayerShoot>().obstacleNumLimit++;
    }

    


}
