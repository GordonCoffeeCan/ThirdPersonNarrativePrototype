using System;

public class ControllerAxis {

    public string moveHorizontalAxis;
    public string moveVerticalAxis;
    public string cameraHorizontalAxis;
    public string cameraVerticalAxis;
    public string fireButton;
    public string fireTrigger;
    public string aimButton;
    public string aimTrigger;

    public ControllerAxis() {

        moveHorizontalAxis = "Horizontal";
        moveVerticalAxis = "Vertical";

        cameraHorizontalAxis = "CamHorizontal";
        cameraVerticalAxis = "CamVertical";

        fireButton = "Fire";
        fireTrigger = "Right_Trigger";
        aimButton = "Aim";
        aimTrigger = "Left_Trigger";

#if UNITY_EDITOR

#endif

#if UNITY_STANDALONE

#endif

#if UNITY_ANDROID
        aimTrigger = "L_2_Aim";
        fireTrigger = "R_2_Fire";
        cameraHorizontalAxis = "Android_R_Stick_H";
        cameraVerticalAxis = "Android_R_Stick_V";
#endif
    }

}
