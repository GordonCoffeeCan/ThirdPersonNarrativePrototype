using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    public float walkSpeed = 2;
    //public float runSpeed = 5;

    private float _gravity = 20;

    private float _currentSpeed = 0;

    private Transform _camPivot;

    private CharacterController _characterCtr;

    private Vector3 _moveDirection;

    private void Awake() {
        _characterCtr = this.GetComponent<CharacterController>();
        _moveDirection = Vector3.zero;

        _camPivot = GameObject.Find("CameraPivot").transform;
    }

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        MoveCharacter();
    }

    private void MoveCharacter() {
        _currentSpeed = walkSpeed;

        if (_characterCtr.isGrounded) {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = Camera.main.transform.TransformDirection(_moveDirection);
            _moveDirection.y = 0;
            _moveDirection.Normalize();
            _moveDirection *= _currentSpeed;

        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        _characterCtr.Move(_moveDirection * Time.deltaTime);
    }

    
}
