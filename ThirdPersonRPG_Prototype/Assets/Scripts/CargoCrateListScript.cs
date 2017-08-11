using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoCrateListScript : MonoBehaviour {

    public List<GameObject> cargoList;

    public int listSize;
    private int halfOfSize;

    [SerializeField]
    private GameObject cargo;
    public GameObject spawnPointStarter;

    private Vector3 cargoSpawnPoint;

    // Use this for initialization
    void Start() {

        // listSize = 8;
        if (listSize < 1) {
            listSize = 8;
        }

        halfOfSize = listSize / 2;

        for (int i = 0; i < listSize; i++) {
            if (i < halfOfSize) {
                cargoSpawnPoint = new Vector3(spawnPointStarter.transform.position.x - (i * 2), spawnPointStarter.transform.position.y, spawnPointStarter.transform.position.z);
            } else {
                cargoSpawnPoint = new Vector3(spawnPointStarter.transform.position.x - ((i - halfOfSize) * 2), spawnPointStarter.transform.position.y, spawnPointStarter.transform.position.z + 5);
            }

            GameObject _cargoClone = Instantiate(cargo, cargoSpawnPoint, Quaternion.identity);
            _cargoClone.transform.name = "Cargo";

            cargoList.Add(_cargoClone);
        }
    }

    // Update is called once per frame
    /*void Update() {

    }*/
}