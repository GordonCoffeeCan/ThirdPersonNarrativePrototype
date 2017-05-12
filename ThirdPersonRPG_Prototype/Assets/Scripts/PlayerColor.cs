using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    public Color32 bodyColor = new Color32(255, 255, 255, 255);

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer = this.transform.FindChild("RotationPivot/HumanBody").GetComponent<Renderer>();
        _renderer.materials[1].SetColor("_Color", bodyColor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
