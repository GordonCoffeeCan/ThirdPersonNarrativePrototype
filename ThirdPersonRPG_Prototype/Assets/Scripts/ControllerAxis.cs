using System;

public class ControllerAxis {

    public string moveHorizontalAxis;
    public string moveVerticalAxis;
    public string cameraHorizontalAxis;
    public string cameraVerticalAxis;

    public string aimButton;
    public string aimTrigger;

    public ControllerAxis() {

        moveHorizontalAxis = "Horizontal";
        moveVerticalAxis = "Vertical";

        cameraHorizontalAxis = "CamHorizontal";
        cameraVerticalAxis = "CamVertical";

        aimButton = "Aim";
        aimTrigger = "Triggers";

#if UNITY_EDITOR

#endif

#if UNITY_STANDALONE

#endif

#if UNITY_ANDROID

        cameraHorizontalAxis = "Android_R_Stick_H";
        cameraVerticalAxis = "Android_R_Stick_V";

#endif
    }

}
