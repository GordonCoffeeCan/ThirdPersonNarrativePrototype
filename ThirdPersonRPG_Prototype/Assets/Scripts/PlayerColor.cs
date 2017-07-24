using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    //public Color32 bodyColor = new Color32(255, 255, 255, 255);
    private Color32 bodyColor;

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        bodyColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        _renderer = this.transform.Find("HumanBody").GetComponent<Renderer>();
        _renderer.materials[1].SetColor("_Color", bodyColor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
