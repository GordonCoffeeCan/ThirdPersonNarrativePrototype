using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToBoxes : MonoBehaviour {
    [SerializeField]
    public GameObject cargo;
  
   

    // Use this for initialization
    void Start() {

        cargo.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider _col) {
        if (_col.tag == "EnemyBody") {
            Destroy(_col.transform.parent.gameObject);

            cargo.SetActive(true);

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }
}
