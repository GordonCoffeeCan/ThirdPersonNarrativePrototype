using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Image reticleUI;

	// Use this for initialization
	void Start () {

        if (reticleUI != null) {
            reticleUI.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //show reticle when aim;
        if (ControllerManager.instacne.OnAim()) {
            reticleUI.gameObject.SetActive(true);
        } else {
            reticleUI.gameObject.SetActive(false);
        }
	}
}
