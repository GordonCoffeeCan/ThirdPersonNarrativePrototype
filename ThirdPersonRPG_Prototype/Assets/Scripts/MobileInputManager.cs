using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputManager : MonoBehaviour {

    public static MobileInputManager instance;

    public ETCTouchPad touchPad;
    public ETCJoystick moveJoystick;
    public Canvas virtualControlPad;
    public Text debugInfo;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR

#endif

#if UNITY_STANDALONE
        virtualControlPad.gameObject.SetActive(false);
        this.enabled = false;
        return;
#endif

#if UNITY_ANDROID
        //debugInfo.text = "No GamePad connected!";
        virtualControlPad.gameObject.SetActive(true);
        this.enabled = true;
#endif

#if UNITY_IOS
        virtualControlPad.gameObject.SetActive(true);
        this.enabled = true;
#endif
    }

    public Vector2 OnTouchPadMove() {
        Vector2 touchPadAxis = new Vector2(touchPad.axisX.axisValue, touchPad.axisY.axisValue);
        return touchPadAxis;
    }

    public Vector3 OnJoystickMove() {
        Vector3 joystickAxis = new Vector3(moveJoystick.axisX.axisValue, 0, moveJoystick.axisY.axisValue);
        return joystickAxis;
    }
}
