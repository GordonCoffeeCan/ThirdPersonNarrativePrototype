using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiplayerShoot : NetworkBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Transform rotationPivot;

    [SerializeField]
    private LayerMask layerMask;

    private ControllerAxis controllerAxis;

    private Text debugInfo;

    // Use this for initialization
    void Start () {
        if (playerCamera == null) {
            Debug.LogError("No Player Camera referenced!");
            this.enabled = false;
        } else {
            controllerAxis = new ControllerAxis();
            debugInfo = GameObject.Find("DebugInfo").GetComponent<Text>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(controllerAxis.fireButton) || Input.GetAxis(controllerAxis.fireTrigger) > 0.2f) {
            shoot();
        }
	}

    private void shoot() {

        if(rotationPivot == null) {
            Debug.LogError("No Roation Pivot referenced!");
            return;
        }

        RaycastHit _hit;

        if ((Input.GetButton(controllerAxis.aimButton) || Input.GetAxis(controllerAxis.aimTrigger) > 0.2f)) {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _hit, weapon.range, layerMask)) {
                Debug.Log(_hit.collider.name + " is Hit!");
                debugInfo.text = _hit.collider.name + " is Hit!";
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red, 1);
            }
        } else {
            if (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 1.65f, this.transform.position.z), rotationPivot.transform.forward, out _hit, weapon.range, layerMask)) {
                Debug.Log(_hit.collider.name + " is Hit!");
                Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 1.65f, this.transform.position.z), rotationPivot.transform.forward, Color.red, 1);
                debugInfo.text = _hit.collider.name + " is Hit!";
            }
        }

        
    }
}
