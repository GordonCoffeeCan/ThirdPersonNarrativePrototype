using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossPlatformUIManager : MonoBehaviour {

    public static CrossPlatformUIManager instance;

    private CanvasScaler canvasScaler;

    private void Awake() {
        instance = this;

        canvasScaler = this.GetComponent<CanvasScaler>();
    }

    // Use this for initialization
    void Start () {

        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;


#if UNITY_ANDROID
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
#endif

    }

    // Update is called once per frame
    /*void Update () {
		
	}*/
}
