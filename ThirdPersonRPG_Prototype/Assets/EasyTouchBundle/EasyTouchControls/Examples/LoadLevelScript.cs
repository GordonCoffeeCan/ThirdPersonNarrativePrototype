using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevelScript : MonoBehaviour {

	public void LoadMainMenu(){
		//Application.LoadLevel( "MainMenu");
        SceneManager.LoadScene("MainMenu");
	}

	public void LoadJoystickEvent(){
		//Application.LoadLevel( "Joystick-Event-Input");
        SceneManager.LoadScene("Joystick-Event-Input");
    }

	public void LoadJoysticParameter(){
        //Application.LoadLevel("Joystick-Parameter");
        SceneManager.LoadScene("Joystick-Parameter");
    }

	public void LoadDPadEvent(){
        //Application.LoadLevel("DPad-Event-Input");
        SceneManager.LoadScene("DPad-Event-Input");
    }

	public void LoadDPadClassicalTime(){
        //Application.LoadLevel("DPad-Classical-Time");
        SceneManager.LoadScene("DPad-Classical-Time");
    }

	public void LoadTouchPad(){
        //Application.LoadLevel ("TouchPad-Event-Input");
        SceneManager.LoadScene("TouchPad-Event-Input");
    }

	public void LoadButton(){
        //Application.LoadLevel("Button-Event-Input");
        SceneManager.LoadScene("Button-Event-Input");
    }

	public void LoadFPS(){
        //Application.LoadLevel("FPS_Example");
        SceneManager.LoadScene("FPS_Example");
    }
}
