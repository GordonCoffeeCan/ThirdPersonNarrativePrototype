using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Image reticleUI;

    private ControllerAxis controllerAxis;

	// Use this for initialization
	void Start () {

        controllerAxis = new ControllerAxis();


        if (reticleUI != null) {
            reticleUI.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //show reticle when aim;
        if (Input.GetButton(controllerAxis.aimButton) || Input.GetAxis(controllerAxis.aimTrigger) > 0.2f) {
            reticleUI.gameObject.SetActive(true);
        } else {
            reticleUI.gameObject.SetActive(false);
        }
	}
}
