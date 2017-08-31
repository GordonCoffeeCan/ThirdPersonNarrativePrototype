using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {


	// Use this for initialization
	void Start () {

        Debug.Log(Input.touchCount);
	}
	
	// Update is called once per frame
	void Update () {
        switch (Input.GetTouch(0).phase) {
            case TouchPhase.Began:
                break;
            case TouchPhase.Moved:
                Debug.Log(Input.GetTouch(0).deltaPosition);
                break;
            case TouchPhase.Ended:
                break;
        }
	}
}
