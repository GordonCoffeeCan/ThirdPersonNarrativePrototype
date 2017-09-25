using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadExamples : MonoBehaviour {

	public void LoadExample(string level){
		//Application.LoadLevel( level );
        SceneManager.LoadScene(level);
	}
}
