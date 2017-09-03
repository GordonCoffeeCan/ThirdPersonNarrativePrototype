using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputManager : MonoBehaviour {

    public static MobileInputManager instance;

    [HideInInspector]
    public bool isGamepadConnected = false;

    [SerializeField]
    private ETCTouchPad touchPad;
    [SerializeField]
    private ETCJoystick moveJoystick;
    [SerializeField]
    private Canvas virtualControlPad;

    [HideInInspector]
    public bool isAim;
    
    public Text debugInfo;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        isAim = false;
    }
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        //ActivateVirtualGamePad();
        SetUpStandaloneControllScheme();
        return;
#endif

#if UNITY_STANDALONE
        SetUpStandaloneControllScheme();
        return;
#endif

#if UNITY_ANDROID
        ActivateVirtualGamePad();
#endif

#if UNITY_IOS
        ActivateVirtualGamePad();
#endif

        OnAim();
    }

    private void SetUpStandaloneControllScheme() {
        virtualControlPad.gameObject.SetActive(false);
        isGamepadConnected = true;
    }

    private void ActivateVirtualGamePad() {
        if (Input.GetJoystickNames().Length > 0) {
            if (Input.GetJoystickNames()[0] != "") { //There is a controller connected to the game!
                debugInfo.text = Input.GetJoystickNames()[0];
                virtualControlPad.gameObject.SetActive(false);
                isGamepadConnected = true;
            } else {
                debugInfo.text = "No GamePad connected!"; //There is no controller connected to the game!
                virtualControlPad.gameObject.SetActive(true);
                isGamepadConnected = false;
            }
        } else {
            debugInfo.text = "No GamePad connected!"; //There is no controller connected to the game!
            virtualControlPad.gameObject.SetActive(true);
            isGamepadConnected = false;
        }
    }

    public Vector2 OnTouchPadMove() {
        Vector2 touchPadAxis = new Vector2(touchPad.axisX.axisValue, touchPad.axisY.axisValue);
        return touchPadAxis;
    }

    public Vector3 OnJoystickMove() {
        Vector3 joystickAxis = new Vector3(moveJoystick.axisX.axisValue, 0, moveJoystick.axisY.axisValue);
        return joystickAxis;
    }

    public bool OnFire() {
        return ETCInput.GetButton("ShootButton");
    }

    public bool OnSprint() {
        return ETCInput.GetButton("SprintButton");
    }

    private bool OnAim() {
        if (ETCInput.GetButtonDown("AimButton")) {
            isAim = !isAim;
        }

        return isAim;
    }
}
