using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextScreen : MonoBehaviour {

    public GameObject winText;

    public GameObject zig1;
    public GameObject zig2;
    public GameObject zig3;
    public GameObject zig4;

	// Use this for initialization
	void Start () {

        if (zig1.activeInHierarchy) {
            if (zig2.activeInHierarchy) {
                if (zig3.activeInHierarchy) {
                    if (zig4.activeInHierarchy) {
                        winText.SetActive(true);
                    }
                }
            }

        }
        else {
            winText.SetActive(false);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
