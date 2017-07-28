using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControllerManager : MonoBehaviour {
    public static ControllerManager instacne;
    public string moveHorizontalAxis;
    public string moveVerticalAxis;
    public string cameraHorizontalAxis;
    public string cameraVerticalAxis;
    public string fireButton;
    public string fireTrigger;
    public string aimButton;
    public string aimTrigger;
    public string MenuButton;
    public string BackButton;

    private void Awake() {
        instacne = this;

        moveHorizontalAxis = "Horizontal";
        moveVerticalAxis = "Vertical";

        cameraHorizontalAxis = "CamHorizontal";
        cameraVerticalAxis = "CamVertical";

        fireButton = "Fire";
        fireTrigger = "Right_Trigger";
        aimButton = "Aim";
        aimTrigger = "Left_Trigger";

        MenuButton = "Menu";
        BackButton = "Back";

#if UNITY_EDITOR

#endif

#if UNITY_STANDALONE

#endif

#if UNITY_ANDROID
        aimTrigger = "L_2_Aim";
        fireTrigger = "R_2_Fire";
        cameraHorizontalAxis = "Android_R_Stick_H";
        cameraVerticalAxis = "Android_R_Stick_V";
        MenuButton = "Android_Menu";
        BackButton = "Android_Back";
#endif
    }

    public Vector3 OnMove() {
        return new Vector3(Input.GetAxis(moveHorizontalAxis), 0, Input.GetAxis(moveVerticalAxis));
    }

    public bool OnAim() {
        return (Input.GetButton(aimButton) || Input.GetAxis(aimTrigger) > 0.2f);
    }

    public bool OnFire() {
        return (Input.GetButtonDown(fireButton) || Input.GetAxis(fireTrigger) > 0.2f);
    }

    public bool OnMenu() {
        return Input.GetButtonUp(MenuButton);
    }

    public bool OnBack() {
        return Input.GetButtonUp(BackButton);
    }
}
