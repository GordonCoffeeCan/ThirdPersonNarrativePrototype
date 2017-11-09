using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerLedgeGrab : MonoBehaviour {

    private float dist;

    private RaycastHit armHit;
    private RaycastHit headHit;

    private Vector3 armPos ;
    private Vector3 headPos;

    public Vector3 forwardFromPivot;

	// Use this for initialization
	void Start () {
        dist = 1;

        armPos = new Vector3(0, 1.75f, 0);
        headPos = new Vector3(0, 2, 0);




        forwardFromPivot = this.transform.Find("RotationPivot").gameObject.transform.forward;

        
        
      


    }
	
	// Update is called once per frame
	void Update () {
        forwardFromPivot = this.transform.Find("RotationPivot").gameObject.transform.forward;
        Debug.DrawRay(this.transform.position + armPos, forwardFromPivot * dist, Color.green);
        Debug.DrawRay(this.transform.position + headPos, forwardFromPivot * dist, Color.red);

        if (Physics.Raycast(this.transform.position + armPos, forwardFromPivot, out armHit, dist)) {
            if (Physics.Raycast(this.transform.position + headPos, forwardFromPivot, out armHit, dist)) {

                print("Cannot ledge grab");
            }
            else {
                print("Can ledge grab");
            }



            //the ray collided with something, you can interact
            // with the hit object now by using hit.collider.gameObject
        }


    }
}
