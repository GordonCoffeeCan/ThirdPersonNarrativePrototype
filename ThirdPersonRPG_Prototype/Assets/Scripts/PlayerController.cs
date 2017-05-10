using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    private float _walkSpeed = 2;
    private float _runSpeed = 5;
    private float _cameraRotationSpeed = 70;
    private float _camFollowSpeed = 8;
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
        RotateCamera();
        CameraFollow();
    }

    private void MoveCharacter() {
        _currentSpeed = _walkSpeed;

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

    private void RotateCamera() {
        _camPivot.Rotate(Input.GetAxis("Mouse Y") * _cameraRotationSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * _cameraRotationSpeed * Time.deltaTime, 0 );
        _camPivot.localEulerAngles = new Vector3(_camPivot.localEulerAngles.x, _camPivot.localEulerAngles.y, 0);
    }

    private void CameraFollow() {
        _camPivot.position = Vector3.Lerp(_camPivot.position, _characterCtr.transform.position, _camFollowSpeed * Time.deltaTime);
    }
}
