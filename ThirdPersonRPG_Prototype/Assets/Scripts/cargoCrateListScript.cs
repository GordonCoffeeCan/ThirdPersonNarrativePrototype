using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargoCrateListScript : MonoBehaviour {

    public List<GameObject> cargoList;

    public int listSize;
    private int halfOfSize;

    public GameObject cargo;
    public GameObject spawnPointStarter;

    private Vector3 cargoSpawnPoint;
    private GameObject newCargo;

	// Use this for initialization
	void Start () {

        // listSize = 8;
        if (listSize < 1)
        {
            listSize = 8;
        }

        halfOfSize = listSize / 2;

        for (int i = 0; i < listSize; i++)
        {
            if (i < halfOfSize)
            {
                cargoSpawnPoint = new Vector3(spawnPointStarter.transform.position.x - (i * 2), spawnPointStarter.transform.position.y, spawnPointStarter.transform.position.z);
            }
            else
            {
                cargoSpawnPoint = new Vector3(spawnPointStarter.transform.position.x - ((i - halfOfSize) * 2), spawnPointStarter.transform.position.y, spawnPointStarter.transform.position.z + 5);
            }
            cargoList.Add(Instantiate(cargo, cargoSpawnPoint, Quaternion.identity));
            

        }
    }
	
	// Update is called once per frame
	void Update () {

      
		
	}
}
